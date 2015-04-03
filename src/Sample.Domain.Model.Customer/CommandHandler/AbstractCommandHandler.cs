using System;

namespace Sample.Domain.Model.Customer.CommandHandler
{
    public abstract class AbstractCommandHandler : IDisposable
    {
        private IAccountRepository _repository;
        private bool _disposed = false;

        protected AbstractCommandHandler(IAccountRepository repository)
        {
            if (repository == null) throw new ArgumentNullException("repository", "Repository can not be null");

            _repository = repository;
        }

        protected IAccountRepository AccountRepository { get { return _repository; } }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
