using ProductLaunch.Entities;
using System;

namespace ProductLaunch.Messaging.Messages.Events
{
    public class CustodianCreatedEvent : Message
    {
        public override string Subject { get { return MessageSubject; } }

        public DateTime CreatedAt { get; set; }

        public Custodian Custodian { get; set; }

        public static string MessageSubject = "events.custodian.created";
    }
}
