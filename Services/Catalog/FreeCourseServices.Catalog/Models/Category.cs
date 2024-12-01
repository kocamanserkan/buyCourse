using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourseServices.Catalog.Models
{
    internal class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

    }
}
