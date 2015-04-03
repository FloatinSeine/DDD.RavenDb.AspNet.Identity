
namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity
{
    public class UserPropertyChange
    {
        public string ChangeType { get; private set; }
        public object ChangeValue { get; private set; }

        public UserPropertyChange(string type, object value)
        {
            ChangeType = type;
            ChangeValue = value;
        }
    }
}
