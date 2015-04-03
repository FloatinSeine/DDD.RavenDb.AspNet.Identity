
using System.Collections.Generic;
using System.Security.Claims;

namespace Sample.Domain.Model.Customer.Dtos
{
    public class AccountClaimsDto
    {
        public AccountClaimsDto()
        {
            Claims = new List<Claim>();
        }

        public string CustomerId { get; set; }
        public List<Claim> Claims { get; private set; }
    }
}
