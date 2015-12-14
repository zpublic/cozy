using Nancy;
using Nancy.Conventions;

namespace CozyPress.WebServer.Core {

    public class ApplicationBootstrapper : DefaultNancyBootstrapper {

        protected override void ConfigureConventions(NancyConventions nancyConventions) {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("wwwroot", @"wwwroot"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
