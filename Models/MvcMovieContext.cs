using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using core_sec.Models;
using IssueManager.Models;
namespace IssueManager.Models

{
    public class IssueManagerContext : IdentityDbContext<ApplicationUser>
    {
        public IssueManagerContext (DbContextOptions<IssueManagerContext> options)
            : base(options)
        {
        }

        public DbSet<IssueManager.Models.Movie> Movie { get; set; }

        public DbSet<IssueManager.Models.Artist> Artist { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

 

         public DbSet<IssueManager.Models.IssueType> IssueType { get; set; }

         public DbSet<IssueManager.Models.Issue> Issue { get; set; }

         public DbSet<IssueManager.Models.Config> Config { get; set; }

         public DbSet<core_sec.Models.ApplicationUser> ApplicationUser { get; set; }



    }

}