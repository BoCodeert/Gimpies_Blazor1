using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Gimpies_Blazor1.Database.Models.Entities;

namespace Gimpies_Blazor1.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Roldata (Admin, Buyer, Seller)
            var roles = new[]
            {
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Buyer" },
                new Role { RoleId = 3, RoleName = "Seller" }
             };
            modelBuilder.Entity<Role>().HasData(roles);

            var demoUser = new[]
            {
                new User { Userid = 1, Username = "admin", PasswordHashed = "1", fk_UserRoleID = 1},
                new User { Userid = 2, Username = "buyer", PasswordHashed = "1", fk_UserRoleID = 2},
                new User { Userid = 3, Username = "seller", PasswordHashed="1", fk_UserRoleID = 3}
            };
            modelBuilder.Entity<User>().HasData(demoUser);

            // Policies
            var policies = new[]
            {
        // Admin policies
        new UserPolicy { Id = 1, fk_UserRoleID = 1, PolicyName = UserPolicy.View_Shoes, IsEnabled = true },
        new UserPolicy { Id = 2, fk_UserRoleID = 1, PolicyName = UserPolicy.Add_Shoes, IsEnabled = true },
        new UserPolicy { Id = 3, fk_UserRoleID = 1, PolicyName = UserPolicy.Edit_Shoes, IsEnabled = true },
        new UserPolicy { Id = 4, fk_UserRoleID = 1, PolicyName = UserPolicy.Delete_Shoes, IsEnabled = true },
        new UserPolicy { Id = 5, fk_UserRoleID = 1, PolicyName = UserPolicy.View_Users, IsEnabled = true },
        new UserPolicy { Id = 6, fk_UserRoleID = 1, PolicyName = UserPolicy.Add_Users, IsEnabled = true },
        new UserPolicy { Id = 7, fk_UserRoleID = 1, PolicyName = UserPolicy.Edit_Users, IsEnabled = true },
        new UserPolicy { Id = 8, fk_UserRoleID = 1, PolicyName = UserPolicy.Delete_Users, IsEnabled = true },



        // Buyer policies
        new UserPolicy { Id = 9, fk_UserRoleID = 2, PolicyName = UserPolicy.View_Shoes, IsEnabled = true },
        new UserPolicy { Id = 10, fk_UserRoleID = 2, PolicyName = UserPolicy.Buy_Shoes, IsEnabled = true },

        // Seller policies
        new UserPolicy { Id = 11, fk_UserRoleID = 3, PolicyName = UserPolicy.View_Shoes, IsEnabled = true },
        new UserPolicy { Id = 12, fk_UserRoleID = 3, PolicyName = UserPolicy.Sell_Shoes, IsEnabled = true }
    };
            modelBuilder.Entity<UserPolicy>().HasData(policies);

            // Configureren van relaties
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.fk_UserRoleID);

            modelBuilder.Entity<UserPolicy>()
                .HasOne(up => up.Role)
                .WithMany(r => r.Policies)
                .HasForeignKey(up => up.fk_UserRoleID);
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<UserPolicy> UserPolicies { get; set; }

    }
}
