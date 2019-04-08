using Microsoft.EntityFrameworkCore;
/// <summary>
/// Database structuur
/// </summary>
namespace Overstag.Models
{
    //Tabellen maken
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string
            optionsBuilder.UseMySQL("server=localhost;database=test;user=root;password=root");
        }
    }

    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
