using CircleApp.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CircleApp.Data.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostAsync(int loggedInUserId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> RemovePostAsync(int postId);

        Task AddPostCommentAsync(Comment comment);
        Task RemovePostCommentAsync(int commentId);

        Task TogglePostLikeAsync(int postId, int userId);
        Task TogglePostFavoriteAsync(int postId, int userId);
        Task TogglePostVisibilityAsync(int postId, int userId);
        Task ReportPostAsync(int postId, int userId);
    }
}
