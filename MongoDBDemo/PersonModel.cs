using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBDemo
{
    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
        // Store in the database as dob
        // Remember to use UTC to avoid weird date issues
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
    }
}
