using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.O
{
    class O2CatchingExceptions
    {
        static void CheckInput(string userInput)
        {
            try
            {
                int index = Convert.ToInt32(userInput);
                if (index < 0 || index > 5)
                {
                    throw new IndexOutOfRangeException("You typed in " + userInput);
                }

                Console.WriteLine("Your number was " + index);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Exception: " +
                    "Number should be between 0 and 5. {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "An exception was thrown. Message was: {0}", ex.Message);
            }
            finally
            {
                Console.WriteLine("Thank you");
            }
        }
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            string userInput = "6";
            CheckInput(userInput);
            userInput = "3";
            CheckInput(userInput);
        }
    }
}
