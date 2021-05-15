using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace RecepieServer.Repository
{
    public interface IRecipeRepository
    {
        Recipe GetRecipeByID(long id);

        IEnumerable<Recipe> GetAllRecipes();
    }

    public class RecipeRepo : IRecipeRepository
    {
        private  List<Recipe> recipes = new List<Recipe>();

        string[] names = { "Tort", "Tort Fancy", "Tarta", "Ceva cu fructe", "Sa fac spume daca stiu" };
        
        public RecipeRepo()
        {
            PopulateRecipeList();
        }
         
       
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return recipes;
        }

        public Recipe GetRecipeByID(long id)
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

        void PopulateRecipeList()
        { 
            string path = Directory.GetCurrentDirectory();
            string xmlPath = Path.Combine(path, "Recipes","AllRecipes.xml");
            if (File.Exists(xmlPath))
            {
                recipes = new List<Recipe>();
                XDocument doc = XDocument.Load(xmlPath);
                foreach (var recipe in doc.Descendants("DocumentElement").Descendants("Recipe"))  
                {
                    var ID = Convert.ToInt64(recipe.Element("ID").Value);
                    var name = recipe.Element("Name").Value;

                    Recipe r = new Recipe(ID,name);
                    recipes.Add(r);
                }
            }
            else
            {
                CreateSomeRandomRecipes();
                XDocument doc = new XDocument(
                    new XComment("This doc contains all the recipies by Name & ID"),
                    new XElement("DocumentElement"));

                foreach(var r in recipes)
                {
                    doc.Root.Add(new XElement("Recipe" ,new XElement("Name", r.Name), new XElement("ID", r.ID)) );
                }

                doc.Save(xmlPath);

            }
            
            

        }
    }
}
