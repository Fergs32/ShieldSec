using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Analysis.PostProcessing
{
    /// <summary>
    ///  This class is used to check if a file is signed with a certificate whose serial number is in the malicious list.
    /// </summary>
    public class SerialNumberChecker
    {
        private readonly ConcurrentDictionary<string, string> _maliciousSerialNumbers;
        /// <summary>
        /// Constructor for the Serial Number Checker class.
        /// </summary>
        /// <param name="maliciousSerialNumbers"> A dictionary of malicious serial numbers.</param>
        public SerialNumberChecker()
        {
            _maliciousSerialNumbers = new ConcurrentDictionary<string, string>();
            Initialize();
        }
        /// <summary>
        ///  Initializes the serial number checker by loading the malicious serial numbers from disk.
        /// </summary>
        public void Initialize()
        {
            try
            {
                var serialNumbers = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "malicious_serial_numbers.txt"));
                var malwareNames = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "corresonding_malware_names.txt"));

                if (serialNumbers.Length != malwareNames.Length)
                {
                    throw new Exception("The number of serial numbers does not match the number of malware names.");
                }

                for (int i = 0; i < serialNumbers.Length; i++)
                {
                    string serial = serialNumbers[i].Trim();
                    string malwareName = malwareNames[i].Trim();

                    _maliciousSerialNumbers.TryAdd(serial, malwareName);
                }

                Debug.WriteLine($"Loaded {_maliciousSerialNumbers.Count} malicious serial numbers.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error loading files: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during initialization: {ex.Message}");
            }
        }
        /// <summary>
        /// Checks if a file is signed with a certificate whose serial number is in the malicious list.
        /// </summary>
        /// <param name="filePath">The file to check.</param>
        /// <param name="maliciousSerialNumbers">
        /// A set of malicious certificate serial numbers (as hex strings, e.g. "6D75E5B8C1234D2A").
        /// </param>
        /// <returns>True if the file's signing certificate serial number matches one in the list; otherwise, false.</returns>
        public Task<bool> IsFileMalicious(string filePath)
        {
            try
            {
                X509Certificate certificate = X509Certificate.CreateFromSignedFile(filePath);

                string serialNumber = certificate.GetSerialNumberString();

                serialNumber = serialNumber.ToUpperInvariant();

                return Task.FromResult(_maliciousSerialNumbers.ContainsKey(serialNumber));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking certificate for file '{filePath}': {ex.Message}");
                return Task.FromResult(false);
            }
        }
        /// <summary>
        ///  Returns the malware name associated with the specified serial number.
        /// </summary>
        /// <param name="serialNumber"> The serial number to check.</param>
        /// <returns> The malware name associated with the serial number, or "N/A" if not found.</returns>
        public string getMalwareName(string serialNumber)
        {
            if (_maliciousSerialNumbers.TryGetValue(serialNumber, out string? malwareName))
            {
                return malwareName;
            }

            return "N/A";
        }
    }
}
