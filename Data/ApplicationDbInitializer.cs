using assignment_4.Models;
using Microsoft.AspNetCore.Identity;

namespace assignment_4.Data
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            // Create roles
            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();

            // Create users
            var admin = new ApplicationUser { UserName = "admin@uia.no", Email = "admin@uia.no", Nickname = "Admin"};
            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();

            var user = new ApplicationUser { UserName = "user@uia.no", Email = "user@uia.no", Nickname = "User"};
            um.CreateAsync(user, "Password1.").Wait();


            // Finally save user and role changes to the database
            db.SaveChanges();
          


        }
    }
}

