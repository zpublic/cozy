using Nancy;
using CozyMarkdown.Data;

namespace CozyMarkdown.WebStie.Module {

    public class BaseModule : NancyModule {

        protected DbContent db;

        public BaseModule() {
            db = DbContent.GetInstance();
        }

        public BaseModule(string path) : base(path) {
            db = DbContent.GetInstance();
        }
    }
}
