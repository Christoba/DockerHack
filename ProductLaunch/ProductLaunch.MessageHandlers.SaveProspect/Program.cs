

namespace kCura.Hack.Client
{
    using System;
    using System.Threading;

    using NATS.Client;
    using ProductLaunch.Messaging;
    using ProductLaunch.Messaging.Messages.Events;

    class Program
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            ClientHelper.LogService.LogInfo("Client Console Running at: {0}", DateTime.Now);
            ClientHelper.LogService.LogInfo("Connecting to message queue url: {0}", MessageQueue.MessageQueueUrl);

            using (var connection = MessageQueue.CreateConnection())
            {
                ClientHelper.LogService.LogInfo("Connected to message queue!");

                var subscription = connection.SubscribeAsync(CustodianCreatedEvent.MessageSubject);
                subscription.MessageHandler += SaveCustodian;
                subscription.Start();
                ClientHelper.LogService.LogInfo("Listening on subject: {0}", CustodianCreatedEvent.MessageSubject);

                _ResetEvent.WaitOne();
                connection.Close();
            }
        }

        private static void SaveCustodian(object sender, MsgHandlerEventArgs e)
        {
            ClientHelper.LogService.LogInfo("Received message, subject: {0}", e.Message.Subject);
            var eventMessage = MessageHelper.FromData<CustodianCreatedEvent>(e.Message.Data);
            ClientHelper.LogService.LogInfo("Saving new custodian, created at: {0} with event ID: {1}", eventMessage.CreatedAt, eventMessage.CorrelationId);

            var prospect = eventMessage.Custodian;
            ////using (var context = new ProductLaunchContext())
            ////{
            ////    //reload child objects:
            ////    prospect.Country = context.Countries.Single(x => x.CountryCode == prospect.Country.CountryCode);
            ////    prospect.CustodianType = context.Roles.Single(x => x.TypeCode == prospect.CustodianType.TypeCode);

            ////    context.Prospects.Add(prospect);
            ////    context.SaveChanges();
            ////}

            ClientHelper.LogService.LogInfo("STUBBED! NOT CURRENTLY SAVING.");

            ClientHelper.LogService.LogInfo("Custodian saved. Custodian First Name: {0}, Last Name: {1}", eventMessage.Custodian.FirstName, eventMessage.Custodian.LastName);
        }
    }
}