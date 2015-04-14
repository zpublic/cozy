namespace Simple.Data.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.CodeDom.Compiler;
    using System.IO;
    using Microsoft.CSharp;

    public class QueryExecutor
    {
        private readonly string _query;

        public QueryExecutor(string query)
        {
            _query = query;
        }

        private static readonly string[] ConsoleAssemblyNames = new[]
                                                                    {
                                                                        "mscorlib.dll", "Microsoft.CSharp.dll",
                                                                        "System.dll", "System.Core.dll",
                                                                        "System.Data.dll", "System.Data.DataSetExtensions.dll",
                                                                        "System.Xml.dll", "System.Xml.Linq.dll",
                                                                        "Simple.Data.dll"
                                                                    };

        private static readonly Dictionary<string, string> CSharpCodeProviderOptions = new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };

        public bool CompileAndRun(Simple.Data.Database database, out object output)
        {
            var code = MakeCode();
            var compiled = Compile(code);

            if (compiled.Errors.HasErrors)
            {
                output = ReturnErrors(compiled);
                return false;
            }
            output = ReturnOutput(compiled, database);
            return true;
        }

        private string MakeCode()
        {
            var dbVariable = _query.Split('.')[0];
            var code =
                string.Concat(
                    "using System; using Microsoft.CSharp; namespace Q { public class Qr { public object Run(dynamic ",
                    dbVariable, ") { return ", _query, "; } } }");
            return code;
        }

        private static string ReturnErrors(CompilerResults compiled)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Compilation errors:");
            compiled.Errors.Cast<CompilerError>()
                .Select(ce => ce.ErrorText)
                .ToList()
                .ForEach(error => builder.AppendLine(error));
            return builder.ToString();
        }

        private static CompilerResults Compile(string code)
        {
            using (var csc = new CSharpCodeProvider(CSharpCodeProviderOptions))
            {
                var parameters = new CompilerParameters(ConsoleAssemblyNames) { IncludeDebugInformation = true, GenerateInMemory = true };
                var results = csc.CompileAssemblyFromSource(parameters, code);
                return results;
            }
        }

        private static object ReturnOutput(CompilerResults compiled, Database database)
        {
            try
            {
                var assembly = compiled.CompiledAssembly;
                var type = assembly.GetType("Q.Qr");
                var instance = Activator.CreateInstance(type);
                var run = type.GetMethod("Run");
                return run.Invoke(instance, new object[] { database });
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
