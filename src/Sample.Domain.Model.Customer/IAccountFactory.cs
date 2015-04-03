
namespace Sample.Domain.Model.Customer
{
    public interface IAccountFactory
    {
        string Create(string username, string salt, string hashPassword, string activationToken);
    }
}
