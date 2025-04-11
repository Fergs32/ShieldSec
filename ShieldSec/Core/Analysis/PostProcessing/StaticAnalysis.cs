using PeNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Analysis.PostProcessing
{
    public class StaticAnalysis
    {
        // TODO: I want to make this set of suspicious imports configurable, meaning that the user can add or remove imports from the list.
        private static readonly HashSet<string> SuspiciousImports = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "VirtualAlloc",
            "WriteProcessMemory",
            "CreateRemoteThread",
            "LoadLibraryA",
            "GetProcAddress",
        };
        /// <summary>
        /// Analyzes a PE file and returns a TreeNode containing detailed information.
        /// </summary>
        /// <param name="filePath">The path to the PE file.</param>
        /// <returns>A TreeNode with the file name as root and details as child nodes.</returns>
        /// <summary>
        /// Analyzes a PE file and returns a TreeNode containing a detailed, "straight-to-the-point" analysis.
        /// </summary>
        /// <param name="filePath">The path to the PE file.</param>
        /// <returns>A TreeNode with the file's analysis details.</returns>
        public static TreeNode AnalyzePEFileToTreeNode(string filePath)
        {
            try
            {
                var peFile = new PeFile(filePath);
                var rootNode = new TreeNode(Path.GetFileName(filePath));
                var fileInfoNode = new TreeNode("File Information");
                var fi = new FileInfo(filePath);
                fileInfoNode.Nodes.Add($"File Size: {fi.Length} bytes");
                fileInfoNode.Nodes.Add($"Last Modified: {fi.LastWriteTime}");
                fileInfoNode.Nodes.Add($"File Location: {filePath}");
                fileInfoNode.Nodes.Add($"MD5: {peFile.Md5}");
                fileInfoNode.Nodes.Add($"SHA1: {peFile.Sha1}");
                fileInfoNode.Nodes.Add($"SHA256: {peFile.Sha256}");
                rootNode.Nodes.Add(fileInfoNode);

                var headersNode = new TreeNode("Headers");
                headersNode.Nodes.Add($"Image Base: 0x{peFile.ImageNtHeaders.OptionalHeader.ImageBase:X}");
                headersNode.Nodes.Add($"Entry Point: 0x{peFile.ImageNtHeaders.OptionalHeader.AddressOfEntryPoint:X}");
                headersNode.Nodes.Add($"Machine: {peFile.ImageNtHeaders.FileHeader.Machine}");
                headersNode.Nodes.Add($"Number of Sections: {peFile.ImageNtHeaders.FileHeader.NumberOfSections}");
                headersNode.Nodes.Add($"Time Date Stamp: {UnixTimeStampToDateTime(peFile.ImageNtHeaders.FileHeader.TimeDateStamp)}");
                rootNode.Nodes.Add(headersNode);

                var sectionsNode = new TreeNode("Sections");
                foreach (var section in peFile.ImageSectionHeaders)
                {
                    string sectionName = section.Name.TrimEnd('\0');
                    string sectionInfo = $"{sectionName} - Virtual Size: {section.VirtualSize} bytes";

                    // if the section is both writable and executable, we need to mark it as suspicious.
                    // why? because it's a common technique used by malware to inject code into a process, although it's not always malicious, we should still be cautious.
                    // Mappings below:
                    // WRITE = 0x80000000, EXECUTE = 0x20000000.
                    // IMAGE_SCN_MEM_EXECUTE = 0x20000000, IMAGE_SCN_MEM_WRITE = 0x80000000.
                    // References:
                    // https://learn.microsoft.com/en-us/windows/win32/debug/pe-format
                    // https://docs.microsoft.com/en-us/windows/win32/debug/pe-format#section-characteristics
                    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
                    // https://secana.github.io/PeNet/articles/sections.html
                    bool isExecutable = ((uint)section.Characteristics & 0x20000000) != 0;
                    bool isWritable = ((uint)section.Characteristics & 0x80000000) != 0;
                    if (isExecutable && isWritable)
                    {
                        sectionInfo += " [Suspicious: RWX]";
                    }

                    sectionsNode.Nodes.Add(sectionInfo);
                }
                rootNode.Nodes.Add(sectionsNode);
                var importsNode = new TreeNode("Imported Functions");
                if (peFile.ImportedFunctions != null && peFile.ImportedFunctions.Any())
                {
                    foreach (var imp in peFile.ImportedFunctions)
                    {
                        string importStr = $"{imp.DLL} -> {imp.Name}";
                        if (SuspiciousImports.Contains(imp.Name))
                        {
                            importStr += " [Suspicious]";
                        }
                        importsNode.Nodes.Add(importStr);
                    }
                }
                else
                {
                    importsNode.Nodes.Add("No imported functions found.");
                }
                rootNode.Nodes.Add(importsNode);

                var suspiciousImports = SuspiciousImports.Intersect(peFile.ImportedFunctions.Select(i => i.Name));
                if (suspiciousImports.Any())
                {
                    var suspiciousImportsNode = new TreeNode("Suspicious Imports");
                    foreach (var imp in suspiciousImports)
                    {
                        suspiciousImportsNode.Nodes.Add(imp);
                    }
                    rootNode.Nodes.Add(suspiciousImportsNode);
                }
                var suspiciousSummary = new TreeNode("Suspicious Features Summary");
                if (sectionsNode.Nodes.Cast<TreeNode>().Any(n => n.Text.Contains("[Suspicious: RWX]")))
                {
                    suspiciousSummary.Nodes.Add("One or more sections are both writable and executable.");
                }
                if (importsNode.Nodes.Cast<TreeNode>().Any(n => n.Text.Contains("[Suspicious]")))
                {
                    suspiciousSummary.Nodes.Add("Suspicious imported functions detected.");
                }
                if (peFile.ImageNtHeaders.FileHeader.NumberOfSections > 10)
                {
                    suspiciousSummary.Nodes.Add("Unusually high number of sections detected.");
                }
                if (suspiciousSummary.Nodes.Count == 0)
                {
                    suspiciousSummary.Nodes.Add("No suspicious features detected.");
                }
                rootNode.Nodes.Add(suspiciousSummary);

                rootNode.Nodes.Add($"Threat Score: {calculateThreatScore(peFile)}/100 | {calculateThreatScore(peFile) switch { < 30 => "Low", < 60 => "Medium", _ => "High" }}");

                return rootNode;
            }
            catch (Exception ex)
            {
                return new TreeNode($"Error analyzing file: {ex.Message}");
            }
        }

        private static DateTime UnixTimeStampToDateTime(uint unixTimeStamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        private static int calculateThreatScore(PeFile peFile)
        {
            bool isTrustedSigner = peFile.IsTrustedAuthenticodeSignature;
            int score = 0;
            if (!isTrustedSigner)
            {
                score += 10;
            }
            if (peFile.ImportedFunctions != null)
            {
                foreach (var imp in peFile.ImportedFunctions)
                {
                    if (SuspiciousImports.Contains(imp.Name))
                    {
                        score += 10;
                    }
                }
            }
            if (peFile.ImageSectionHeaders != null)
            {
                foreach (var section in peFile.ImageSectionHeaders)
                {
                    bool isExecutable = ((uint)section.Characteristics & 0x20000000u) != 0;
                    bool isWritable = ((uint)section.Characteristics & 0x80000000u) != 0;
                    if (isExecutable && isWritable)
                    {
                        score += 20;
                    }
                }
            }
            if (peFile.ImageNtHeaders.FileHeader.NumberOfSections > 10)
            {
                score += 5;
            }
            return score;
        }
    }
}
