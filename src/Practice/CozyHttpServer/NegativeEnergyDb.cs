using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyHttpServer
{
    public class NegativeEnergyDb
    {
        private static readonly string _DatabasePath = System.Environment.CurrentDirectory + "/NegativeEnergy.db";

        public int GetZanNum(string name)
        {
            int n = 0;
            try
            {
                var db = Database.Opener.OpenFile(_DatabasePath);
                var data = db.Zan.FindByName(name);
                if (data != null)
                {
                    Console.WriteLine(name + " : " + data.Num.ToString());
                    n = data.Num;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                n = 0;
            }
            return n;
        }

        public void AddZanNum(string name, int add)
        {
            try
            {
                var db = Database.Opener.OpenFile(_DatabasePath);
                var data = db.Zan.FindByName(name);
                if (data != null)
                {
                    Console.WriteLine(name + " : " + (data.Num + add).ToString());
                    db.Zan.UpdateByName(Name: name, Num: data.Num + add);
                }
                else
                {
                    Console.WriteLine(name + " : " + add.ToString());
                    db.Zan.Insert(Name: name, Num: add);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
