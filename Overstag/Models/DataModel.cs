using Microsoft.EntityFrameworkCore;
using Overstag.Models;
/// <summary>
/// Database structuur
/// </summary>
namespace Overstag.Models
{
    //Tabellen maken
    public class OverstagContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invoice> Invoices { get; set; } 
        public DbSet<Logging> Logging { get; set; }
        public DbSet<Family> Families { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string
            optionsBuilder.UseMySQL("server=localhost;database=test;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Many to many (event aan user koppelen)
            mb.Entity<Participate>()
              .HasKey(ue => new { ue.EventID, ue.UserID });

            mb.Entity<Participate>()
                .HasOne(u => u.Event)
                .WithMany(e => e.Participators)
                .HasForeignKey(f => f.EventID);

            mb.Entity<Participate>()
                .HasOne(u => u.User)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(f => f.UserID);

            //One to may (account aan familie koppelen)
            mb.Entity<Account>()
                .HasOne(a => a.Family)
                .WithMany(l => l.Members);

        }

        
    }

    //No database field
    public class SQLQuery
    {
        public string Query { get; set; }
        public string TableName { get; set; }
        public int Type { get; set; }
    }

}
