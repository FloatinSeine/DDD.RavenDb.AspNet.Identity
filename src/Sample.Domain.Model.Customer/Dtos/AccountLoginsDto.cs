
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Sample.Domain.Model.Customer.Dtos
{
    public class AccountLoginsDto
    {
        public string CustomerId { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
    }
}