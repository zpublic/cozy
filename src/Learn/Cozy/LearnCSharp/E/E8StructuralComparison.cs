using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Cozy.LearnCSharp.E
{
    // 实现IEquatable接口
    public class Student : IEquatable<Student>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }

        public override string ToString()
        {
            return String.Format("Id : {0} Name : {1} Class : {2}", ID, Name, Class);
        }

        // 重写Equals
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return base.Equals(obj);
            }
            return Equals(obj as Student);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        // 实现IEquatable接口的Equals方法
        public bool Equals(Student other)
        {
            if(other == null)
            {
                return base.Equals(other);
            }

            return this.ID == other.ID && this.Name == other.Name;
        }
    }

    class TupleComparer : IEqualityComparer
    {
        // 实现IEqualityComparer定义的Equal方法
        public new bool Equals(object x, object y)
        {
            // 调用object的Equals虚方法
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    class E8StructuralComparison
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Comparison();
        }

        public static void Comparison()
        {
            Class_Compare();
            Array_Compare();
            Tuple_Compare();
        }

        public static void Class_Compare()
        {
            Student stu1 = new Student { ID = 1, Name = "john", Class = 1 };
            Student stu2 = new Student { ID = 2, Name = "kyon", Class = 1 };
            Student stu3 = new Student { ID = 1, Name = "john", Class = 2 };

            bool b1 = stu1 == stu2;
            bool b2 = stu1 == stu3;             // 引用的不是同一个对象
            bool b3 = stu1.Equals(stu3);        // 调用Equals比较

            Console.Write("{0} {1} {2}\n", b1, b2, b3);
        }

        public static void Array_Compare()
        {
            Student[] stuArr1 = 
            {
                new Student { ID = 1, Name = "john", Class = 1 },
                new Student { ID = 2, Name = "kyon", Class = 1 }
            };

            Student[] stuArr2 = 
            {
                new Student { ID = 1, Name = "john", Class = 1 },
                new Student { ID = 2, Name = "kyon", Class = 1 }
            };

            bool b1 = stuArr1 == stuArr2;               // 引用的不是同一个对象
            bool b2 = stuArr1.Equals(stuArr2);          // 默认的Equals方法

            // 调用IStructuralEquatable接口定义的Equals方法 采用EqualityComparer的默认实现
            bool b3 = (stuArr1 as IStructuralEquatable).Equals(stuArr2, EqualityComparer<Student>.Default);
            Console.Write("{0} {1} {2}\n", b1, b2, b3);
        }

        public static void Tuple_Compare()
        {
            Tuple<Student, int> tp1 = Tuple.Create<Student, int>(new Student { ID = 1, Name = "john", Class = 1 }, 2);
            Tuple<Student, int> tp2 = Tuple.Create<Student, int>(new Student { ID = 1, Name = "kyon", Class = 1 }, 2);
            Tuple<Student, int> tp3 = Tuple.Create<Student, int>(new Student { ID = 1, Name = "john", Class = 1 }, 3);
            Tuple<Student, int> tp4 = Tuple.Create<Student, int>(new Student { ID = 1, Name = "john", Class = 2 }, 2);

            bool b1 = tp1.Equals(tp3);
            bool b2 = (tp1 as IStructuralEquatable).Equals(tp2, new TupleComparer());
            bool b3 = (tp1 as IStructuralEquatable).Equals(tp3, new TupleComparer());
            bool b4 = (tp1 as IStructuralEquatable).Equals(tp4, new TupleComparer());

            Console.Write("{0} {1} {2} {3}\n", b1, b2, b3, b4);
        }
    }
}
