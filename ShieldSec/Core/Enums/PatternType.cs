using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Enums
{
    /// <summary>
    ///  Enum for the pattern type of a rule
    /// </summary>
    public enum PatternType
    {
        Regex,
        Hex,
        Entropy,
        FileHeader
    }
}
