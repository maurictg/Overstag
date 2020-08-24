using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Overstag.Models.Database;
using Overstag.Models.Database.Relations;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    public class Database : DbContext
    {
        //* = intermediate table
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; } // * user_activity
        public DbSet<Suggestion> Suggestions { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; } // * user_suggestion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string
#if DEBUG
            optionsBuilder.UseMySql(Core.General.Credentials.mySqlConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
#else
            optionsBuilder.UseMySql(Core.General.Credentials.mySqlLiveCString);
            //optionsBuilder.UseSqlServer(Core.General.Credentials.msSqlConnectionString);
#endif
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasMany(x => x.Subscriptions)
                .WithOne(y => y.User);

            mb.Entity<User>()
                .HasMany(x => x.Votes)
                .WithOne(y => y.User);

            mb.Entity<User>()
                .HasMany(x => x.Invoices)
                .WithOne(y => y.User);

            mb.Entity<Account>()
                .HasMany(x => x.Auths)
                .WithOne(y => y.Account);

            mb.Entity<Account>()
                .HasOne(x => x.User)
                .WithOne(y => y.Account);

            mb.Entity<Activity>()
                .HasMany(x => x.Subscriptions)
                .WithOne(y => y.Activity);

            mb.Entity<Suggestion>()
                .HasMany(x => x.Votes)
                .WithOne(y => y.Suggestion);

            mb.Entity<Invoice>()
                .HasOne(x => x.Payment)
                .WithOne(y => y.Invoice);
        }
    }
}
