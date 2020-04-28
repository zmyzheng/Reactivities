using Application.Interfaces;
using Application.Interfaces.Photos;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        public PhotoUploadResult AddPhoto(IFormFile file)
        {
            throw new System.NotImplementedException();
        }

        public string DeletePhoto(string publicId)
        {
            throw new System.NotImplementedException();
        }
    }
}