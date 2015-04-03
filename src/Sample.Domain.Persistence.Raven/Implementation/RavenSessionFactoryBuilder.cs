
using System;
using Raven.Client.Document;

namespace Sample.Domain.Persistence.Raven.Implementation
{
    public class RavenSessionFactoryBuilder : IRavenSessionFactoryBuilder
    {
        private readonly string _connectionStringName;
        private IRavenSessionFactory _ravenSessionFactory;

        public RavenSessionFactoryBuilder(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString", "Connection Name can not be Null or Empty String");
            _connectionStringName = connectionString;
        }

        public IRavenSessionFactory GetSessionFactory()
        {
            return _ravenSessionFactory ?? (_ravenSessionFactory = CreateSessionFactory(_connectionStringName));
        }

        private static IRavenSessionFactory CreateSessionFactory(string connectionName)
        {
            return new RavenSessionFactory(new DocumentStore
            {
                ConnectionStringName = connectionName
            });
        }
    }
}
