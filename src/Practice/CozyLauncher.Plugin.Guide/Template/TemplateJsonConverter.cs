using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CozyLauncher.Plugin.Guide.Template
{
    public static class TemplateJsonConverter
    {
        public static string Serialization(TemplateBase template)
        {
            return JsonConvert.SerializeObject(template);
        }

        public static TemplateBase Deserialization(string obj)
        {
            return JsonConvert.DeserializeObject<TemplateBase>(obj, new TemplateConverter(), new TemplateInfoConverter());
        }
    }
}
