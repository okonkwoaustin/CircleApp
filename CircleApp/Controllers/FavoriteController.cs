using CircleApp.Controllers.Base;
using CircleApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    [Authorize]
    public class FavoriteController : BaseController
    {
        private readonly IPostService _postService;

        public FavoriteController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = GetUserId();
            if (loggedInUserId == null) return RedirectToLogin();
            var myFavoritePosts = await _postService.GetAllFavoritedPostsAsync(loggedInUserId.Value);

            return View(myFavoritePosts);
        }
    }
}
