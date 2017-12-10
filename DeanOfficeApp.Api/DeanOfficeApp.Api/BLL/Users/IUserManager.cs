using DeanOfficeApp.Api.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace DeanOfficeApp.Api.BLL.Users
{
    public interface IUserManager
    {
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<ApplicationUser> FindByIdAsync(int userId);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(int id, string role);
    }
}