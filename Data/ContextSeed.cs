using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<Models.User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.ProjectManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Developer.ToString()));
        }
    }
}
