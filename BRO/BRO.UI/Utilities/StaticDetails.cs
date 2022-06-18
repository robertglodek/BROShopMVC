using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.Utilities
{
    public static class StaticDetails
    {
        public static List<string> GetAvailableTutorialsNames()
        {
            return new List<string>() { "Bmi", "Bwt", "Bk" };
        }
    }
}
