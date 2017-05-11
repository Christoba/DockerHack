namespace kCura.Hack.Messaging
{
    using System;
    using kCura.Hack.Data;

    public class CustodianCreatedEvent : Message
    {
        public override string Subject { get { return MessageSubject; } }

        public DateTime CreatedAt { get; set; }

        public Custodian Custodian { get; set; }

        public static string MessageSubject = "events.custodian.created";
    }
}
