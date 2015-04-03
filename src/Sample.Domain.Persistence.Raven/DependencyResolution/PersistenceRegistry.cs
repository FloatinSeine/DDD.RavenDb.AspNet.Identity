
using Sample.Domain.Model.Customer;
using Sample.Domain.Persistence.Raven.Customer;
using Sample.Domain.Persistence.Raven.Implementation;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Sample.Domain.Persistence.Raven.DependencyResolution
{
    public class PersistenceRegistry : Registry
    {
        public PersistenceRegistry()
        {

            For<IRavenSessionFactoryBuilder>()
                .Use<RavenSessionFactoryBuilder>()
                .Ctor<string>("connectionString").Is("MembershipsDataStore").Named("MembershipStore");

            //For<IRavenSessionFactoryBuilder>()
            //    .Use<RavenSessionFactoryBuilder>()
            //    .Ctor<string>("connectionString").Is("InventoryDataStore").Named("InventoryStore");

            For<IRavenSessionFactory>()
                .Use("Fetch Raven Session Factory", ctx => ctx.GetInstance<IRavenSessionFactoryBuilder>("MembershipStore").GetSessionFactory()).Named("MembershipFactory");


            For<IAccountRepository>(Lifecycles.Unique).Use<CustomerAccountRepository>().Ctor<IRavenSessionFactory>().Named("MembershipFactory");

        }

    }
}
