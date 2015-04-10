using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Cozy.LearnCSharp.K
{
    class K4ExpandoObject
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            dynamic expObj = new ExpandoObject();
            expObj.FirstName = "Daffy";
            expObj.LastName = "Duck";
            Console.WriteLine(expObj.FirstName + " " + expObj.LastName);
            Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToShortDateString();
            expObj.GetTomorrowDate = GetTomorrow;
            Console.WriteLine("Tomorrow is {0}", expObj.GetTomorrowDate(DateTime.Now));

            expObj.Friends = new List<Person>();
            expObj.Friends.Add(new Person() { FirstName = "Bob", LastName = "Jones" });
            expObj.Friends.Add(new Person() { FirstName = "Robert", LastName = "Jones" });
            expObj.Friends.Add(new Person() { FirstName = "Bobby", LastName = "Jones" });

            foreach (Person friend in expObj.Friends)
            {
                Console.WriteLine(friend.FirstName + "  " + friend.LastName);
            }
        }
    }
}
