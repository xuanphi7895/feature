using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if (!userManager.Users.Any()){
                var user = new AppUser
                {
                    DisplayName = "Phi",
                    Email = "xuanphi7895@gmail.com",
                    UserName = "xuanphi@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Phi",
                        LastName = "Tran Xuan",
                        Street = "Xom 14, Lang Dong Mieu",
                        City = "Hue",
                        State = "HUE",
                        ZipCode = "50000"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}