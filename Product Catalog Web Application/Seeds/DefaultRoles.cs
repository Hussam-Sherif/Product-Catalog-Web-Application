namespace Bookify.Web.Seeds
{
    public class DefaultRoles 
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
               await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));

            }
        }
    }

    public static class AppRoles
    {
        public const string Admin = "Admin";

    }
}
