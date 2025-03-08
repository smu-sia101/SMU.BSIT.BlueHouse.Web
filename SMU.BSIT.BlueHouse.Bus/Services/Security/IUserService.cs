using SMU.BSIT.BlueHouse.Bus.Enums;

namespace SMU.BSIT.BlueHouse.Bus.Services.Security
{
    public interface IUserService
    {
        string Username { get; }
        bool HasPermission(Permission permission, Type type);
        bool HasRole(Role role);
    }
}
