using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public string IP { get; set; }
        public DateTime Registered { get; set; }
    }
}
