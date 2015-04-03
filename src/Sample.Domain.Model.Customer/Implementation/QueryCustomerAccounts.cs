using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Domain.CQRS.Specifications;
using Sample.Domain.Model.Customer.Dtos;
using Sample.Domain.Model.Customer.Specifications;

namespace Sample.Domain.Model.Customer.Implementation
{
    public class QueryCustomerAccounts : IQueryCustomerAccounts, IDisposable
    {
        private bool _disposed = false;
        private IAccountRepository _repository;

        public QueryCustomerAccounts(IAccountRepository repository)
        {
            _repository = repository;
        }

        public CustomerAccount FindById(string id)
        {
            return _repository.Get(id);
        }

        public CustomerAccount FindByUserName(string username)
        {
            var spec = new CustomerAccountHasUsername(username);
            return FindSingleCustomerAccount(spec);
        }

        public CustomerAccount FindByLinkedAccount2(string provider, string providerId)
        {
            var spec = new CustomerAccountHasLinkedAccount(provider, providerId);
            return FindSingleCustomerAccount(spec);
        }

        public AccountDto FindByLinkedAccount(string provider, string providerId)
        {
            return _repository.FetchCustomerAccountBySocialLogon(provider, providerId);
            //var spec = new CustomerAccountHasLinkedAccount(provider, providerId);
            //return FindSingleCustomerAccount(spec);
        }

        public string FetchCustomerAccountPassword(string accountId)
        {
            var dto = _repository.FetchCustomerAccountLocalAccountPassword(accountId);
            return dto.Password;
        }

        public AccountDto FetchCustomerAccountDetails(string accountId)
        {
            return _repository.FetchCustomerAccountDetails(accountId);
        }

        public AccountClaimsDto FetchCustomerAccountClaims(string accountId)
        {
            return _repository.FetchCustomerAccountClaims(accountId);
        }

        public AccountLoginsDto FetchCustomerAccountSocialLogins(string accountId)
        {
            return _repository.FetchCustomerAccountSocialLogins(accountId);
        }

        public AccountAccessProprtiesDto FetchCustomerAccountAccessProperties(string accountId)
        {
            return _repository.FetchCustomerAccountAccessProperties(accountId);
        }

        private CustomerAccount FindSingleCustomerAccount(ISpecification<CustomerAccount> specification)
        {
            var accounts = FindBySpecification(specification);
            if (accounts.Count > 1)
            {
                throw new Exception("Too many accounts found. Count = " + accounts.Count);
            }
            return accounts.SingleOrDefault();
        }

        private IList<CustomerAccount> FindBySpecification(ISpecification<CustomerAccount> specification)
        {
            return _repository.FindAll(specification).ToList();
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
