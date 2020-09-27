using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSphere.Models.Identity;

namespace VSphere.Context
{
    public class DataInitializer
    {
        public static void SeedData(UserManager<ApplicationIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AddRolesData(roleManager);
            AddUserData(userManager, roleManager);
        }

        private static void AddRolesData(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();

            var rolesToAdd = new List<string>();
            rolesToAdd.Add("Admin");
            rolesToAdd.Add("DataStore");
            rolesToAdd.Add("Host");
            rolesToAdd.Add("Server");
            rolesToAdd.Add("VM");
            rolesToAdd.Add("Role");
            rolesToAdd.Add("Manager");

            foreach (var item in rolesToAdd)
            {
                if (roles.FirstOrDefault(x => x.Name == item) == null)
                {
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = item.ToString(),
                        NormalizedName = item.ToString().ToUpper()
                    });
                }
            }
        }

        private static void AddUserData(UserManager<ApplicationIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();

            if (userManager.FindByEmailAsync("admin@vshere.com").Result == null)
            {
                var user = new ApplicationIdentityUser();
                user.Email = "admin@vshere.com";
                user.UserName = "admin@vshere.com";
                user.NormalizedEmail = "admin@vshere.com".ToUpper();
                user.EmailConfirmed = true;
                user.NormalizedUserName = "admin@vshere.com".ToUpper();
                user.FullName = "Admin";
                user.Insert = DateTime.Now;

                var result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
