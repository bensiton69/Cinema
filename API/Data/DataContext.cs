using System;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Guid
        , IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>
        , IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ShowTime> ShowTime { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Seat> Seats{ get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            builder.Entity<Venue>();

            builder.Entity<AdminUser>();
            builder.Entity<CostumerUser>();
        }

    }

}
