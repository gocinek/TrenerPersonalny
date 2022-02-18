using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Data
{
    public class ApiDbContext : IdentityDbContext<Client>
    {
        public virtual DbSet<Client> Client { get; set; }
      //  public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }


        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Client>() //.Ignore(c => c.NormalizedEmail)
                                           .Ignore(c => c.EmailConfirmed)
                                           // .Ignore(c => c.PasswordHash)
                                           .Ignore(c => c.PhoneNumberConfirmed)
                                           .Ignore(c => c.TwoFactorEnabled)
                                           .Ignore(c => c.LockoutEnd)
                                           .Ignore(c => c.LockoutEnabled)
                                           .Ignore(c => c.AccessFailedCount);
                                    //       .Ignore(c => c.Id);

           // builder.Entity<Client>().HasKey(c => c.userId);
        }
    }
}
