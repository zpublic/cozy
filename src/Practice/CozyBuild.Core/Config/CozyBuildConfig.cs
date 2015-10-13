using CozyBuild.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBuild.Core.Config
{
    public class CozyBuildConfig
    {
        public CppBuildConfig CppBuild { get; } = new CppBuildConfig();
        public CSharpBuildConfig CSharpBuild { get; } = new CSharpBuildConfig();
        public VisualStudioConfig VisualStudio { get; } = new VisualStudioConfig();
    }
}
