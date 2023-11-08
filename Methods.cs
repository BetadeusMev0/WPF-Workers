using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace worker_list
{
    class  Methods
    {
        public static void CreateXml() 
        {
            if (!(File.Exists("workers.xml"))) File.WriteAllText("workers.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<workers>\n</workers>");
        }
        public static void AddWorker(string departamentName, string workerName, string post, int salary, string description)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("workers.xml");
            XmlElement xRoot = xmlDocument.DocumentElement;
            XmlElement xDepartament = null;

            foreach (XmlElement xNode in xRoot) if (xNode.Attributes.GetNamedItem("name").Value == departamentName) xDepartament = xNode;

            if (xDepartament == null) 
            {
                xDepartament = xmlDocument.CreateElement("departament");
                XmlAttribute departamentAttr = xmlDocument.CreateAttribute("name");
                XmlText text = xmlDocument.CreateTextNode(departamentName);

                departamentAttr.AppendChild(text);
                xDepartament.Attributes.Append(departamentAttr);
                xRoot.AppendChild(xDepartament);
            }


        }


    }
}
