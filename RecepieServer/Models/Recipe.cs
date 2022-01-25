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
        [XmlElement]
        public string Thumbnail { get; set; }

        static long idCounter =  0;
    }

    public partial class Recipe
    {
        public Recipe() { }

        public Recipe(string name)
        {
            ID = idCounter++;
            Name = name;
            Thumbnail = "default.jpg";
        }

        public Recipe(long id, string name)
        {
            ID = id;
            Name = name;
            Thumbnail = "default.jpg";
        }
    }
}
