using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cozy.LearnFoundation.G
{
    class G1SStreamedXML
    {
        static void testXmlReader()
        {
            XmlReader rdr = XmlReader.Create("../../LearnFoundation/G/Details/books.xml");
            while (rdr.Read())
            {
                if (rdr.Value.ToString().Trim() != "")
                {
                    Console.WriteLine(rdr.Value.ToString());
                }
            }
            Console.WriteLine("\n");

            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreComments = true;
            rdr = XmlReader.Create("../../LearnFoundation/G/Details/books.xml", setting);
            while (rdr.Read())
            {
                if (rdr.Value.ToString().Trim() != "")
                {
                    Console.WriteLine(rdr.Value.ToString());
                }
            }
            Console.WriteLine("\n");
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            testXmlReader();
        }
    }
}
