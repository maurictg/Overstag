using System.Text.Json.Serialization;
using System;

namespace Overstag.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string IP { get; set; }
        public DateTime Registered { get; set; }

        //Relations
        public int UserId { get; set; }
        [JsonIgnore]
        public Account User { get; set; }
    }
}
