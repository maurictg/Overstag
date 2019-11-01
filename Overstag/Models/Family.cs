using System;
using System.Collections.Generic;

namespace Overstag.Models
{
    public class Family
    {
        public int Id { get; set; }
        public int ParentID { get; set; }
        public string Token { get; set; }

        //Relations
        public List<Account> Members { get; set; }
    }
}
