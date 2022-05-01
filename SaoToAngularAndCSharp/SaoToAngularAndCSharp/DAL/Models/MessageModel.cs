using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.DAL.Models
{
    public class MessageModel
    {

        //public string Id { get; set; }
        public string WhatIsSent { get; set; }
        public DateTime TimeOfMessage { get; set; }

        public string SentBy { get; set; }

        public MessageModel(string whatIsSent, string senderId)
        {
            this.TimeOfMessage =  DateTime.Now;
            this.WhatIsSent = whatIsSent;
            this.SentBy = senderId;
        }
    }
}
