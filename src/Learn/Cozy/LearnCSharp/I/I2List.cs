using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.I
{
    class I2List
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //List<T>是.NET里面使用最为广泛的集合类型，该类型实现了IEnumerable,
            //ICollection,IList,IEnumerable<T>,ICollection<T>,IList<T>接口

            //创建一个列表
            var modelList = new List<Model>();

            var model = new Model { Id = 1, Name = "F" };

            //往列表添加一个元素
            modelList.Add(model);

            //往列表添加一个集合
            modelList.AddRange(new [] {
                new Model {Id = 2, Name = "E"},
                new Model {Id = 3, Name = "D"},
                new Model {Id = 4, Name = "C"},
                new Model {Id = 5, Name = "B"},
                new Model {Id = 6, Name = "A"}
            });

            //往中间位置插入一个元素
            modelList.Insert(modelList.Count / 2, new Model {Id = 10, Name = "Max"});

            //根据值删除元素
            modelList.Remove(model);

            //根据索引删除元素
            modelList.RemoveAt(1);

            //根据索引访问最后一个元素
            Console.WriteLine("列表中最后一个元素： {{Id:{0}，Name:{1}}}",
                modelList[modelList.Count - 1].Id, modelList[modelList.Count - 1].Name);

            //根据Name排序,并且继承IEnumerable<T>，使用foreach迭代
            modelList.OrderBy(x => x.Name).ToList().ForEach(x => Console.WriteLine("根据Name排序：{0}", x.Name));

            //根据查找Id大于5的元素个数
            Console.WriteLine("查找Id大于5的元素: 个数{0}", modelList.Count(x => x.Id > 5));

            //根据查找Id大于5的元素，并且显示出来
            modelList.Where(x => x.Id > 5).ToList().ForEach(x => Console.WriteLine("{{Id:{0}，Name:{1}}}", x.Id, x.Name));
        }
    }

    class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }     
    }
}
