using System;
using Sample.Domain.DDD;

namespace Sample.Domain.Model.Customer
{
    public class LocalMembership : Entity
    {
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public int FailedPasswordMatchAttempts { get; private set; }
        public DateTime PasswordChangeDate { get; private set; }
        public DateTime? LastPasswordFailureDate { get; private set; }

        public LocalMembership()
        {
        }

        public LocalMembership(string password) : this(String.Empty, password)
        {
        }

        public LocalMembership(string salt, string password)
        {
            Password = password;
            Salt = salt;
            PasswordChangeDate = DateTime.UtcNow;
            FailedPasswordMatchAttempts = 0;
        }

        public void ResetFailedMatchAttempts()
        {
            FailedPasswordMatchAttempts = 0;
        }

    }
}
