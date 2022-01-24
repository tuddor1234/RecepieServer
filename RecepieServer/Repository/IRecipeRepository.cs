using Microsoft.AspNetCore.Http;
using RecepieServer.Controllers;
using RecepieServer.Models;
using RecepieServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace RecepieServer.Repository
{
    public interface IRecipeRepository
    {
        Recipe GetRecipeByID(long id);

        IEnumerable<Recipe> GetAllRecipes();

        RecipeDetails GetRecipeDetails(Recipe recipe);

        Task<byte[]> GetResourceForRecipe(long id, string resource);
    }

    public class RecipeRepo : IRecipeRepository
    {
        RecipeSerializer Controller;
        private readonly IStorageService _storageService;

        List<Recipe> Recipes => Controller.Recipes;
        public RecipeRepo(IStorageService storageService)
        {
            Controller = new RecipeSerializer(storageService);
            _storageService = storageService;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return Recipes;
        }

        public Recipe GetRecipeByID(long id)
        {
            return Recipes.Where(item => item.ID == id).SingleOrDefault();
        }

        public RecipeDetails GetRecipeDetails(Recipe recipe)
        {
            return RecipeDetailsController.Load(_storageService, recipe);
        }

        public void PostRecipe()
        {

        }

   
        public Task<byte[]> GetResourceForRecipe(long id, string resource)
        {
            return RecipeDetailsController.GetResource(id, resource);
        }


    }
}
