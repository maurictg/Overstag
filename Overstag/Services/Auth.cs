using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models.Database;
using Overstag.Models.Database.Meta;


namespace Overstag.Services
{
    public static class Auth
    {
        public static string CreateToken(AuthType type, int accountId, int length = 16, int validWeeks = 8)
        {
            using (var db = new Database())
            {
                var auth = new Models.Database.Auth()
                {
                    AccountId = accountId,
                    ExpiresAt = DateTime.Now.AddMonths(validWeeks),
                    RegisteredAt = DateTime.Now,
                    Type = type,
                    Token = Encryption.Random.rString(length)
                };

                try
                {
                    db.Auths.Add(auth);
                    db.SaveChanges();
                    return auth.Token;
                }
                catch(Exception e)
                {
                    Console.WriteLine("[ERR] Failed to generate auth: " + e.ToString());
                    return null;
                }
            }
        }
    }
}
