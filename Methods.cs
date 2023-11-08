using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker_list
{
    class  Methods
    {
        public static void CreateXml() 
        {
            if (!(File.Exists("workers.xml"))) File.WriteAllText("workers.xml", "<workers>\n</workers>");



        }
    }
}
