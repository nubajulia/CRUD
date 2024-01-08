using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiCaller
{
    [XmlRoot("pizzas")]
    public class PizzaReader
    {
        [XmlElement("pizza")]
        public List<Pizza> pizza { get; set; }

    }

    public class Pizza
    {
        [XmlAttribute("nombre")]
        public string nombre { get; set;}

        [XmlAttribute("precio")]
        public double Precio { get; set;}

        [XmlElement("ingrediente")]
        public List<Ingrediente> ingrediente { get; set; }
        
    }
    public class Ingrediente
    {
        [XmlAttribute("nombre")]
        public string nombre { get; set;}
    }
}
