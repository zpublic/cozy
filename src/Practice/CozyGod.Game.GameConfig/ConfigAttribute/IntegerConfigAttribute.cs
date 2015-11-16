using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Game.GameConfig.ConfigAttribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class CozyConfigAttribute : Attribute
    {

    }
}
