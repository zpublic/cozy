using CozyLauncher.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CozyLauncher.Plugin.Guide.Template.Info;

namespace CozyLauncher.Plugin.Guide.Template
{
    public class TemplateConverter : JsonCreationConverter<TemplateBase>
    {
        protected override TemplateBase Create(Type objectType, JObject jObject)
        {
            if(FieldExists("Info", jObject))
            {
                return new DataTemplate();
            }
            else if (FieldExists("IsHorizontal", jObject))
            {
                return new PanelTemplate();
            }
            else
            {
                return new TabTemplate();
            }
        }
    }

    public class TemplateInfoConverter : JsonCreationConverter<ITemplateInfo>
    {
        protected override ITemplateInfo Create(Type objectType, JObject jObject)
        {
            if (FieldExists("Path", jObject))
            {
                return new ImageInfo();
            }
            else if (FieldExists("Header", jObject))
            {
                return new TabInfo();
            }
            else
            {
                return new TextInfo();
            }
        }
    }
}
