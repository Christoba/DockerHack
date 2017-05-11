namespace kCura.Hack.Client
{
    using System;
    using System.Threading;

    using kCura.Hack.Data;

    using NATS.Client;
    using kCura.Hack.Messaging;

    using Raven.Client;

    /// <summary>
    /// The Client Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// The document store endpoint
        /// </summary>
        private const string DocumentStoreEndpoint = @"http://172.29.4.11:8080";

        /// <summary>
        /// The default database
        /// </summary>
        private const string DefaultDatabase = @"CustodianStore";

        /// <summary>
        /// The reset event
        /// </summary>
        private static ManualResetEvent resetEvent = new ManualResetEvent(false);

        /// <summary>
        /// The document store service
        /// </summary>
        private static IDocumentStoreService documentStoreService;

        /// <summary>
        /// The entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            ClientHelper.LogService.LogInfo("Client Console Running at: {0}", DateTime.Now);
            ClientHelper.LogService.LogInfo("Connecting to message queue url: {0}", MessageQueue.MessageQueueUrl);

            using (var connection = MessageQueue.CreateConnection())
            using (documentStoreService = new DocumentStoreService(DocumentStoreEndpoint, DefaultDatabase))
            {
                ClientHelper.LogService.LogInfo("Connected to message queue!");

                ClientHelper.LogService.LogInfo("Creating document store...");
                documentStoreService.CreateStore();
                ClientHelper.LogService.LogInfo("Document store created at {0} with database {1}", DocumentStoreEndpoint, DefaultDatabase);

                var subscription = connection.SubscribeAsync(CustodianCreatedEvent.MessageSubject);
                subscription.MessageHandler += SaveCustodian;
                subscription.Start();
                ClientHelper.LogService.LogInfo("Listening on subject: {0}", CustodianCreatedEvent.MessageSubject);

                resetEvent.WaitOne();
                connection.Close();
            }
        }

        /// <summary>
        /// Saves the custodian.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MsgHandlerEventArgs"/> instance containing the event data.</param>
        private static void SaveCustodian(object sender, MsgHandlerEventArgs e)
        {
            ClientHelper.LogService.LogInfo("Received message, subject: {0}", e.Message.Subject);
            var eventMessage = MessageHelper.FromData<CustodianCreatedEvent>(e.Message.Data);
            ClientHelper.LogService.LogInfo("Saving new custodian, created at: {0} with event ID: {1}", eventMessage.CreatedAt, eventMessage.CorrelationId);

            var prospect = eventMessage.Custodian;
            documentStoreService.StoreEntity(prospect);

            ClientHelper.LogService.LogInfo("Custodian saved. Custodian First Name: {0}, Last Name: {1}", eventMessage.Custodian.FirstName, eventMessage.Custodian.LastName);
        }
    }
}