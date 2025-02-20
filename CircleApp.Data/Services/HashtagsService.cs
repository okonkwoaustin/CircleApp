

using CircleApp.Data.Helpers;
using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Data.Services
{
    public class HashtagsService : IHashtagsService
    {
        private readonly AppDbContext _context;

        public HashtagsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ProcessHashtagsForNewPostAsync(string content)
        {
            //Find and store hashtags
            var postHashtags = HashtagsHelper.GetHashtags(content);
            foreach (var hashTag in postHashtags)
            {
                var hashtagDb = await _context.HashTags.FirstOrDefaultAsync(n => n.Name == hashTag);
                if (hashtagDb != null)
                {
                    hashtagDb.Count += 1;
                    hashtagDb.DateUpdated = DateTime.UtcNow;

                    _context.HashTags.Update(hashtagDb);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newHashtag = new HashTag()
                    {
                        Name = hashTag,
                        Count = 1,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };
                    await _context.HashTags.AddAsync(newHashtag);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task ProcessHashtagsForRemovedPostAsync(string content)
        {
            var postHashtags = HashtagsHelper.GetHashtags(content);
            foreach (var hashtag in postHashtags)
            {
                var hashtagDb = await _context.HashTags.FirstOrDefaultAsync(n => n.Name == hashtag);
                if (hashtagDb != null)
                {
                    hashtagDb.Count -= 1;
                    hashtagDb.DateUpdated = DateTime.UtcNow;

                    _context.HashTags.Update(hashtagDb);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
