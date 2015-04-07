using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{

    class I7Dictionarie
    {

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");


            #region Dictionary<TKey, TValue>
            //Dictionary<TKey, TValue>  k-v
            var dictionary = new Dictionary<string, string>();

            dictionary.Add("1", "A");
            dictionary.Add("2", "B");
            dictionary.Add("3", "C");
            dictionary.Add("4", "D");
            dictionary.Add("5", "A");
            dictionary.Add("6", "A");
            dictionary.Add("7", "A");
            dictionary.Add("8", "A");

            dictionary.Remove("5");

            foreach (var key in dictionary.Keys) {
                Console.WriteLine(key);
            }

            foreach (var value in dictionary.Values) {
                Console.WriteLine(value);
            }

            Console.WriteLine("key为\"1\"的value是{0}", dictionary["1"]);

            #endregion

            #region LookUp<TKey,TValue>

            //LookUp<TKey,TValue>   k-*v
            //LookUp<TKey,TValue>只能通过ToLookup()方法去创建，
            //实现了IEnumerable<T>接口的类型都拥有此方法

            //拿到Value为A的字典集合
            var lookupDictionary = dictionary.ToLookup(x => x.Value)["A"];

            //lookupDictionary
            foreach (KeyValuePair<string, string> item in lookupDictionary)
            {
                Console.WriteLine(item);
            }

            //dictionary
            foreach (KeyValuePair<string, string> item in dictionary)
            {
                Console.WriteLine(item);
            }

            #endregion

        }
    }
}
