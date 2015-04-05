using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.B
{
    class B4WeakReference
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            var model = new Model { Name = "Test" };

            //强引用
            //直接把一个对象的实例分配到一个变量时，该变量就包含对对象的一个强引用
            //GC就不会回收正在使用的引用
            //只有strongReference变量离开作用域或者 strongReference = null 时，GC才会对它标记为可回收
            var strongReference = model;

            //弱引用
            //直接把需要弱引用的实例传入到WeakReference的构造函数即可
            //弱引用可以在使用引用的时候，GC可适当对原对象引用进行回收
            var weakReference = new WeakReference(model);

            //下面的代码无法证明以上说法。
            //虽然测试代码无法证明，但也有原因：调用GC.Collect()不意味立即进行垃圾回收，而且 'GC永远只回收你永远访问不到的对象'。
            model = null;
            GC.Collect();
            Console.WriteLine(string.Format("强引用在GC后的结果，Name:{0}", strongReference.Name));
            Console.WriteLine(string.Format("弱引用在GC后的结果，Name:{0}", ((Model)weakReference.Target).Name));
        }
    }

    class Model : IDisposable
    {
        public string Name { get; set; }

        public void Dispose()
        {           
            GC.SuppressFinalize(this);
        } 
    }
}
