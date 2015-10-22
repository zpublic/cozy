using LiteDB;
using CozyMarkdown.Data.Models;

namespace CozyMarkdown.Data {

    public class DbContent {

        private LiteDatabase liteDatabase;
        private static DbContent instance;

        private DbContent() {
            liteDatabase = new LiteDatabase("CozyMarkdown.db");
        }

        public static DbContent GetInstance() {
            instance = instance ?? new DbContent();
            return instance;
        }

        public LiteCollection<T> GetContent<T>() where T : IEntityModel, new() {
            return liteDatabase.GetCollection<T>(nameof(T));
        }
    }
}
