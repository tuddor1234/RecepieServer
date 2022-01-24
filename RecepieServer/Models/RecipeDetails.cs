using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RecepieServer.Models
{
    public class RecipeDetails
    {
        [XmlElement]
        public Recipe recipe;

        [XmlArray]
        public List<Instruction> Instructions { get; set; }

        public RecipeDetails()
        {
            Instructions = new List<Instruction>();
        }
    }
}
