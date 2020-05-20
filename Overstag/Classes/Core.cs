using System;
using System.IO;
using System.Text.Json;

namespace Overstag.Core
{
    public class Credentials
    {
        public string mailUsername { get; set; }
        public string mailPass { get; set; }
        public string mySqlConnectionString { get; set; }
        public string msSqlConnectionString { get; set; }
        public string msSqlLiveCString { get; set; }
        public string mySqlLiveCString { get; set; }
        public string mollieApiToken { get; set; }
        public string msSqlDebugCString { get; set; }

        /// <summary>
        /// Get credentials from file on server
        /// </summary>
        /// <returns>Credentials object</returns>
        public Credentials Get()
        {
            Console.WriteLine("[LOG] Reading credentials from file...");
            return JsonSerializer.Deserialize<Credentials>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "credentials.json")));
        }
    }

    public static class General
    {
        private static Credentials _credentials;
        public static Credentials Credentials { 
            get {
                if (_credentials == null)
                    _credentials = new Credentials().Get();

                return _credentials; 
            } 
        }

        public static bool DateIsPassed(DateTime check)
            => check < DateTime.Now;

        public static int getAge(DateTime bd)
         => (new DateTime(DateTime.Now.Year, bd.Month, bd.Day) > DateTime.Now ? (DateTime.Now.Year - bd.Year)-1 : (DateTime.Now.Year - bd.Year));

    }
}
