using InstituteManagement_Models.Subscriptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InstituteManagement_Models.Context
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> RegionList { get; set;}
        public DbSet<UserAccountConfirmations> UserAccountConfirmations { get; set;}
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderData> OrderData { get; set; }
        public DbSet<SubscriptionData> SubscriptionData { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Subscription>()
        //        .HasOne(s => s.Plans)
        //        .WithMany(p =>
        //        {
        //            return p.Subscription;
        //        })
        //        .HasForeignKey(s => s.PlanId);

        //    modelBuilder.Entity<Plans>()
        //        .HasMany(p => p.Subscription)
        //        .WithOne(s => s.Plans)
        //        .HasForeignKey(s => s.PlanId);

        //    // Rest of your model configuration
        //}
    }


    
}
