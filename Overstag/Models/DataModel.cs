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
        public DbSet<Participate> Participate { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string
            optionsBuilder.UseMySQL("server=localhost;database=test;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one2many
            /*modelBuilder.Entity<Account>()
            .HasOne<Parent>(s => s.Parent)
            .WithMany(g => g.Children)
            .HasForeignKey(s => s.ParentId);*/

            //many2many
            modelBuilder.Entity<Participate>()
            .HasKey(t => new { t.UserId, t.EventId });

            modelBuilder.Entity<Participate>()
            .HasOne(pt => pt.User)
            .WithMany(p => p.Participants)
            .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<Participate>()
            .HasOne(pt => pt.Event)
            .WithMany(t => t.Participants)
            .HasForeignKey(pt => pt.EventId);

        }
    }

    
}
