using Cozy.LearnCSharp.J.Details.DataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.J
{
    class J3ExpressionTree
    {
        private static void DisplayTree(int indent, string message, Expression expression)
        {
            string output = String.Format("{0} {1} ! NodeType: {2}; Expr: {3} ",
                  "".PadLeft(indent, '>'), message, expression.NodeType, expression);

            indent++;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    Console.WriteLine(output);
                    LambdaExpression lambdaExpr = (LambdaExpression)expression;
                    foreach (var parameter in lambdaExpr.Parameters)
                    {
                        DisplayTree(indent, "Parameter", parameter);
                    }
                    DisplayTree(indent, "Body", lambdaExpr.Body);
                    break;
                case ExpressionType.Constant:
                    ConstantExpression constExpr = (ConstantExpression)expression;
                    Console.WriteLine("{0} Const Value: {1}", output, constExpr.Value);
                    break;
                case ExpressionType.Parameter:
                    ParameterExpression paramExpr = (ParameterExpression)expression;
                    Console.WriteLine("{0} Param Type: {1}", output, paramExpr.Type.Name);
                    break;
                case ExpressionType.Equal:
                case ExpressionType.AndAlso:
                case ExpressionType.GreaterThan:
                    BinaryExpression binExpr = (BinaryExpression)expression;
                    if (binExpr.Method != null)
                    {
                        Console.WriteLine("{0} Method: {1}", output, binExpr.Method.Name);
                    }
                    else
                    {
                        Console.WriteLine(output);
                    }
                    DisplayTree(indent, "Left", binExpr.Left);
                    DisplayTree(indent, "Right", binExpr.Right);
                    break;
                case ExpressionType.MemberAccess:
                    MemberExpression memberExpr = (MemberExpression)expression;
                    Console.WriteLine("{0} Member Name: {1}, Type: {2}", output,
                       memberExpr.Member.Name, memberExpr.Type.Name);
                    DisplayTree(indent, "Member Expr", memberExpr.Expression);
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("{0} {1}", expression.NodeType, expression.Type.Name);
                    break;
            }
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Expression<Func<Racer, bool>> expression = r => r.Country == "Brazil" && r.Wins > 6;
            DisplayTree(0, "Lambda", expression);
        }
    }
}
