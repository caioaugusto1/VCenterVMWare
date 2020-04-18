using VSphere.Models.Base;

namespace VSphere.Models
{
    public class ServerViewModel : BaseViewModel
    {
        public string IP { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Description { get; private set; }
    }
}
