using SMU.BSIT.BlueHouse.Bus.Enums;

namespace SMU.BSIT.BlueHouse.Bus.Services.Security
{
    public class UserService : IUserService
    {
        public string Username => "Bogart";

        public bool HasPermission(Permission permission, Type type)
        {
            return true;
            //TODO: Implement permission checking
        }

        public bool HasRole(Role role)
        {
            return true;
            //TODO: Implement permission checking
        }
    }
}
