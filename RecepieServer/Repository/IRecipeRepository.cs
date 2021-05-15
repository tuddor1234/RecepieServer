using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace RecepieServer.Repository
{
    public interface IRecipeRepository
    {
        Recipe GetRecipeByID(Guid id);

        IEnumerable<Recipe> GetAllRecipes();
    }

    public class RecipeRepo : IRecipeRepository
    {
        private readonly List<Recipe> recipes = new List<Recipe>();

        string[] names = { "Tort", "Tort Fancy", "Tarta", "Ceva cu fructe", "Sa fac spume daca stiu" };
        
        public RecipeRepo()
        {
            CreateSomeRandomRecipes();
        }
         
       
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return recipes;
        }

        public Recipe GetRecipeByID(Guid id)
        {
            Recipe returnedRecipe = null;
            foreach (var r in recipes)
            {
                if (r.ID == id)
                { 
                    returnedRecipe = r; 
                    break;
                }
            }

            return returnedRecipe;
        }


        public void PostRecipe()
        {

        }




        void CreateSomeRandomRecipes()
        {

            foreach (var name in names)
            {
                recipes.Add(new Recipe() { Name = name });
            }
        }
    }
}
