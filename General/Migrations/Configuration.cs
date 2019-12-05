namespace General.Migrations
{
    using General.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<General.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(General.Models.ApplicationDbContext context)
        {

            //if (!context.Roles.Any(r => r.Name == "Admin"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Admin" };
            //    var role1 = new IdentityRole { Name = "User" };

            //    manager.Create(role);
            //    manager.Create(role1);
            //}

            //if (!context.Users.Any(u => u.UserName == "483004311139639539"))
            //{
            //    var passwordHasher = new PasswordHasher();
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser
            //    {
            //        UserName = "483004311139639539"
            //        ,
            //        FirstName = "محسن",
            //        LastName = "دادخواه",
            //        FullName = "محسن دادخواه",
            //        Email = "Test@Gmail.com",
            //        MarketingCode = "ma00010000001",
            //        LeadersMarketingCode = "ma00010000001",
            //        ReagentMarketingCode = "ma00010000001",
            //       PasswordHash = passwordHasher.HashPassword("Dadkhah123456.2222!"),
            //       CooperationStatus = true,
            //    EmailConfirmed = true,
            //        LockoutEnabled = false,
            //        SecurityStamp = Guid.NewGuid().ToString()

            //    };
            //    manager.Create(user, "ChangeItAsap!");
            //    manager.AddToRole(user.Id, "Administrator");
            //    manager.AddToRole(user.Id, "Users");
            //}
            //var passwordHash = new PasswordHasher();
            //string password = passwordHash.HashPassword("Md123456!");
            //context.Users.AddOrUpdate(u => u.UserName,
            //    new ApplicationUser
            //    {
            //        UserName = "1234567891",
            //        PasswordHash = password,
            //        PhoneNumber = "12345678911",
            //        Email = "Test1@qq.com"
            //    });
            //context.Roles.AddOrUpdate(
            //    new IdentityRole { Id = "1", Name = "Administrator" }
            //    );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
