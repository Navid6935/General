    using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace General.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int Counter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FathersName { get; set; }
        public string BirthCertificateId { get; set; }
        public string NationalCode { get; set; }
         public int? DayofBirth { get; set; }
          public string MounthofBirth { get; set; }
         public string YearofBirth { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string MobileNumber { get; set; }
        public string AcountNumber { get; set; }
        public string CardNumber { get; set; }
        public string BranchName { get; set; }
        public int BranchCode { get; set; }
        public string ShabaName { get; set; }
        public string InsuranceNumber1 { get; set; }
        public string InsuranceNumber2 { get; set; }
        public string InsuranceNumber3 { get; set; }
        public string InsuranceNumber4 { get; set; }
        public string InsuranceNumber5 { get; set; }
        public string InsuranceNumber6 { get; set; }
        public string Password2 { get; set; }
        public string ConfirmPassword2 { get; set; }
        public string MarketingCode { get; set; }
        public string LeadersMarketingCode { get; set; }
        public string ReagentMarketingCode { get; set; }

        public bool CooperationStatus { get; set; }
        public int RegisterStatus { get; set; }

        public string DisconnectCooperationDate { get; set; }
      
        public string DisconnectCooperationCause { get; set; }
        //public int? Level { get; set; }
        //public int? ArmsNum { get; set; }
        public int? CId { get; set; }
        public virtual Areas.BasicInformation.Models.City CName { get; set; }

        public int? SId { get; set; }
        public virtual Areas.BasicInformation.Models.State SName { get; set; }


        public int? EId { get; set; }
        public virtual Areas.BasicInformation.Models.Education EName { get; set; }

        //public int Gmail { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        static ApplicationDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
        }

        public System.Data.Entity.DbSet<General.Areas.BasicInformation.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<General.Areas.BasicInformation.Models.Education> Educations { get; set; }

        public System.Data.Entity.DbSet<General.Areas.BasicInformation.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.Installment> Installments { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.Percentage> Percentages { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.AllArmsAndIncome> AllArmsAndIncomes { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.UsersArms> UsersArms { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Users.Models.UsersWallet> UsersWallets { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.Arms> Arms { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Users.Models.Support> Supports { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.EducationalPamphlets> EducationalPamphlets { get; set; }

        public System.Data.Entity.DbSet<General.Areas.Administrator.Models.Messages> Messages { get; set; }
    }
}