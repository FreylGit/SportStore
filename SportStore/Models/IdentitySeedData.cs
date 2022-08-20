using Microsoft.AspNetCore.Identity;

namespace SportStore.Models
{
    public class IdentitySeedData
    {
        private const string _adminUser = "Admin";
        private const string _adminPassword = "Secret123$";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                userManager = (UserManager<IdentityUser>) scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
                IdentityUser user = await userManager.FindByIdAsync(_adminUser);
                if (user == null)
                {
                    user = new IdentityUser("Admin");
                    await userManager.CreateAsync(user, _adminPassword);
                }
            }
           

            
        }
    }
}
