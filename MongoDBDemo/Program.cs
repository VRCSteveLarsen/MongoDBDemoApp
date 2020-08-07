using System;
using System.Reflection.Metadata;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD(MongoCollections.AddressBookDB);

            //PersonModel person = new PersonModel
            //{
            //    FirstName = "Jane",
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
            var recs = db.LoadRecords<NameModel>(MongoCollections.Users);
            // Find a valid record with a Primary address and get the Guid
            //var id = recs.Where(r => r.PrimaryAddress != null).First().Id;

            foreach (var rec in recs)
            {
                Console.WriteLine($"{rec.Id}: {rec.FirstName} {rec.LastName}");
                Console.WriteLine();// Separate records
            }

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

            //// Get a specific record via Guid
            //var oneRec = db.LoadRecordById<PersonModel>(MongoCollections.Users, id);
            //// Using UTC to avoid some timezone weirdness
            //oneRec.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            //// Upsert record
            //db.UpsertRecord(MongoCollections.Users, oneRec.Id, oneRec);


            Console.ReadLine();
        }
    }
}
