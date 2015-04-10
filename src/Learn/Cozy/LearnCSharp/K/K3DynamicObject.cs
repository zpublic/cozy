using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.K
{
    class K3DynamicObject
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            dynamic dyn;

            dyn = new WroxDynamicObject();
            dyn.FirstName = "Bugs";
            dyn.LastName = "Bunny";
            Console.WriteLine(dyn.GetType());
            Console.WriteLine("{0} {1}", dyn.FirstName, dyn.LastName);

            dyn.MiddleName = "Rabbit";
            Console.WriteLine(dyn.MiddleName);
            Console.WriteLine(dyn.GetType());
            Console.WriteLine("{0} {1} {2}", dyn.FirstName, dyn.MiddleName, dyn.LastName);
            List<Person> friends = new List<Person>();
            friends.Add(new Person() { FirstName = "Daffy", LastName = "Duck" });
            friends.Add(new Person() { FirstName = "Porky", LastName = "Pig" });
            friends.Add(new Person() { FirstName = "Tweety", LastName = "Bird" });
            dyn.Friends = friends;
            foreach (Person friend in dyn.Friends) {
                Console.WriteLine("{0} {1}", friend.FirstName, friend.LastName);
            }

            Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToShortDateString();
            dyn.GetTomorrowDate = GetTomorrow;
            Console.WriteLine("Tomorrow is {0}", dyn.GetTomorrowDate(DateTime.Now));

        }
    }

    class WroxDynamicObject : DynamicObject
    {

        Dictionary<string, object> _dynamicData = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool success = false;
            result = null;
            if (_dynamicData.ContainsKey(binder.Name))
            {
                result = _dynamicData[binder.Name];
                success = true;
            }
            else
                result = "Property Not Found!";

            return success;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dynamicData[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            dynamic method = _dynamicData[binder.Name];
            result = method((DateTime)args[0]);
            return result != null;
        }

    }
}
