using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Responses;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Data
{
    public class ApiDbContext : IdentityDbContext<Client, Role, int>
    {
        public virtual DbSet<Client> Client { get; set; }
        //  public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<Excercises> Excercises {get; set; }
        public virtual DbSet<ExcerciseType> ExcerciseType { get; set; }
        public virtual DbSet<Trainers> Trainers { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderPayment> OrderPayments { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>().HasOne(cc => cc.UserCreditCard).WithOne().HasForeignKey<UserCreditCard>(a => a.Id)
                                           .OnDelete(DeleteBehavior.Cascade);



            builder.Entity<Client>().Ignore(c => c.EmailConfirmed)
                                           .Ignore(c => c.PhoneNumberConfirmed)
                                           .Ignore(c => c.TwoFactorEnabled)
                                           .Ignore(c => c.LockoutEnd)
                                           .Ignore(c => c.LockoutEnabled)
                                           .Ignore(c => c.AccessFailedCount)
                                           .Ignore(c => c.PhoneNumber);

            builder.Entity<Role>().HasData(
                    new Role {Id = 1, Name = "Admin", NormalizedName = "ADMIN"},
                    new Role {Id = 2, Name = "Trainer", NormalizedName = "TRAINER"},
                    new Role {Id = 3, Name = "Client", NormalizedName = "CLIENT"}
            );
        }
    }
}
