using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace RecepieServer
{ 

    [XmlRoot("Recipe")]
    public partial class Recipe
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public long ID { get; set; }
      
        static long idCounter =  0;
    }

    public partial class Recipe
    {
        public Recipe() { }

        public Recipe(string name)
        {
            ID = idCounter++;
            Name = name;
        }

        public Recipe(long id, string name)
        {
            ID = id;
            Name = name;
        }


        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Recipe));

            var DirPath = $"Recipes/{ID}";
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }

            var FilePath = DirPath + "/recipe.xml";
           
            //@TODO SWITCH TO DETAILS
            FileStream file = File.Open(FilePath,FileMode.OpenOrCreate);
            serializer.Serialize(file,this);   
        }

        public void Load()
        {

        }
    }
}
