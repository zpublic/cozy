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

        public T Save<T>(T model) where T : BaseModel, new() {
            var collection = liteDatabase.GetCollection<T>(nameof(T));
            var obj = collection.FindById(model.Id);
            if (obj == null) {
                collection.Insert(model);
            }
            else {
                collection.Update(model);
            }
            return model;
        }

        public bool Delete<T>(BsonValue id) where T : BaseModel, new() {
            return liteDatabase.GetCollection<T>(nameof(T)).Delete(id);
        }
    }
}
