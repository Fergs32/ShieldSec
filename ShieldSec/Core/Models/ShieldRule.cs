using ShieldSec.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Models
{
    /// <summary>
    ///  Object representing a Shield rule
    /// </summary>
    public class ShieldRule
    {
        /// <summary>
        ///  The name of the rule
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  The description of the rule (required)
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        ///  Pattern type of the rule
        /// </summary>
        public PatternType Type { get; set; }
        /// <summary>
        ///  The pattern to match, for example a regex pattern
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        ///  Entry threshold for entropy
        /// </summary>
        public double? EntropyThreshold { get; set; }
        /// <summary>
        ///  Severity level of the rule
        /// </summary>
        public SeverityLevel SeverityLevel { get; set; }
        /// <summary>
        ///  Allowed file extensions
        /// </summary>
        public HashSet<string> TargetExtensions { get; set; } = new();
        /// <summary>
        ///  Minimum file size
        /// </summary>
        public int MinimumFileSize { get; set; } = 0;
        /// <summary>
        ///  Require a signature check
        /// </summary>
        public bool RequireSignatureCheck { get; set; }
        /// <summary>
        ///  Allow signed files
        /// </summary>
        public bool AllowSignedFiles { get; set; } = true;
    }
}
