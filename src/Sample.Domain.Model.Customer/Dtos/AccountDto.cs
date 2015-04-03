using System;


namespace Sample.Domain.Model.Customer.Dtos
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool LockedOut { get; set; }
        public DateTimeOffset? LockedOutEndDate { get; set; }

    }
}
