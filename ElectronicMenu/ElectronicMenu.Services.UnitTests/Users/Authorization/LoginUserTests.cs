using ElectronicMenu.BL.Auth.Entities;
using ElectronicMenu.DataAccess;
using ElectronicMenu.DataAccess.Entities;
using ElectronicMenu.Services.Controllers.Positions.Entities;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace ElectronicMenu.Services.UnitTests.Users.Authorization
{
    public class LoginUserTests : ElectronicMenuServicesTestsBaseClass
    {
        [Test]
        public async Task SuccessfullLoginTest()
        {
            //prepare
            var user = new UserEntity()
            {
                ExternalId = Guid.NewGuid(),    //Почему-то ExternalId всегда нулевой генерировался
                UserName = "test@test2",        //На каждом повторном тесте база не пустая, и поэтому первый тест создает юзера,
                                                //а дальше Create уже натыкается на него при повторном запуске
                PhoneNumber = "1234567890"
            };
            var password = "Password1@";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

            var findResult = await userManager.FindByNameAsync(user.UserName);
            if (findResult is null)
            {
                var result = await userManager.CreateAsync(user, password);
                result.Succeeded.Should().BeTrue(); //Вот благодаря этой строчке стало понятно, что юзер не создается, если он есть
            }

            //execute
            var query = $"?login={user.UserName}&password={password}";
            var requestUri = ElectronicMenuApiEndpoints.AuthorizeUserEndpoint + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            //assert ответ положительный
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //assert токены есть
            var responseContentJson = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<TokensResponse>(responseContentJson);
            content.Should().NotBeNull();
            content.AccessToken.Should().NotBeNull();
            content.RefreshToken.Should().NotBeNull();

            //assert токены валидны
            //Тест на создание позиции, потому что на получение всех позиций у меня не нужна авторизация
            var requestToCreateNewPosition =
                new HttpRequestMessage(HttpMethod.Get, ElectronicMenuApiEndpoints.CreateNewPositionEndpoint);

            requestToCreateNewPosition.Content = JsonContent.Create(new CreatePositionRequest
            {
                PositionName = "string",
                ImgLink = "string",
                Price = 0,
                Weight = 0,
                Calories = 0,
                IsVegan = 0,
                Ingridients = "string"
            });

            var clientWithToken = TestHttpClient;
            clientWithToken.SetBearerToken(content.AccessToken); //Тут был просто client, разве не должен быть clientWithToken?,
                                                                 //понятно, что уже существующий client потом не используется и его можно изменить
                                                                 //но зачем тогда было делать отдельную переменную
            var createNewPositionResponse = await clientWithToken.SendAsync(requestToCreateNewPosition);
            createNewPositionResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task LoginAsUnexistingUserTest()
        {
            //prepare
            var login = "not_existing@mail.ru";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();

            var user = userRepository.GetAll().FirstOrDefault(x => x.UserName.ToLower() == login.ToLower());
            if (user != null)
            {
                userRepository.Delete(user);
            }
            var password = "password";

            //execute
            var query = $"?login={login}&password={password}";
            var requestUri = ElectronicMenuApiEndpoints.AuthorizeUserEndpoint + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task LoginWithWrongPasswordTest()
        {
            //prepare
            var user = new UserEntity()
            {
                ExternalId = Guid.NewGuid(),    //Почему-то ExternalId всегда нулевой генерировался
                UserName = "test@test2",        //На каждом повторном тесте база не пустая, и поэтому первый тест создает юзера,
                                                //а дальше Create уже натыкается на него при повторном запуске
                PhoneNumber = "1234567890"
            };
            var password = "Password1@";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

            var findResult = await userManager.FindByNameAsync(user.UserName);
            if (findResult is null)
            {
                var result = await userManager.CreateAsync(user, password);
                result.Succeeded.Should().BeTrue(); //Вот благодаря этой строчке стало понятно, что юзер не создается, если он есть
            }
            var incorrect_password = "kvhdbkvhbk";

            //execute
            var query = $"?email={user.UserName}&password={incorrect_password}";
            var requestUri = ElectronicMenuApiEndpoints.AuthorizeUserEndpoint + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("qwe", "")]
        [TestCase("test@test", "")]
        [TestCase("", "password")]
        public async Task InvalidLoginOrPasswordTest(string login, string password)
        {
            //execute
            var query = $"?login={login}&password={password}";
            var requestUri = ElectronicMenuApiEndpoints.AuthorizeUserEndpoint + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
