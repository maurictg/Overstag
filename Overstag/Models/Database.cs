using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Overstag.Models.Database;
using Overstag.Models.Database.Relations;
using Overstag.Models.Database.Meta;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Overstag.Models.Database
{
    public class Database : DbContext
    {
        //* = intermediate table
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Group> Groups { get; set; }
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
            //Conversions for JSON objects
            mb.Entity<Invoice>()
                .Property(x => x.Subscriptions)
                .HasConversion(y => JsonSerializer.SerializeToUtf8Bytes(y, null), z => JsonSerializer.Deserialize<List<SubscriptionData>>(z, null));

            mb.Entity<Invoice>()
                .Property(x => x.UserData)
                .HasConversion(y => JsonSerializer.SerializeToUtf8Bytes(y, null), z => JsonSerializer.Deserialize<UserData>(z, null));

            //Relations
            mb.Entity<User>()
                .HasOne(x => x.Account)
                .WithOne(y => y.User)
                .HasForeignKey<User>(z => z.AccountId);

            mb.Entity<Account>()
                .HasOne(x => x.User)
                .WithOne(y => y.Account)
                .HasForeignKey<Account>(z => z.UserId);

            mb.Entity<User>()
                .HasMany(x => x.Subscriptions)
                .WithOne(y => y.User)
                .HasForeignKey(z => z.UserId);

            mb.Entity<User>()
                .HasMany(x => x.Votes)
                .WithOne(y => y.User)
                .HasForeignKey(z => z.UserId);

            mb.Entity<User>()
                .HasMany(x => x.Invoices)
                .WithOne(y => y.User)
                .HasForeignKey(z => z.UserId);

            mb.Entity<User>()
                .HasOne(x => x.Group)
                .WithMany(y => y.Participants)
                .HasForeignKey(z => z.GroupId);

            mb.Entity<Account>()
                .HasMany(x => x.Auths)
                .WithOne(y => y.Account)
                .HasForeignKey(z => z.AccountId);

            mb.Entity<Activity>()
                .HasMany(x => x.Subscriptions)
                .WithOne(y => y.Activity)
                .HasForeignKey(z => z.ActivityId);

            mb.Entity<Suggestion>()
                .HasMany(x => x.Votes)
                .WithOne(y => y.Suggestion)
                .HasForeignKey(z => z.SuggestionId);

            mb.Entity<Invoice>()
                .HasOne(x => x.Payment)
                .WithOne(y => y.Invoice)
                .HasForeignKey<Invoice>(z => z.PaymentId);

            mb.Entity<Payment>()
                .HasOne(x => x.Invoice)
                .WithOne(y => y.Payment)
                .HasForeignKey<Payment>(z => z.InvoiceId);

            //Many to many
            mb.Entity<Subscription>()
                .HasKey(x => new { x.ActivityId, x.UserId });

            mb.Entity<Vote>()
                .HasKey(x => new { x.SuggestionId, x.UserId });
        }
    }
}
