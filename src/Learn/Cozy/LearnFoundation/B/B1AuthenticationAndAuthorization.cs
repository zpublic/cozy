using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Security.Permissions;

namespace Cozy.LearnFoundation.B
{
    class B1AuthenticationAndAuthorization
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Identity_and_Principal();
            Declarative_RoleBase_Security();
            Claims();
        }

        public static void Identity_and_Principal()
        {
            // 指定当前线程主体应保存一个Identity对象
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            // 访问主体属性
            var principal = WindowsPrincipal.Current as WindowsPrincipal;
            var identity = principal.Identity as WindowsIdentity;

            Console.WriteLine("IdentityType : {0}", identity.ToString());
            Console.WriteLine("Name : {0}", identity.Name);
            Console.WriteLine("User : {0}", principal.IsInRole(WindowsBuiltInRole.User));
            Console.WriteLine("Adminisrators : {0}", principal.IsInRole(WindowsBuiltInRole.Administrator));
            Console.WriteLine("AuthType : {0}", identity.AuthenticationType);
            Console.WriteLine("Anonymous : {0}", identity.IsAnonymous);
            Console.WriteLine("Token : {0}", identity.Token);
        }

        public static void Declarative_RoleBase_Security()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            try
            {
                RolesTest();
            }
            catch(SecurityException e)
            {
                Console.WriteLine("Security Exception {0}", e.Message);
            }
        }

        // 除非在Role指定的用户环境中执行 否则RoleTest将抛出一个异常
        [PrincipalPermission(SecurityAction.Demand, Role=@"kingwl\AngelC")]
        public static void RolesTest()
        {
            Console.WriteLine("Method used");
        }

        public static void Claims()
        {
            // 使用声称访问用户的信息
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            var principal = WindowsPrincipal.Current as WindowsPrincipal;

            foreach(var claim in principal.Claims)
            {
                Console.WriteLine("Subject {0}", claim.Subject);
                Console.WriteLine("Issuer {0}", claim.Issuer);
                Console.WriteLine("Type {0}", claim.Type);
                Console.WriteLine("Value Type {0}", claim.ValueType);
                Console.WriteLine("Value {0}", claim.Value);
                foreach(var prop in claim.Properties)
                {
                    Console.WriteLine("\t Property {0} {1}", prop.Key, prop.Value);
                }
            }
        }
    }
}
