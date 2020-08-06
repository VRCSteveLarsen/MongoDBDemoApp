using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBDemo
{
    [BsonIgnoreExtraElementsAttribute]
    public class NameModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
