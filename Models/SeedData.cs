using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using core_sec;
using core_sec.Models;
using Microsoft.AspNetCore.Http;

namespace IssueManager.Models
{
    public static class SeedData
    {
        
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new IssueManagerContext(
            serviceProvider.GetRequiredService<DbContextOptions<IssueManagerContext>>()))
            {
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                IdentityResult IR = null;

                if (!await roleManager.RoleExistsAsync("AdminRole"))
                {
                    IR = await roleManager.CreateAsync(new IdentityRole("AdminRole"));
                }

               

                if (!await roleManager.RoleExistsAsync("UserRole"))
                {
                    IR = await roleManager.CreateAsync(new IdentityRole("UserRole"));
                }           
               context.SaveChanges();

            }
            /*using (var context = new IssueManagerContext(
            serviceProvider.GetRequiredService<DbContextOptions<IssueManagerContext>>()))
            {

                

                var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
                

                    IdentityUser user = new IdentityUser();
                    user.UserName = "AdminAdmin";
                    user.Email = "AdminAdmin@gmail.com";

                    IdentityUser user1 = new IdentityUser();
                    user1.UserName = "UserUser";
                    user1.Email = "UserUser@gmail.com";
                    
                    IdentityResult result = null;
                    result =  userManager.CreateAsync
                    (user, user.UserName).Result;
            
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user,
                        "AdminRole").Wait();
                    }
                    
                    IdentityResult result1 = null;
                    result1 =  userManager.CreateAsync
                    (user1, user1.UserName).Result;

                    if (result1.Succeeded)
                    {
                        userManager.AddToRoleAsync(user1,
                                            "UserRole").Wait();
                    }
                    
                    var user_admin = await userManager.FindByNameAsync("AdminAdmin123@gmail.com");
                    var user_User = await userManager.FindByNameAsync("UserUser123@gmail.com");
                    await userManager.AddToRoleAsync(user_admin, "AdminRole");
                    await userManager.AddToRoleAsync(user_User, "UserRole");
                
                    context.SaveChanges();
                
            }
            */
            using (var context = new IssueManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<IssueManagerContext>>()))
            {
                // Look for any movies.
                if (context.Artist.Any())
                {
                    return;   // DB has been seeded
                }

                context.Artist.AddRange(
                     new Artist
                     {
                         Name = "Rob",
                         Surname = "Reiner"
                     },
                     new Artist
                     {
                         Name = "Ivan",
                         Surname = "Reitman"
                     },
                     new Artist
                     {
                         Name = "Howard",
                         Surname = "Hawks"
                     } 
                );
                context.SaveChanges();
            }

            using (var context = new IssueManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<IssueManagerContext>>()))
            {
                // Look for any movies.
                if (context.IssueType.Any())
                {
                    return;   // DB has been seeded
                }

                context.IssueType.AddRange(
                    new IssueType
                     {
                         Name = "Another"
                     },
                     new IssueType
                     {
                         Name = "Network"
                     },
                     new IssueType
                     {
                         Name = "Laptop"
                     },
                     new IssueType
                     {
                         Name = "Screen"
                     } 
                );
                context.SaveChanges();
            }

            using (var context = new IssueManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<IssueManagerContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                     new Movie
                     {
                         Title = "When Harry Met Sally",
                         ReleaseDate = DateTime.Parse("1989-1-11"),
                         Genre = "Romantic Comedy",
                         Price = 7.99M,
                         ArtistId = 1,
                         Description = "Harry i Sally poznają się w czasach studenckich, ale nie pałają do siebie zbytnią sympatią. Spotkanie po latach daje jednak początek przyjaźni."
                     },

                     new Movie
                     {
                         Title = "Ghostbusters",
                         ReleaseDate = DateTime.Parse("1984-3-13"),
                         Genre = "Comedy",
                         Price = 8.99M,
                         ArtistId = 2,
                         Description = "Trzech doktorów parapsychologii zakłada firmę zajmującą się zwalczaniem duchów. Wkrótce będą musieli zmierzyć się z pradawną złą mocą opanowującą miasto."

                     },

                     new Movie
                     {
                         Title = "Ghostbusters 2",
                         ReleaseDate = DateTime.Parse("1986-2-23"),
                         Genre = "Comedy",
                         Price = 9.99M,
                         ArtistId = 2,
                         Description = "Pogromcy duchów próbują przeszkodzić rumuńskiemu czarnoksiężnikowi w powrocie z zaświatów. "
                     },

                   new Movie
                   {
                       Title = "Rio Bravo",
                       ReleaseDate = DateTime.Parse("1959-4-15"),
                       Genre = "Western",
                       Price = 3.99M,
                       ArtistId = 3,
                       Description = "Szeryf usiłuje zapobiec wydostaniu się z więzienia znanego mordercy."
                   }
                );
                context.SaveChanges();
            }
        }           
    }
}