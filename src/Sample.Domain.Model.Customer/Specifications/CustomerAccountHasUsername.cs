using System;
using System.Linq.Expressions;
using Sample.Domain.CQRS.Specifications;

namespace Sample.Domain.Model.Customer.Specifications
{
    public class CustomerAccountHasUsername : Specification<CustomerAccount>
    {
        public string Username { get; private set; }

        public CustomerAccountHasUsername(string username)
        {
            Username = username;
        }

        public override Expression<Func<CustomerAccount, bool>> ToExpression
        {
            get { return (x => x.Username.Equals(Username)); }
        }

    }
}
