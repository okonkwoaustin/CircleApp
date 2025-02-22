using CircleApp.Data.Helpers.Enums;
using CircleApp.Data.Services;
using CircleApp.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public SettingsController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            int loggedInUserId = 1;
            var userDb = await _userService.GetUser(loggedInUserId);
            return View(userDb);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(ProfilePictureVM profilePictureVM)
        {
            var loggedInUser = 1;
            var uploadedProfilePictureUrl = await _fileService.UploadImageAsync(profilePictureVM.ProfilePictureImage, FileImageTypes.ProfilePicture);

            await _userService.UpdateUserProfilePicture(loggedInUser, uploadedProfilePictureUrl);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileVM profileVM)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordVM updatePasswordVM)
        {
            return RedirectToAction("Index");
        }
    }
}
