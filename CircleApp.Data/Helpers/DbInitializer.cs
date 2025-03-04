

using CircleApp.Data.Helpers.Constants;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CircleApp.Data.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedUsersAndRoles(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            //Roles
            if (!roleManager.Roles.Any())
            {
                foreach(var roleName in AppRoles.All)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                    }
                }
            }

            //Users with roles
            if(!userManager.Users.Any(n => !string.IsNullOrEmpty(n.Email)))
            {
                var userPassword = "CodeSocial123?";
                var newUser = new User()
                {
                    UserName = "jane.jane",
                    Email = "jane@gmail.com",
                    FullName = "Jane Okonkwo",
                    ProfilePictureUrl = "https://img.freepik.com/premium-photo/portrait-girl-closeup-girl-brown-background-beautiful-look_215924-2096.jpg",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, userPassword);
                if (result.Succeeded) await userManager.AddToRoleAsync(newUser, AppRoles.User);


                var newAdmin = new User()
                {
                    UserName = "admin.admin",
                    Email = "admin@gmail.com",
                    FullName = "Jane Admin",
                    ProfilePictureUrl = "https://img.freepik.com/premium-photo/portrait-girl-closeup-girl-brown-background-beautiful-look_215924-2096.jpg",
                    EmailConfirmed = true
                };

                var resultNewAdmin = await userManager.CreateAsync(newAdmin, userPassword);
                if (resultNewAdmin.Succeeded) await userManager.AddToRoleAsync(newAdmin, AppRoles.Admin);
            }
        }
        public static async Task SeedAsync(AppDbContext appDbContext)
        {
            //if (!appDbContext.Users.Any() && !appDbContext.Posts.Any())
            //{
            //    var newUser = new User()
            //    {
            //        FullName = "Okonkwo Jane",
            //        ProfilePictureUrl = "https://img.freepik.com/premium-photo/portrait-girl-closeup-girl-brown-background-beautiful-look_215924-2096.jpg"
            //    };
            //    await appDbContext.Users.AddAsync(newUser);
            //    await appDbContext.SaveChangesAsync();

            //    var newPostWithoutImage = new Post()
            //    {
            //        Content = "This is going to be our first post which is being loaded from the database and it has been created using our test user.",
            //        ImageUrl = "",
            //        NrOfReports = 0,
            //        DateCreated = DateTime.UtcNow,
            //        DateUpdated = DateTime.UtcNow,

            //        UserId = newUser.Id
            //    };

            //    var newPostWithImage = new Post()
            //    {
            //        Content = "This is going to be our first post which is being loaded from the database and it has been created using our test user. This post has an image",
            //        ImageUrl = "https://unsplash.com/photos/foggy-mountain-summit-1Z2niiBPg5A",
            //        NrOfReports = 0,
            //        DateCreated = DateTime.UtcNow,
            //        DateUpdated = DateTime.UtcNow,

            //        UserId = newUser.Id
            //    };

                //await appDbContext.Posts.AddRangeAsync(newPostWithoutImage, newPostWithImage);
                //await appDbContext.SaveChangesAsync();
            //}
        }
    }
}
