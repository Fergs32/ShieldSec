using ShieldSec.Core.Enums;
using ShieldSec.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShieldSec.Core.Managers
{
    /// <summary>
    ///  This class is responsible for managing the shield rules.
    ///  Here are some references that helped me to implement this class:
    ///  - https://stackoverflow.com/questions/4890789/regex-for-an-ip-address
    ///  - https://stackoverflow.com/questions/5717312/regular-expression-for-url
    ///  - https://en.wikipedia.org/wiki/Entropy_(information_theory)
    ///  - https://www.elastic.co/blog/ten-process-injection-techniques-technical-survey-common-and-trending-process
    /// </summary>
    public sealed class ShieldRuleManager
    {
        private readonly List<ShieldRule>? _malwareRules;
        private readonly new HashSet<string> highRiskExtensions = new HashSet<string>
        {
            ".exe", ".scr", ".sys", ".drv",
            ".ps1", ".bat", ".cmd", ".vbs", ".js",
            ".docm", ".xlsm", ".pptm", ".jar", ".msi"
        };
        /// <summary>
        ///  Constructor to initialize the malware rules
        /// </summary>
        public ShieldRuleManager()
        {
            _malwareRules = new List<ShieldRule>();
            InitializeMalwareRules();
        }
        /// <summary>
        ///  Checks the file content against all malware rules and returns detailed matches
        /// </summary>
        /// <param name="fileContent">The content of the file to check</param>
        /// <param name="textContent">The text content of the file to check</param>
        /// <returns>List of matched shield rules with detection details</returns>
        public async Task<List<ShieldRuleMatch>> CheckMalwareAsync(String filePath)
        {
            var matches = new List<ShieldRuleMatch>();

            var fileContent = await File.ReadAllBytesAsync(filePath);
            var textContent = Encoding.UTF8.GetString(fileContent);

            if (_malwareRules == null) return matches;

            foreach (var rule in _malwareRules)
            {
                var matchResult = CheckRuleMatch(fileContent, textContent, rule, filePath);
                if (matchResult.IsMatch)
                {
                    matches.Add(new ShieldRuleMatch
                    {
                        Rule = rule,
                        MatchDetails = matchResult.Details,
                        Timestamp = DateTime.UtcNow
                    });
                }
            }

            return matches;
        }
        /// <summary>
        ///  Initializes the malware rules with common detection patterns
        /// </summary>
        private void InitializeMalwareRules()
        {
            _malwareRules?.Add(new ShieldRule
            {
                Name = "Suspicious Network Patterns",
                Type = PatternType.Regex,
                Pattern = @"\b(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b",
                Description = "Public IP addresses in non-network files",
                SeverityLevel = SeverityLevel.Medium,
                TargetExtensions = highRiskExtensions
            });

            //_malwareRules?.Add(new ShieldRule
            //{
            //    Name = "Packed Executable",
            //    Type = PatternType.Entropy,
            //    TargetExtensions = highRiskExtensions,
            //    EntropyThreshold = 7.2,
            //    MinimumFileSize = 40960, // when the file size is less than 40KB, we don't check for packed executables
            //    Description = "High entropy in executable/script files",
            //    SeverityLevel = SeverityLevel.High,
            //    RequireSignatureCheck = true
            //});

            //_malwareRules?.Add(new ShieldRule
            ///{
            //    Name = "Executable Header",
            //    Type = PatternType.FileHeader,
            //    Pattern = "4D5A", // "MZ" in ASCII, in other words, the header of an executable file
            //    Description = "Detects executable files",
            //    SeverityLevel = SeverityLevel.Critical
            //});

            _malwareRules?.Add(new ShieldRule
            {
                Name = "Suspicious Process Injection",
                Type = PatternType.Regex,
                Pattern = @"\b(CreateRemoteThread|VirtualAllocEx|WriteProcessMemory)\b",
                TargetExtensions = highRiskExtensions,
                Description = "Common injection patterns in binaries",
                SeverityLevel = SeverityLevel.High
            });
        }
        /// <summary>
        ///  Enhanced rule checking with detailed match information
        /// </summary>
        private ShieldRuleMatchResult CheckRuleMatch(byte[] fileContent, string textContent, ShieldRule rule, string filePath)
        {
            var result = new ShieldRuleMatchResult { Rule = rule };
            var extension = Path.GetExtension(filePath).ToLower();


            try
            {
                if (rule.TargetExtensions.Any() && !rule.TargetExtensions.Contains(extension)) return result;

                if (fileContent.Length < rule.MinimumFileSize) return result;

                switch (rule.Type)
                {
                    case PatternType.Regex:
                        var regex = new Regex(rule.Pattern, RegexOptions.IgnoreCase);
                        var matches = regex.Matches(textContent);
                        if (matches.Count > 0)
                        {
                            result.IsMatch = true;
                            result.Details = $"Found {matches.Count} matches for pattern: {rule.Pattern}";
                        }
                        break;

                    case PatternType.Hex:
                        var hexPattern = rule.Pattern.Replace(" ", "");
                        var patternBytes = StringToByteArray(hexPattern);
                        if (SearchBytes(fileContent, patternBytes))
                        {
                            result.IsMatch = true;
                            result.Details = $"Hex pattern found: {rule.Pattern}";
                        }
                        break;

                    case PatternType.Entropy:
                        var entropy = CalculateEntropy(fileContent);
                        if (entropy > rule.EntropyThreshold)
                        {
                            result.IsMatch = true;
                            result.Details = $"High entropy detected: {entropy:0.00} (Threshold: {rule.EntropyThreshold:0.00})";
                        }
                        break;

                    case PatternType.FileHeader:
                        var headerPattern = StringToByteArray(rule.Pattern);
                        if (fileContent.Length >= headerPattern.Length &&
                            fileContent.Take(headerPattern.Length).SequenceEqual(headerPattern))
                        {
                            result.IsMatch = true;
                            result.Details = "Matching file header found";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Details = $"Rule check failed: {ex.Message}";
            }
            return result;
        }
        /// <summary>
        ///  Calculate the entropy of a given byte array.
        ///  Reference: https://en.wikipedia.org/wiki/Entropy_(information_theory)
        /// </summary>
        /// <param name="data"> The byte array to calculate the entropy from </param>
        /// <returns> The entropy value </returns>
        private double CalculateEntropy(byte[] data)
        {
            // we use a dictionary to store the frequency of each byte in the byte array
            var frequency = new Dictionary<byte, int>();
            foreach (var b in data)
            {
                // if the byte is not in the dictionary, add it with a value of 0
                if (!frequency.ContainsKey(b)) frequency[b] = 0;
                frequency[b]++;
            }

            double entropy = 0;
            var len = data.Length;
            foreach (var count in frequency.Values)
            {
                var probability = (double)count / len;
                // based on the formula: H(X) = -Σ p(x) * log2(p(x))
                // where p(x) is the probability of the byte x
                // and log2 is the base 2 logarithm
                // and H(X) is the entropy of the byte array
                // we calculate the entropy of the byte array
                entropy -= probability * Math.Log2(probability);
            }

            return entropy;
        }
        /// <summary>
        ///  Converts a hex string to a byte array.
        /// </summary>
        /// <param name="hex"> The hex string to convert </param>
        /// <returns> The byte array </returns>
        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                // select every 2 characters
                .Where(x => x % 2 == 0)
                // we convert each 2 characters to a byte, and store them in an array
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
        /// <summary>
        ///  Searches for a byte array in another byte array.
        /// </summary>
        /// <param name="haystack"> The byte array to search in </param>
        /// <param name="needle"> The byte array to search for </param>
        /// <returns> True if the needle is found in the haystack, false otherwise </returns>
        private bool SearchBytes(byte[] haystack, byte[] needle)
        {
            // we to -needle.length so we don't go out of bounds, and also ensures there are enough bytes remaining in haystack for a complete match
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                // a visual representation of what going on here is below
                // haystack: 1 2 3 4 5 6 7 8 9 10
                // needle: 3 4 5
                // we take the first 3 bytes from haystack and compare them to the needle
                // if they match, we return true

                // I probably could use the sliding window technique here, but I think this is more readable
                if (haystack.Skip(i).Take(needle.Length).SequenceEqual(needle))
                    return true;
            }
            return false;
        }
    }
}
