namespace kCura.Hack.Data
{
    using System;

    using Raven.Client;

    /// <summary>
    /// The interface for the document store service.
    /// </summary>
    public interface IDocumentStoreService : IDisposable
    {
        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        IDocumentStore Store { get; }

        /// <summary>
        /// Creates the store.
        /// </summary>
        /// <returns>
        /// The <see cref="IDocumentStore"/>.
        /// </returns>
        IDocumentStore CreateStore();

        /// <summary>
        /// Stores the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void StoreEntity(object entity);
    }
}
