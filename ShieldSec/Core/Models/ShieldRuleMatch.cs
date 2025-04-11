using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Models
{
    /// <summary>
    ///  Final match result with contextual information
    /// </summary>
    public class ShieldRuleMatch
    {
        /// <summary>
        ///  The rule that was matched
        /// </summary>
        public required ShieldRule Rule { get; set; }
        /// <summary>
        ///  Match details
        /// </summary>
        public required string MatchDetails { get; set; }
        /// <summary>
        ///  Timestamp of the match
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
