using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Services
{
    public static class Html
    {
        public static string YesNo(bool yes)
            => $"<b class=\"{((yes) ? "green-text" : "red-text")}\">{((yes) ? "Ja" : "Nee")}</b>";

        public static string ManWoman(bool man)
            => $"<b class=\"{((man) ? "blue-text" : "red-text")}\">{((man) ? "Man" : "Vrouw")}</b>";

        public static string MV(bool man)
           => ManWoman(man).Replace("Man","M").Replace("Vrouw","V");
    }
}
