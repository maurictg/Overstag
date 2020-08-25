using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.Database
{
    [Table("file")]
    public class File
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Mimetype { get; set; }
    }
}
