﻿using Microsoft.AspNetCore.Hosting;
using RecepieServer.Models;
using RecepieServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.Serialization;
namespace RecepieServer.Controllers
{
    //HERE GOES ALL THE RANDOM STUFF THAT WE SHOULD NOT NEED
    public partial class RecipeSerializer
    {
        string[] names = { "Tort", "Tort Fancy", "Tarta", "Ceva cu fructe", "Sa fac spume daca stiu" };
        void CreateRadomRecipes()
        {
            Recipes.Clear();
            foreach (var name in names)
            {
                var NewRecipe = new Recipe(name);
                var value = new Random().Next(3);
                string img = value == 0 ? "default.jpg" : $"{value}.jpg";
                NewRecipe.Thumbnail = img;

                Recipes.Add(NewRecipe);
                RecipeDetailsController.CreateRandomDetails(_storageService , NewRecipe);

                _storageService.Delete(NewRecipe.ID);

                FileStream thumbnail = File.OpenRead($"Resources/Images/Thumbnails/{NewRecipe.Thumbnail}");
                thumbnail.Position = 0;
                _storageService.UploadPicture(thumbnail, NewRecipe.ID, NewRecipe.Thumbnail);
                thumbnail.Close();
            }

            Stream recipe = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            serializer.Serialize(recipe, Recipes);

            recipe.Position = 0;
            _storageService.UploadRecipe(recipe);

            recipe.Dispose();
        }

    }

    public partial class RecipeSerializer
    {
        private readonly IStorageService _storageService;
        [XmlArray]
        public List<Recipe> Recipes = new List<Recipe>();

        public RecipeSerializer(IStorageService storageService)
        {
            _storageService = storageService;
            LoadRecipesFromDisk();
        }

        void LoadRecipesFromDisk()
        {
            Stream stream = _storageService.DownloadRecipe();
            if(stream == null)
            {
                CreateRadomRecipes();
                stream = _storageService.DownloadRecipe();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            Recipes = (List<Recipe>)serializer.Deserialize(stream);
        }
    }
}
