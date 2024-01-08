using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCaller
{
    public class ConfigReader
    {
        public string test { get; set; }

        public string fichero { get; set; }      
        
        public string pizzas { get; set; }

        public ConfigReader()
        {
            test = ConfigurationManager.AppSettings["test"];

            fichero = ConfigurationManager.AppSettings["fichero"];

            pizzas = ConfigurationManager.AppSettings["pizzas"];
        }
    }
}
