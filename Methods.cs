using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Animation;
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


        public static void test() 
        {
            AddWorker("Finland", "Andrey", "Kretin", 200, "");
            RemoveDepartament("Finlands");
            AddApplication("Finland", "Andrey", "Test Applicatioan", "Other", "Nu rabota tipo", "c:sas");
            RemoveApplication("Finland", "Andrey", "231");
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
            XmlElement xDepartament = GetElement(xRoot, departamentName);
            
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

        public static Result RemoveDepartament(string departamentName) 
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("workers.xml");

            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement xDepartament = GetElement(xRoot, departamentName);
            if (xDepartament == null) return Result.Error;

            xRoot.RemoveChild(xDepartament);

            xmlDoc.Save("workers.xml");
            return Result.Success;
        }

       

        public static Result AddApplication(string departamentName, string workerName, string applicationName, string applicationType, string applicationDescription, string applicationPath) 
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("workers.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement xDepartament = GetElement(xRoot, departamentName);
            if (xDepartament == null) { return Result.Error; }
            XmlElement xWorker = GetElement(xDepartament, workerName);
            if (xWorker == null) return Result.Error;
            XmlElement xApplication = xDoc.CreateElement("application");
            XmlAttribute xAppNameAttr = xDoc.CreateAttribute("name");
            var xAppNameTxt = xDoc.CreateTextNode(applicationName);
            xAppNameAttr.AppendChild(xAppNameTxt);

            XmlAttribute xAppTypeAttr = xDoc.CreateAttribute("type");
            var xAppTypeTxt = xDoc.CreateTextNode(applicationType);
            xAppTypeAttr.AppendChild(xAppTypeTxt);

            XmlAttribute xAppDescAttr = xDoc.CreateAttribute("description");
            var xAppDescTxt = xDoc.CreateTextNode(applicationDescription);
            xAppDescAttr.AppendChild(xAppDescTxt);

            var xAppPath = xDoc.CreateTextNode(applicationPath);
            xApplication.AppendChild(xAppPath);

            xApplication.Attributes.Append(xAppNameAttr);
            xApplication.Attributes.Append(xAppTypeAttr);
            xApplication.Attributes.Append(xAppDescAttr);
            
            xWorker.AppendChild(xApplication);

            xDoc.Save("workers.xml");

            return Result.Success;
        }

        public static Result RemoveApplication(string departamentName, string workerName, string applicationName) 
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("workers.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement xDepartament = GetElement(xRoot, departamentName);
            if (xDepartament == null) return Result.Error;
            XmlElement xWorker = GetElement(xDepartament, workerName);
            if (xWorker == null) return Result.Error;
            XmlElement xApplication = null;
            if ((xApplication = GetElement(xWorker, applicationName)) == null) return Result.Error;
            xWorker.RemoveChild(xApplication);
            xDoc.Save("workers.xml");
            return Result.Success;
        }

        private static XmlElement GetElement(XmlElement xRoot, string elementName) 
        {
            XmlElement xElement = null;

            foreach (XmlElement xNode in xRoot)
            {
                if (xNode.GetAttribute("name") == elementName) { xElement = xNode; break; }
            }

            return xElement;
        }

        public static List<Departament> GetDepartaments(string fileName) 
        {
            return null;
        }

        public static void RefreshDepartaments(List<Departament> departaments, string fileName) 
        {

        }




        public class Departament 
        {
            public string Name { get; set; }
            public List<Worker> workers{ get; set; }
        }

        public class Worker 
        {
            public string Name { get; set; }
            public string Post { get; set; }
            public string salary { get; set; }
            public string description { get; set; }

            public List<Application> applications { get; set; }
        }


        public class Application 
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public string Path { get; set; }
        }


    }
}
