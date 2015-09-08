using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCoderAlmanac.Core
{
    public class DateResource
    {
        public List<string> varNames = new List<string>()
        {
            "jieguo", "huodong", "pay", "expire", "zhangdan", "every", "free", "i1", "a", "virtual", "ad", "spider", "mima", "pass", "ui",
        };

        public List<string> tools = new List<string>()
        {
            "Eclipse写程序", "MSOffice写文档", "记事本写程序", "Windows8", "Linux", "MacOS", "IE", "Android设备", "iOS设备",
        };

        public List<Special> specials = new List<Special>()
        {
            new Special(DateTime.ParseExact("20151225","yyyyMMdd",System.Globalization.CultureInfo.CurrentCulture), "bad", "待在男（女）友身边", "脱团火葬场，入团保平安。")
        };

        public List<string> directions = new List<string>()
        {
            "北方","东北方","东方","东南方","南方","西南方","西方","西北方",
        };

        public List<string> drinks = new List<string>()
        {
            "水","茶","红茶","绿茶","咖啡","奶茶","可乐","鲜奶","豆奶","果汁","果味汽水","苏打水","运动饮料","酸奶","酒",
        };
    }
}
