

using Sample.Domain.CQRS.Command;
using Sample.Domain.CQRS.Command.Implementation;
using StructureMap.Configuration.DSL;

namespace Sample.Domain.DependencyResolution
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            ForSingletonOf<ICommandBus>().Use<InProcessCommandBus>();
        }

    }
}
