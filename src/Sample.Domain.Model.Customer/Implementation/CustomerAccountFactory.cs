using System;
using System.Linq;
using Sample.Domain.Model.Customer.Specifications;

namespace Sample.Domain.Model.Customer.Implementation
{
    public class CustomerAccountFactory : IAccountFactory, IDisposable
    {
        private IAccountRepository _repository;
        private bool _disposed = false;

        public CustomerAccountFactory(IAccountRepository repository)
        {
            _repository = repository;
        }

        public string Create(string username, string salt, string password, string activationToken)
        {
            var spec = new CustomerAccountHasUsername(username);
            var accs = _repository.FindAll(spec);

            if (accs.Any())
            {
                //Account with username already exists.
                throw new Exception("Can not create CustomerAccount");
            }

            var acc = new CustomerAccount();
            acc.CreateLocalMembership(username, password);

            _repository.Store(acc);

            return acc.Id;
        }

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
