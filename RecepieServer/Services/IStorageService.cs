using Microsoft.AspNetCore.Http;
using System.IO;

namespace RecepieServer.Services
{
    public interface IStorageService
    {
        public void UploadRecipe( FileStream file, long id = long.MaxValue);
        public void UploadPicture( FileStream file, long id, string fileName);
        public void Upload(IFormFile file, long id = long.MaxValue);

        public Stream DownloadRecipe(long id = long.MaxValue);
    }
}