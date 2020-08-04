using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace MongoDBDemo
{
    // We need a list of the tables/collections we are going to use
    // MongoDB will just make a new table if we misspell the table name
    public static class MongoCollections
    {
        public const string AddressBookDB = "AddressBook";
        public const string Users = "Users";
    }
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD(MongoCollections.AddressBookDB);

            //PersonModel person = new PersonModel
            //{
            //    FirstName = "Joe",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "18512"
            //    }
            //};
            //// Insert a new record
            //db.InsertRecord(MongoCollections.Users, person);

            // Use a static const string for the tables we want
            var recs = db.LoadRecords<PersonModel>(MongoCollections.Users);
            // Find a valid record with a Primary address and get the Guid
            var id = recs.Where(r => r.PrimaryAddress != null).First().Id;

            //foreach (var rec in recs)
            //{
            //    Console.WriteLine($"{rec.Id}: {rec.FirstName} {rec.LastName}");

            //    // Check to see if we have an addres
            //    if(rec.PrimaryAddress != null)
            //    {
            //        Console.WriteLine(rec.PrimaryAddress.City);
            //    }

            //    Console.WriteLine();// Separate records
            //}

            // Get a specific record via Guid
            var record = db.LoadRecordById<PersonModel>(MongoCollections.Users, id);

            Console.ReadLine();
        }
    }

    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }

    public class AddressModel
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
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
            return collection.Find(new BsonDocument()).ToList();
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
    }
}
