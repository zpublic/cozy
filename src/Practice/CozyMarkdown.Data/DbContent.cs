using LiteDB;
using CozyMarkdown.Data.Models;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

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

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> queryFunc) where T : BaseModel, new() {
            return liteDatabase.GetCollection<T>(nameof(T)).Find(queryFunc);
        }

        public T Get<T>(Expression<Func<T, bool>> queryFunc) where T : BaseModel, new() {
            return liteDatabase.GetCollection<T>(nameof(T)).FindOne(queryFunc);
        }

        public IEnumerable<T> GetAll<T>() where T : BaseModel, new() {
            return liteDatabase.GetCollection<T>(nameof(T)).FindAll();
        }

        public T Insert<T>(T model) where T : BaseModel, new() {
            var collection = liteDatabase.GetCollection<T>(nameof(T));
            var id = collection.Insert(model);
            if (id != null) {
                return collection.FindById(id);
            }
            return null;
        }

        public T Update<T>(T model) where T : BaseModel, new() {
            var collection = liteDatabase.GetCollection<T>(nameof(T));
            if (!collection.Update(model)) {
                return collection.FindById(model.Id);
            }
            return null;
        }

        public bool Delete<T>(Guid id) where T : BaseModel, new() {
            return liteDatabase.GetCollection<T>(nameof(T)).Delete(id);
        }
    }
}
