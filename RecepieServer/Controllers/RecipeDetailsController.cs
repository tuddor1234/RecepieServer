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
            new Instruction(){type = InstructionType.Text, data = "Am pus la încins o tigaie largă (antiaderentă) și am așezat carnea tocată în ea. Am lucrat la foc mare și am tot amestecat carnea pentru a o rumeni parțial. În funcție de tipul cărnii și de conținutul ei de grăsime decidem dacă este sau nu nevoie de ulei. Eu am folosit carne tocată de porc iar aceasta a avut destulă grăsime care s-a topit în timpul prăjirii. Ideea este să acordăm suficient timp acestei etape (10-12 minute) pentru ca să trecem de stadiul de „carne albită” gătită în suc propriu și să permitem bucățelelor de carne să se rumenească ușor. Caramelizarea (reacția Maillard) dă savoare preparatelor!" },
            new Instruction(){type = InstructionType.Text, data = "Am folosit 2 foi de aluat foietaj cu unt din Kaufland (a câte 230 g fiecare) pe care le-am suprapus deoarece sunt destul de subțirele. Apoi le-am așezat pe o folie de plastic (sau pe o coală de hârtie de copt) și le-am întins ușor cu sucitorul pentru a obține o foaie de cca. 25×30 cm.Am tăiat foietajul (cu rola pentru pizza) în dreptunghiuri de 5×10 cm. Am obținut 15 asemenea dreptunghiuri peste care am distribuit în mod egal umplutura de carne tocată cu ceapă călită și smântână." },
            new Instruction(){type = InstructionType.Text, data = "Separat, într-un alt castron, am amestecat cu un tel romul, gălbenușurile, coaja rasă de lămâie, vanilia și untul topit și răcorit. Am turnat totul peste făina cu maiaua din castronul mare și am frâmântat 5 minute un aluat pe care apoi l-am lăsat la dospit cca. 30 de minute la un loc cald, acoperit cu o folie de plastic." },
            new Instruction(){type = InstructionType.Text, data = "Am așezat pe foc o tigaie mai adâncă cu un strat de 2 degete de ulei. Am încins bine uleiul (la cca. 160 C) și am înmuiat o lingură metalică în el. Cu ea am rupt bucățele potrivite din aluat pe care le-am pus cu grijă în tigaie. După fiecare porție de aluat am cufundat lingura în ulei. Aceste gogoși cu lingura sau „șoricei prăjiți” trebuie să plutească în baie de ulei. Dacă uleiul este corect încins atunci gogoșelele nu se vor îmbiba cu el." },
            new Instruction(){type = InstructionType.Text, data = "Am fiert ciolanul afumat în vreo 4 L de apă. Am folosit oala sub presiune deoarece aceasta scurtează mult timpii de gătire. Din momentul în care oala a început să fâsâie am cronometrat 60 de minute. Apoi am stins focul și am depresurizat oala. Așa arată ciolanul afumat fiert. Am degresat cu polonicul zeama în care a fiert ciolanul. Se adună multă grăsime la suprafață și nu avem nevoie de ea. Zeama aromată va fi folosită la prepararea ciorbei de varză acră." },
        };



        public static void CreateRandomDetails(IStorageService service,Recipe r)
        {

            RecipeDetails details = new RecipeDetails() { recipe = r };
            Random random = new Random();
            int size = random.Next(10);
            for (int i = 0; i < size; i++)
            {
                Instruction instr = new Instruction() { type = (InstructionType) random.Next(2) };
           
                if(instr.type != InstructionType.Image)
                {
                   instr.data = randomDetails[random.Next(8)].data;
                   
                }
                else
                {
                    var index = random.Next(5);
                    instr.data = index.ToString() + ".jpg";
                    FileStream file = File.OpenRead($"Resources/Images/Instructions/{instr.data}");
                    service.UploadPicture(file, r.ID, instr.data);
                    file.Close();      
                }

                details.Instructions.Add(instr);
            }

            Save(service, details);
        }
    }

    public partial class RecipeDetailsController
    {
        public static void Save(IStorageService storageService, RecipeDetails details)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RecipeDetails));

            Stream stream = new MemoryStream();
            serializer.Serialize(stream, details);

            stream.Position = 0;
            storageService.UploadRecipe(stream, details.recipe.ID);
            stream.Dispose();
        }

        public static RecipeDetails Load(IStorageService service, Recipe recipe)
        {
            Stream stream = service.DownloadRecipe(recipe.ID);
            XmlSerializer serializer = new XmlSerializer(typeof(RecipeDetails));
            RecipeDetails rd = (RecipeDetails) serializer.Deserialize(stream);
            return rd;
        }
    }
}
