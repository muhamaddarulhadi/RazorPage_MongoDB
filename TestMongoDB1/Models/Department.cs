using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestMongoDB.Models;

    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {get; set;}
        public string? DeptName {get; set;}
         public string? Institution {get; set;}
        public string? Address {get; set;}
    }
