using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecepieServer.Services
{
    class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public static StorageService Instance;

        public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;

            Instance = this;
        }

        //public void Upload(FileStream file,long id)
        //{
        //    var containerName = _configuration.GetSection("Storage:ContainerName").Value;

        //    string path = 
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(containerName).;
        //    BlobClient blobClient;
        //    if (id != long.MaxValue)
        //    {
        //        blobClient = containerClient.GetBlobClient($"Recipes/{id}/{file.Name}");
        //    }
        //    else
        //    {
        //        blobClient = containerClient.GetBlobClient($"Recipes");
        //    }
     
        //    blobClient.Upload(file,true);
        //}
        public void UploadRecipe(FileStream file,long id)
        {
            var connectionString = _configuration.GetSection("Storage:ConnectionString").Value;
            var blobContainerName = _configuration.GetSection("Storage:ContainerName").Value;

            string path = "Recipes";
            if(id != long.MaxValue)
            {
                path += $"/{id}/recipe.xml";
            }
            else
            {
                path += "/allRecipes.xml";
            }
            
            BlobClient blobClient = new BlobClient(connectionString,blobContainerName,path);

            // upload the file
            blobClient.Upload(file,true);

        }

        public void Upload(IFormFile file ,long id)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient;
            if (id != long.MaxValue)
            {
                blobClient = containerClient.GetBlobClient($"Recipes/{id}/{file.Name}");
            }
            else
            {
                blobClient = containerClient.GetBlobClient($"Recipes");
            }

            Stream stream = file.OpenReadStream();
            blobClient.Upload(stream, true);
        }

        public void UploadPicture(FileStream file, long id, string fileName)
        {
            var connectionString = _configuration.GetSection("Storage:ConnectionString").Value;
            var blobContainerName = _configuration.GetSection("Storage:ContainerName").Value;

            string path = $"Recipes/{id}/{fileName}";
  
            BlobClient blobClient = new BlobClient(connectionString, blobContainerName, path);
            blobClient.Upload(file, true);
        }

        public Stream DownloadRecipe(long id = long.MaxValue)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            string path = "Recipes/";
            if(id == long.MaxValue)
            {
                path += "allRecipes.xml";
            }
            else
            {
                path += $"{id}/recipe.xml";
            }
            var blobClient = containerClient.GetBlobClient(path);
            if (!blobClient.Exists())
                return null;

            return blobClient.DownloadContent().Value.Content.ToStream();
        }
    }
}
