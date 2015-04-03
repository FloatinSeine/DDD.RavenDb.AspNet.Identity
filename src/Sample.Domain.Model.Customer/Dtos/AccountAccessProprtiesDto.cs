using System;

namespace Sample.Domain.Model.Customer.Dtos
{
    public class AccountAccessProprtiesDto
    {
        public string CustomerId { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEndDate { get; set; }
        public DateTime? LastFailureDate { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
