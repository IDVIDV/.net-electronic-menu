using Duende.IdentityServer.Models;
using ElectronicMenu.BL.Auth.Entities;
using ElectronicMenu.DataAccess.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using static Duende.IdentityServer.Models.IdentityResources;

namespace ElectronicMenu.BL.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _identityServerUri;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthProvider(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager,
        IHttpClientFactory httpClientFactory,
        string identityServerUri,
        string clientId,
        string clientSecret)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityServerUri = identityServerUri;
            _httpClientFactory = httpClientFactory;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<TokensResponse> AuthorizeUser(string login, string password)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user is null)
            {
                throw new Exception();
            }

            var verificationPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!verificationPasswordResult.Succeeded)
            {
                throw new Exception();
            }

            var client = _httpClientFactory.CreateClient();
            var discoveryDoc = await client.GetDiscoveryDocumentAsync(_identityServerUri);
            if (discoveryDoc.IsError)
            {
                throw new Exception();
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDoc.TokenEndpoint,
                GrantType = GrantType.ResourceOwnerPassword,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                UserName = user.UserName,
                Password = password,
                Scope = "api offline_access"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception();
            }

            return new TokensResponse()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            };
        }

        public async Task RegisterUser(RegisterUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);
            if (!(user is null))
            {
                throw new Exception();  //Пользователь уже есть
            }

            UserEntity userEntity = new UserEntity()
            {
                UserName = model.Login,
                PhoneNumber = model.PhoneNumber
            };

            var identityResult =  _userManager.CreateAsync(user, model.Password);
            if (!identityResult.IsCompletedSuccessfully)
            {
                throw new Exception();
            }
        }
    }
}
