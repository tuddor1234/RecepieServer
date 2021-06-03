using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RecepieServer
{ 
    public class Recipe
    {
        static long idCounter =  0;
        public Recipe()
        {
            ID = idCounter++;
            CreateNewXMLFile();
            // @TODO update the file
        }

        public Recipe(long id, string name)
        {
            ID = id;
            Name = name;
            CreateNewXMLFile();
        }

        public string Name { get; set; }
        public long ID { get; set; }

        void CreateNewXMLFile()
        {
            string currentPath = $"Recipes/{ID}";
            System.IO.Directory.CreateDirectory(currentPath);
            XDocument doc;

            //{ "Tort", "Tort Fancy", "Tarta", "Ceva cu fructe", "Sa fac spume daca stiu" };
            switch (ID)
            {
                case 0:
                    doc = new XDocument(
               new XComment("This is Tort"),
               new XElement("Recipe",
               new XElement("Text", "Mergem la LIDL"),
               new XElement("Image", "data:image/jpeg;base64,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"),
               new XElement("Text", "Luam tort"),
               new XElement("Image", "https://images.immediate.co.uk/production/volatile/sites/30/2020/08/triple-chocolate-peanut-butter-layer-cake-2-06eee24.jpg?quality=90&resize=500%2C454"),
               new XElement("Text", "Pofta buna")
               )
           );
                    break;
                case 1:
                    doc = new XDocument(
               new XComment("This is Tort fancy"),
               new XElement("Recipe",
               new XElement("Text", "Mergem la cofetarie"),
               new XElement("Image", "http://poemcaffe.ro/wp-content/uploads/2018/08/cofetarie2.jpg"),
                new XElement("Text", "Luam tort"),
               new XElement("Image", "https://images.immediate.co.uk/production/volatile/sites/30/2020/08/triple-chocolate-peanut-butter-layer-cake-2-06eee24.jpg?quality=90&resize=500%2C454"),
               new XElement("Text", "Pofta buna"),
               new XElement("Text", "Reteta asta are un rand extra ")
               )
           );
                    break;


                case 2:
                    doc = new XDocument(
               new XComment("This is Tarta"),
               new XElement("Recipe",
              
               new XElement("Image", "https://lifenews.ro/wp-content/uploads/2019/10/Obor-Recolta-piata-626x445.jpg"),
               new XElement("Text", "Mergi la piata"),
               new XElement("Text", "Cumperi cirese"),
               new XElement("Image", "https://www.msc.com/getattachment/b3f79c9a-a4a5-4bc5-a721-467801562f56/637411247812684996?width=370"),
               new XElement("Text", "Le mananci asa ca sunt scumpe")
               )
           );
                    break;
                case 3:

                    doc = new XDocument(
               new XComment("This is ceva cu fructe"),
               new XElement("Recipe",
               new XElement("Text", "Multe poze cu fructe"),
               new XElement("Image", "https://sites.google.com/site/fructesitee/_/rsrc/1363682688031/home/Mixed-Fruits-15-AC996402V2-1600x1200.jpg?height=300&width=400"),
               new XElement("Image", "https://sites.google.com/site/fructesitee/_/rsrc/1363682688031/home/Mixed-Fruits-15-AC996402V2-1600x1200.jpg?height=300&width=400"),
               new XElement("Image", "https://sites.google.com/site/fructesitee/_/rsrc/1363682688031/home/Mixed-Fruits-15-AC996402V2-1600x1200.jpg?height=300&width=400"),
               new XElement("Image", "https://sites.google.com/site/fructesitee/_/rsrc/1363682688031/home/Mixed-Fruits-15-AC996402V2-1600x1200.jpg?height=300&width=400"),
               new XElement("Image", "https://sites.google.com/site/fructesitee/_/rsrc/1363682688031/home/Mixed-Fruits-15-AC996402V2-1600x1200.jpg?height=300&width=400"),
               new XElement("Image", "https://sites.google.com/site/fructesitee/_/rsrc/1363682688031/home/Mixed-Fruits-15-AC996402V2-1600x1200.jpg?height=300&width=400")
             
               )
           );
                    break;


                default:
                    doc = new XDocument(
                   new XComment("This is a test for a recipe"),
                   new XElement("Recipe",
                   new XElement("Text", "THIS IS RANDOM TEXT 1"),
                   new XElement("Image", "https://miro.medium.com/max/2320/1*t_G1kZwKv0p2arQCgYG7IQ.gif"),
                   new XElement("Text", "THIS IS RANDOM TEXT 2"),
                   new XElement("Image", "https://pbs.twimg.com/profile_images/1342768807891378178/8le-DzgC_400x400.jpg"),
                   new XElement("Text", "THIS IS RANDOM TEXT 3")
                   )
               );
                    break;
            }


           

                doc.Save(currentPath + "/recipe.xml");
            
        }

    }
}
