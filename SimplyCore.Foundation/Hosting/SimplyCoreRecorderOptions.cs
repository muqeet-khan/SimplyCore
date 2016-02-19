using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyCore.Foundation.Hosting
{
    public class SimplyCoreRecorderOptions
    {

        public SimplyCoreRecorderOptions()
        {
            MatchPattern = "!Simply";
        }

        public string MatchPattern { get; set; }
    }
}
