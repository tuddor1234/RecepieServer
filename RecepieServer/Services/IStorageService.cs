using Microsoft.AspNetCore.Http;
using System.IO;

namespace RecepieServer.Services
{
    public interface IStorageService
    {
        public void UploadRecipe(Stream file, long id = long.MaxValue);
        public void UploadPicture(Stream file, long id, string fileName);
        public void Upload(IFormFile file, long id = long.MaxValue);

        public Stream DownloadRecipe(long id = long.MaxValue);
        void Delete(long id);
    }
}