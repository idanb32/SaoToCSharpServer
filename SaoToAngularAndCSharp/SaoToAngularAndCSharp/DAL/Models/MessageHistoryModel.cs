using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.DAL.Models
{
    public class MessageHistoryModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string SentTo { get; set; }
        public string SentBy { get; set; }
        public List<MessageModel> ChatLog { get; set; }
    }
}
