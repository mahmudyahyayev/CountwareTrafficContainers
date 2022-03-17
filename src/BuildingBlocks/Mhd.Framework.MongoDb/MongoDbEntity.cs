using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mhd.Framework.MongoDb
{
    public abstract class MongoDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; set; }
    }
}

