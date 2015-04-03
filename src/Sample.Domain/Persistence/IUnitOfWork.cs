using System;

namespace Sample.Domain.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
