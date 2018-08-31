using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tryingWithAngularChat.Models
{
    public class Workspace
    {
        public Workspace()
        {
            this.channels = new List<Channel>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string workspaceId { get; set; }
        [BsonElement("workspaceName")]
        public string workspaceName { get; set; }
        [BsonElement("channels")]
        public List<Channel> channels { get; set; }
       
    }
}
