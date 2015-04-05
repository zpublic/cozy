using Cozy.LearnCSharp.O.Details;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.O
{
    class O3UserDefinedException
    {
        static void DoWork(string fileName)
        {
            var peopleToRing = new ColdCallFileReader();
            try
            {
                peopleToRing.Open(fileName);
                for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                {
                    peopleToRing.ProcessNextPerson();
                }
                Console.WriteLine("All callers processed correctly");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file {0} does not exist", fileName);
            }
            catch (ColdCallFileFormatException ex)
            {
                Console.WriteLine("The file {0} appears to have been corrupted",
                    fileName);
                Console.WriteLine("Details of problem are: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        "Inner exception was: {0}", ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred:\n" + ex.Message);
            }
            finally
            {
                peopleToRing.Dispose();
            }
        }
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            string fileName = "../../LearnCSharp/O/Details/cold_calling.txt";
            DoWork(fileName);
            fileName = "../../LearnCSharp/O/Details/no_file.txt";
            DoWork(fileName);
        }
    }
}
