using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tryingWithAngularChat.Models
{
    public class Channel
    {
        public Channel()
        {
            this.users = new List<User>();
            this.messages = new List<Message>();
            this.admin = new User();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string channelId { get; set; }
        [BsonElement("channelName")]
        public string channelName { get; set; }
        [BsonElement("users")]
        public List<User> users { get; set; }
        [BsonElement("admin")]
        public User admin { get; set; }
        [BsonElement("messages")]
        public List<Message> messages { get; set; }
    }
}
