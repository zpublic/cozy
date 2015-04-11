using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace Cozy.LearnFoundation.D
{
    class D8IsolatedStorageReadAndWrite
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            IsolatedStorage_Read_and_Write_Sample();
        }

        public static void IsolatedStorage_Read_and_Write_Sample()
        {
            string fileName = @"SelfWindow.xml";

            IsolatedStorageFile storFile = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream storStream = new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write);
            System.Xml.XmlWriter writer = new System.Xml.XmlTextWriter(storStream, Encoding.UTF8);
            writer.WriteStartDocument();

            writer.WriteStartElement("Settings");

            writer.WriteStartElement("UserID");
            writer.WriteValue(42);
            writer.WriteEndElement();
            writer.WriteStartElement("UserName");
            writer.WriteValue("kingwl");
            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
            storStream.Close();

            string[] userFiles = storFile.GetFileNames();

            foreach(var userFile in userFiles)
            {
                if(userFile == fileName)
                {
                    var storFileStreamnew =  new IsolatedStorageFileStream(fileName,FileMode.Open,FileAccess.Read);
                    StreamReader storReader = new StreamReader(storFileStreamnew);
                    System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(storReader);

                    int UserID = 0;
                    string UserName = null;

                    while(reader.Read())
                    {
                        switch(reader.Name)
                        {
                            case "UserID":
                                UserID = int.Parse(reader.ReadString());
                                break;
                            case "UserName":
                                UserName = reader.ReadString();
                                break;
                            default:
                                break;
                        }
                    }

                    Console.WriteLine("{0} {1}", UserID, UserName);

                    storFileStreamnew.Close();
                }
            }
            storFile.Close();
        }
    }
}
