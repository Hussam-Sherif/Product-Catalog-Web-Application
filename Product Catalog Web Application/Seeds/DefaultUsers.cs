namespace Bookify.Web.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedAdminUser(UserManager<IdentityUser> userManager)
        {
            IdentityUser Admin = new()
            {
                UserName = "Admin",
                Email = "Admin@PC.com",
                EmailConfirmed = true,
            };
            var user= await userManager.FindByEmailAsync(Admin.Email);
            if(user is null)
            {
                await userManager.CreateAsync(Admin, "Admin@123");
               await userManager.AddToRoleAsync(Admin, AppRoles.Admin);


            }
           
        }
    }
}
