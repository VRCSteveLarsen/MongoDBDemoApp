using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBDemo
{
    // We need a list of the tables/collections we are going to use
    // MongoDB will just make a new table if we misspell the table name
    public static class MongoCollections
    {
        public const string AddressBookDB = "AddressBook";
        public const string Users = "Users";
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            // Create the collection
            var collection = db.GetCollection<T>(table);
            // Insert the record
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            // Create the collection
            var collection = db.GetCollection<T>(table);
            // Convert the find results to a list we can use
            return collection.Find<T>(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            // Create the collection
            var collection = db.GetCollection<T>(table);
            // Create the filter
            var filter = Builders<T>.Filter.Eq("Id", id);
            // Return the filtered results we found
            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            // Create the collection
            var collection = db.GetCollection<T>(table);
            // Create the filter
            var filter = Builders<T>.Filter.Eq("Id", id);
            // Replace the record via an upsert
            // Upsert is an update if the filter finds something or
            // an insert if there is nothing matching
            collection.ReplaceOne(
                filter,
                record,
                new ReplaceOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            // Create the collection
            var collection = db.GetCollection<T>(table);
            // Create the filter
            var filter = Builders<T>.Filter.Eq("Id", id);
            // Delete the record
            collection.DeleteOne(filter);
        }
    }
}
