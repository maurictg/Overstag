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
        public DbSet<Family> Families { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Accountancy.Transaction> Transactions { get; set; }
        public DbSet<Accountancy.Request> Requests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string
#if DEBUG
            optionsBuilder.UseSqlServer(Core.General.Credentials.msSqlDebugCString);
            //optionsBuilder.UseSqlServer(Core.General.Credentials.msSqlLiveCString);

            optionsBuilder.EnableSensitiveDataLogging();
#else
            optionsBuilder.UseSqlServer(Core.General.Credentials.msSqlConnectionString);
#endif
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Mapping relations between tables

            //Many to many (event aan user koppelen)
            mb.Entity<Participate>()
              .HasKey(ue => new { ue.EventID, ue.UserID });

            /*mb.Entity<Participate>()
                .HasOne(u => u.Event)
                .WithMany(e => e.Participators)
                .HasForeignKey(f => f.EventID);

            mb.Entity<Participate>()
                .HasOne(u => u.User)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(f => f.UserID);*/

            //Many to many (idee aan user koppelen)
            mb.Entity<Vote>()
                .HasKey(v => new { v.IdeaID, v.UserID });

            /*mb.Entity<Vote>()
                .HasOne(u => u.User)
                .WithMany(v => v.Votes)
                .HasForeignKey(w => w.UserID);

            mb.Entity<Vote>()
                .HasOne(v => v.Idea)
                .WithMany(w => w.Votes)
                .HasForeignKey(x => x.IdeaID);*/

            //One to many (account aan familie koppelen)
            mb.Entity<Account>()
                .HasOne(f => f.Family)
                .WithMany(n => n.Members);

            //One to many (account aan invoice koppelen)
            mb.Entity<Account>()
                .HasMany(f => f.Invoices)
                .WithOne(g => g.User)
                .OnDelete(DeleteBehavior.ClientCascade);

            //One to many (account aan payments)
            mb.Entity<Account>()
                .HasMany(f => f.Payments)
                .WithOne(g => g.User)
                .OnDelete(DeleteBehavior.ClientCascade);

            //One to many (account aan requests)
            mb.Entity<Account>()
                .HasMany(f => f.Requests)
                .WithOne(g => g.User)
                .OnDelete(DeleteBehavior.ClientCascade);

            //One to many (account aan auths)
            mb.Entity<Account>()
                .HasMany(f => f.Auths)
                .WithOne(g => g.User)
                .OnDelete(DeleteBehavior.ClientCascade);

            //One to one (payment aan invoice)
            mb.Entity<Invoice>()
                .HasOne(f => f.Payment)
                .WithOne(g => g.Invoice)
                .HasForeignKey<Payment>(x => x.InvoiceId);

        }

        
    }
}
