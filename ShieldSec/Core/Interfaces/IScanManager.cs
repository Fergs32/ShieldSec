namespace ShieldSec.Core.Interfaces
{
    /// <summary>
    ///  Interface for the ScanManager class.
    /// </summary>
    public interface IScanManager
    {
        /// <summary>
        ///  Scans the file and returns the result.
        /// </summary>
        /// <param name="filePath"> The path to the file to scan </param>
        /// <returns> True if the file is known to be malicious, false otherwise </returns>
        Task<bool> ScanFileAndGetResultAsync(string filePath);
    }
}
