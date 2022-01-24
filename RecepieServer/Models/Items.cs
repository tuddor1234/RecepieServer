using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace RecepieServer.Models
{
    public enum InstructionType {

        Image = 0 ,
        Text = 1
    };

    public class Instruction
    {
        [XmlElement]
        public InstructionType type;

        [XmlElement]
        public string data { get; set; }

        public bool IsImage { get { return type == InstructionType.Image; }}
    }

    public class Ingredient
    {
        public string Name;
        public float QuantityPerPortion;
    }
}
