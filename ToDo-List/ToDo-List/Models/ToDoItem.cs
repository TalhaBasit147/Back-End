using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDo_List.Models
{
    public class ToDoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ItemId { get; set; }

        [BsonElement("Name")]
        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public bool ItemStatus { get; set; }
    }
}
