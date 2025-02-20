using CircleApp.Data;
using CircleApp.Data.Helpers.Enums;
using CircleApp.Data.Models;
using CircleApp.Data.Services;
using CircleApp.ViewModels.Stories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoriesService _storiesService;
        private readonly IFileService _fileService;

        public StoriesController(IStoriesService storiesService, IFileService fileService)
        {
            _storiesService = storiesService;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStory(StoryVM storyVM)
        {
            int loggedInUserId = 1;

            var imageUploadPath = await _fileService.UploadImageAsync(storyVM.Image, FileImageTypes.StoryImage);

            var newStory = new Story
            {
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
                ImageUrl = imageUploadPath,
                UserId = loggedInUserId
            };
            await _storiesService.CreateStoryAsync(newStory);

            

            return RedirectToAction("Index", "Home");
        }
    }
}
