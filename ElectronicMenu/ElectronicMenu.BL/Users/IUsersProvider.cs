using ElectronicMenu.BL.Users.Entities;

namespace ElectronicMenu.BL.Users
{
    public interface IUsersProvider
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUser(Guid userId);
    }
}
