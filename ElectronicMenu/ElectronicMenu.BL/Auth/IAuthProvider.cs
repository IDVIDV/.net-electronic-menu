using ElectronicMenu.BL.Auth.Entities;

namespace ElectronicMenu.BL.Auth
{
    public interface IAuthProvider
    {
        Task<TokensResponse> AuthorizeUser(string login, string password);
        Task RegisterUser(string login, string password);
    }
}
