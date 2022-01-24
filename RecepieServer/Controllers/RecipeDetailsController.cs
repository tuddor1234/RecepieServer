using RecepieServer.Models;
using RecepieServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RecepieServer.Controllers
{
    public partial class RecipeDetailsController
    {
        static List<Instruction> randomDetails = new List<Instruction>()
        {
            new Instruction(){type = InstructionType.Text, data = "Beat some eggs" },
            new Instruction(){type = InstructionType.Text, data = "Neat the dough" },
            new Instruction(){type = InstructionType.Text, data = "Combine all ingridients into a big bowl and stir it up." },
            new Instruction(){type = InstructionType.Image, data = "" },
            new Instruction(){type = InstructionType.Image, data = "" } ,
            new Instruction(){type = InstructionType.Image, data = "" }
        };



        public static void CreateRandomDetails(IStorageService service,Recipe r)
        {

            RecipeDetails details = new RecipeDetails() { recipe = r };
            Random random = new Random();
            int size = random.Next(5);
            for (int i = 0; i < size; i++)
            {
                var instruction = randomDetails[random.Next(2,6)];
                if(instruction.type == InstructionType.Image)
                {
                    var index = random.Next(5);
                    instruction.data = index.ToString() + ".jpg";
                    FileStream file = File.OpenRead($"Resources/Images/Instructions/{instruction.data}");
                    service.UploadPicture(file, r.ID, instruction.data);
                    file.Close();      
                }
                details.Instructions.Add(instruction);
            }

            Save(service, details);
        }
    }

    public partial class RecipeDetailsController
    {
        public static void Save(IStorageService storageService, RecipeDetails details)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RecipeDetails));

            var DirPath = $"Recipes/{details.recipe.ID}";
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }

            var FilePath = DirPath + "/recipe.xml";
            FileStream file = File.Open(FilePath, FileMode.OpenOrCreate);
            serializer.Serialize(file, details);

            file.Position = 0;
            storageService.UploadRecipe(file, details.recipe.ID);
            file.Close();
        }

        public static RecipeDetails Load(IStorageService service, Recipe recipe)
        {
            Stream stream = service.DownloadRecipe(recipe.ID);
            XmlSerializer serializer = new XmlSerializer(typeof(RecipeDetails));
            RecipeDetails rd = (RecipeDetails) serializer.Deserialize(stream);
            return rd;
        }

        public static Task<byte[]> GetResource(long id, string resource)
        {
            var DirPath = $"Recipes/{id}/Images/";
            if (!Directory.Exists(DirPath))
            {
                return null;
            }

            var FilePath = DirPath + $"{resource}";
            if (!File.Exists(FilePath))
            {
                return null;
            }

            return File.ReadAllBytesAsync(FilePath);
        }
    }
}
