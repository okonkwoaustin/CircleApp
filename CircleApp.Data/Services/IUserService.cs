using CircleApp.Data.Models;

namespace CircleApp.Data.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int loggedInUserId);
        Task UpdateUserProfilePicture(int loggedInUserId, string profilePictureUrl);
    }
}
