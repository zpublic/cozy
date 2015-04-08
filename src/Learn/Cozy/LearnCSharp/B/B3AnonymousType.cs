using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.B
{
    class B3AnonymousType
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //匿名类在使用linq做数据处理的时候非常有用

            //例如有一个Model类型是和数据库某张表的结构完全一致，但在UI上面需要呈现的数据不需要那么多
            //或者需要两个字段拼接成一个绑定UI上面，匿名类的用处就体现出来了

            //创建一个List<Model>的数据源
            var dataSource = new Func<List<Model>>(() =>
            {
                return Enumerable.Range(0, 20).Select(x => new Model
                {
                    Id = x,
                    FirstName = string.Format("FirstName{0}", x),
                    LastName = string.Format("LastName{0}", x)
                }).ToList();
            })();

            //通过Select()返回一个List<AnonymousType>对象，打印出来
            dataSource.Select(x => new {Number = x.Id, Name = x.FirstName + x.LastName , 年龄 = x.Age}).ToList().ForEach(x =>            
                Console.WriteLine("编号：{0} \t姓名:{1}  \t年龄:{2}", x.Number, x.Name, x.年龄));
        }

        class Model
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }
    }    
}
