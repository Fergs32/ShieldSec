using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Models
{
    /// <summary>
    ///  Result of a rule match
    /// </summary>
    public class ShieldRuleMatchResult
    {
        /// <summary>
        ///  Rule that was matched
        /// </summary>
        public required ShieldRule Rule { get; set; }
        /// <summary>
        ///  Is the rule a match
        /// </summary>
        public bool IsMatch { get; set; }
        /// <summary>
        ///  Additional details about the match
        /// </summary>
        public string Details { get; set; }
    }
}
