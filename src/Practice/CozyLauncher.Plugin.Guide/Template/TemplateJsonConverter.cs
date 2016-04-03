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
            var obj = new TemplateJsonObject();
            obj.ObjectType  = template.GetType().Name;
            obj.Content     = JsonConvert.SerializeObject(template);
            obj.Children    = template.Children.Select(x => Serialization(x));
            return JsonConvert.SerializeObject(obj);
        }

        public static TemplateBase Deserialization(string obj)
        {
            var json = JsonConvert.DeserializeObject<TemplateJsonObject>(obj);
            var template = JsonConvert.DeserializeObject<TemplateBase>(json.Content, new TemplateConverter(), new TemplateInfoConverter());

            template.Children = json.Children.Select(x => Deserialization(x)).ToList();

            return template;
        }
    }
}
