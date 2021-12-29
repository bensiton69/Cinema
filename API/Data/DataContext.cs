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
        public DbSet<Reservation> Reservations { get; set; }
        
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

            //builder.Entity<Movie>()
            //    .HasMany(m => m.ShowingIn);

            //builder.Entity<Venue>()
            //    .HasMany(v => v.AvailableSeats)
            //    .WithOne();
            //builder.Entity<Venue>()
            //    .HasMany(v => v.UnavailableSeats)
            //    .WithOne();
            //builder.Entity<Venue>()
            //    .HasMany(v => v.HandicappedSeats)
            //    .WithOne();

            //builder.Entity<Venue>()
            //    .Navigation(v => v.AvailableSeats)
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);
            //builder.Entity<Venue>()
            //    .Navigation(v => v.HandicappedSeats)
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);
            //builder.Entity<Venue>()
            //    .Navigation(v => v.UnavailableSeats)
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Entity<AdminUser>();
            builder.Entity<CostumerUser>();
        }
    }

}
