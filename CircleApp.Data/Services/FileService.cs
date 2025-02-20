using CircleApp.Data.Helpers.Enums;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CircleApp.Data.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImageAsync(IFormFile file, FileImageTypes fileImage)
        {
            string filePathUpload = fileImage switch
            {
                FileImageTypes.PostImage => Path.Combine("images", "posts"),
                FileImageTypes.StoryImage => Path.Combine("images", "stories"),
                FileImageTypes.ProfilePicture => Path.Combine("images", "profilePictures"),
                FileImageTypes.CoverImage => Path.Combine("images", "covers"),
                _ => throw new ArgumentException("Invalid file type")
            };
            if (file != null && file.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (file.ContentType.Contains("image"))
                {
                    string rootFolderPathImages = Path.Combine(rootFolderPath, filePathUpload);
                    Directory.CreateDirectory(rootFolderPathImages);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(rootFolderPathImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);

                    return $"{filePathUpload}\\{fileName}";
                }
            }
            return "";
        }
    }
}
