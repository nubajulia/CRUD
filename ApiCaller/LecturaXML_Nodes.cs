using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ApiCaller
{
    public class LecturaXML_Nodes
    {
        public void LecturaXML(string filepath)
        {
            string text = string.Empty;

            XmlDocument document = new XmlDocument();
            document.Load(filepath);

            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                text += node.InnerText;
            }

            MessageBox.Show(text);
        }

        public ItemsReaded? LecturaXML_Deserialize(string filepath)
        {
            ItemsReaded? i = null;
            var serializer = new XmlSerializer(typeof(ItemsReaded));
            using(Stream reader = new FileStream(filepath, FileMode.Open))
            {
                i = serializer.Deserialize(reader) as ItemsReaded;
            }
            return i;
        }

        public PizzaReader? LecturaXML_DeserializePizza(string filepath)
        {
            PizzaReader? i = null;
            var serializer = new XmlSerializer(typeof(PizzaReader));
            using(Stream reader = new FileStream(filepath,FileMode.Open))
            {
                i = serializer.Deserialize(reader) as PizzaReader;
            }

            return i;
        }

    }
}
