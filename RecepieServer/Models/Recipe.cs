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
        }

        public string Name { get; set; }
        public long ID { get;  }

        void CreateNewXMLFile()
        {
            string currentPath = $"Recipes/{ID}";
            System.IO.Directory.CreateDirectory(currentPath);

            XDocument doc = new XDocument(
                new XComment("This is a test for a recipe"),
                new XElement("Recipe",
  
                    new XElement("Text", "THIS IS RANDOM TEXT 1"),
                    new XElement("Image", "https://tinyurl.com/5bc6r2z5"),
                    new XElement("Text", "THIS IS RANDOM TEXT 2"),
                    new XElement("Image", "https://tinyurl.com/5bc6r2z5"),
                    new XElement("Text", "THIS IS RANDOM TEXT 3")
                    )
                );

                doc.Save(currentPath + "/recipe.xml");
            
        }

    }
}
