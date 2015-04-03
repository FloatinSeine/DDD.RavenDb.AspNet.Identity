using System;
using Raven.Client;

namespace Sample.Domain.Persistence.Raven.Implementation
{
    public class RavenSessionFactory : IRavenSessionFactory
    {
        private readonly IDocumentStore _documentStore;

        public RavenSessionFactory(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore", "IDocumentStore argument can not be null");
            if (string.IsNullOrEmpty(documentStore.Url)) throw new Exception("IDocumentStore URL is not configured.");
            
            _documentStore = documentStore;
            _documentStore.Initialize();
        }

        public IDocumentSession CreateSession()
        {
            return _documentStore.OpenSession();
        }
    }
}
