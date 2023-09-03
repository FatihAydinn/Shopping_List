using Shopping_List.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Shopping_List.Data
{
    public class IdentityDbContext : IdentityDbContext<CustomUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var customUser = new CustomUser
            {
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Name = "Admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Role = "Admin"
            };

            PasswordHasher<CustomUser> ph = new PasswordHasher<CustomUser>();
            customUser.PasswordHash = ph.HashPassword(customUser, "Admin123*");

            builder.Entity<CustomUser>().HasData(customUser);
        }
    }   
}