using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

namespace Cozy.LearnFoundation.B
{
    class B3AccessControl
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Access_Control_to_Resources();
        }

        public static void Access_Control_to_Resources()
        {
            const string fileName = @"D:\cozy.txt";
            var filestream = File.Open(fileName, FileMode.Open);

            FileSecurity securityDesc = filestream.GetAccessControl();
            AuthorizationRuleCollection rules = securityDesc.GetAccessRules(true, true, typeof(NTAccount));
            foreach(AuthorizationRule rule in rules)
            {
                var fileRule = rule as FileSystemAccessRule;
                Console.WriteLine("Access type : {0}", fileRule.AccessControlType);
                Console.WriteLine("Right :{0}", fileRule.FileSystemRights);
                Console.WriteLine("Identity : {0}", fileRule.IdentityReference.Value);
            }
            // D6FileSecurity
        }
    }
}
