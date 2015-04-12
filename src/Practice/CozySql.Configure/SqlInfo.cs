using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Configure
{
    // Sql信息基类
    class SqlInfo
    {
        public SqlInfo(string name)
        {
            this.name = name;
        }

        // 名称
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public override string ToString()
        {
            return "SqlInfo Name : " + Name;
        }
    }

    // MySql的信息
    class MySqlInfo : SqlInfo
    {
        public MySqlInfo(string name)
        :base(name)
        {

        }

        public MySqlInfo(string name, string address, string username, string password)
            :base(name)
        {
            this.Address = address;
            this.UserName = username;
            this.PassWord = password;
        }

        // 数据库地址
        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        // 用户名
        private string username;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = UserName;
            }
        }

        // 密码
        private string password;
        public string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public override string ToString()
        {
            StringBuilder strbd = new StringBuilder();
            strbd.AppendLine("MySqlInfo Name : " + Name);
            strbd.AppendLine("MySqlInfo Address : " + Address);
            strbd.AppendLine("MySqlInfo UserName : " + UserName);
            strbd.AppendLine("MySqlInfo PassWord : " + PassWord);

            return strbd.ToString();
        }
    }

    // Sqlitle类的信息
    class SqliteInfo : SqlInfo
    {
        public SqliteInfo(string name)
        :base(name)
        {

        }

        public SqliteInfo(string name, string path)
            :base(name)
        {
            this.DBPath = path;
        }

        // 文件路径
        private string dbpath;
        public string DBPath
        {
            get
            {
                return dbpath;
            }
            set
            {
                dbpath = value;
            }
        }
    }
}
