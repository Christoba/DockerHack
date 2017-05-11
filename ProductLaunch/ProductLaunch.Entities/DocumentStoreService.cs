namespace kCura.Hack.Data
{
    using System;

    using Raven.Client;
    using Raven.Client.Document;

    /// <summary>
    /// The Document Store Service
    /// </summary>
    public class DocumentStoreService : IDocumentStoreService
    {
        /// <summary>
        /// The store
        /// </summary>
        private static IDocumentStore store;

        /// <summary>
        /// The endpoint
        /// </summary>
        private string endpoint;

        /// <summary>
        /// The defaultDatabase
        /// </summary>
        private string defaultDatabase;

        /// <summary>
        /// The disposed backing.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentStoreService"/> class.
        /// </summary>
        /// <param name="documentStoreEndpoint">The document store endpoint.</param>
        /// <param name="defaultDatabase">The default defaultDatabase.</param>
        public DocumentStoreService(string documentStoreEndpoint, string defaultDatabase)
        {
            this.endpoint = documentStoreEndpoint;
            this.defaultDatabase = defaultDatabase;
            store = null;
        }

        /// <inheritdoc />
        public IDocumentStore Store
        {
            get { return store; }
        }

        /// <inheritdoc />ef="IDocumentStore" />.
        public IDocumentStore CreateStore()
        {
            if (this.Store != null)
            {
                return this.Store;
            }

            store = new DocumentStore() { Url = this.endpoint, DefaultDatabase = this.defaultDatabase }.Initialize();

            return this.Store;
        }

        /// <inheritdoc />
        public void StoreEntity(object entity)
        {
            if (this.Store == null)
            {
                throw new InvalidOperationException("Uninitialized document store.");
            }

            using (var session = this.Store.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (store != null)
                {
                    store.Dispose();
                    store = null;
                }
            }

            this.disposed = true;
        }
    }
}
