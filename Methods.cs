using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace worker_list
{
    class Methods
    {
        public enum Result 
        {
            Success,
            Error
        }



        public static void CreateXml() 
        {
            if (!(File.Exists("workers.xml"))) File.WriteAllText("workers.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<workers>\n</workers>");
        }

        public static Result RemoveWorker(string departament, string name) 
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("workers.xml");
            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement xDepartament = null;


            foreach(XmlElement xNode in xRoot) 
            {
                if (xNode.GetAttribute("name") == departament) xDepartament = xNode;
            }
            if (xDepartament == null) return Result.Error;

            XmlElement workerEl = null;

            foreach(XmlElement xNode in xDepartament) 
            {
                if (xNode.GetAttribute("name") == name) workerEl = xNode;
            }

            if (workerEl == null) return Result.Error;

            xDepartament.RemoveChild(workerEl);


            xmlDoc.Save("workers.xml");
            return Result.Success;
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
            var workerEl = xmlDocument.CreateElement("worker");
            var workerNameAttr = xmlDocument.CreateAttribute("name");
            var workerNameAttrText = xmlDocument.CreateTextNode(workerName);
            workerNameAttr.AppendChild(workerNameAttrText);

            var workerPost = xmlDocument.CreateAttribute("post");
            var workerPostText = xmlDocument.CreateTextNode(post);
            workerPost.AppendChild(workerPostText);

            var workerSalary = xmlDocument.CreateAttribute("salary");
            var workerSalaryText = xmlDocument.CreateTextNode(salary.ToString());
            workerSalary.AppendChild(workerSalaryText);
           
            var workerDescription = xmlDocument.CreateAttribute("description");
            var workerDescriptionText = xmlDocument.CreateTextNode(description);
            workerDescription.AppendChild(workerDescriptionText);

            workerEl.Attributes.Append(workerNameAttr);
            workerEl.Attributes.Append(workerPost);
            workerEl.Attributes.Append(workerSalary);
            workerEl.Attributes.Append(workerDescription);

            xDepartament.AppendChild(workerEl);
            xmlDocument.Save("workers.xml");
        }


    }
}
