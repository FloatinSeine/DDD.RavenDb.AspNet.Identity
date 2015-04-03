using Raven.Client;

namespace Sample.Domain.Persistence.Raven
{
    public interface IRavenSessionFactory
    {
        IDocumentSession CreateSession();
    }
}
