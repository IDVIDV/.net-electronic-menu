using ElectronicMenu.BL.Auth.Entities;
using ElectronicMenu.DataAccess;
using ElectronicMenu.DataAccess.Entities;
using ElectronicMenu.Services.Controllers.Positions.Entities;
using ElectronicMenu.Services.Controllers.Users.Entities;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Net.Http.Json;

namespace ElectronicMenu.Services.UnitTests.Users.Authorization
{
    public class RegisterUserTests : ElectronicMenuServicesTestsBaseClass
    {
        [Test]
        public async Task SuccessfullRegisterTest()
        {
            //prepare
            var user = new UserEntity()
            {
                Id = 1,
                ExternalId = Guid.NewGuid(),
                UserName = "test@test",
                PhoneNumber = "71111111111"
            };
            var password = "Password1@";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

            //Сначала надо удалить юзера, если есть в базе
            var foundUser = userRepository.GetAll().FirstOrDefault(x => x.UserName.ToLower() == user.UserName.ToLower());
            if (foundUser != null)
            {
                userRepository.Delete(foundUser);
            }

            //По идее делает то же самое
            //var foundUser1 = await userManager.FindByNameAsync(user.UserName);
            //if (foundUser1 != null)
            //{
            //    var result = await userManager.DeleteAsync(foundUser1);
            //    result.Succeeded.Should().BeTrue();
            //}

            //execute
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ElectronicMenuApiEndpoints.RegisterUserEndpoint);
            request.Content = JsonContent.Create(new RegisterUserRequest
            {
                Login = user.UserName,
                Password = password,
                PhoneNumber = user.PhoneNumber
            });

            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            userManager.FindByNameAsync(user.UserName).Should().NotBeNull();
        }

        [Test]
        public async Task RegisterWithExistingLoginTest()
        {
            //prepare
            var user = new UserEntity()
            {
                ExternalId = Guid.NewGuid(),
                UserName = "existing",
                PhoneNumber = "71111111111"
            };
            var password = "Password1@";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var findResult = await userManager.FindByNameAsync(user.UserName);
            if (findResult == null)
            {
                var result = await userManager.CreateAsync(user, password);

                //assert
                result.Succeeded.Should().BeTrue();
            }


            //execute
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ElectronicMenuApiEndpoints.RegisterUserEndpoint);
            request.Content = JsonContent.Create(new RegisterUserRequest
            {
                Login = user.UserName,
                Password = password,
                PhoneNumber = user.PhoneNumber
            });
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        //Тест на неверные данные, логин и пароль - обязательные, телефон - не обязательный, логин >= 4 символов, пароль >=6 символов
        [Test]
        [TestCase("", "", null)]  //Ничего логина и пароля
        [TestCase("validlog", "", null)]  //Нет пароля
        [TestCase("", "validpas1@", null)]   //Нет логина
        [TestCase("", "validpas1@", "71111111111")] //Нет логина, телефон заполнен правильно
        [TestCase("validlog", "", "71111111111")] //Нет пароля, телефон заполнен правильно
        [TestCase("validlog", "validpas1@", "invalidphone")] //Есть все, но телефон заполнен неправильно
        [TestCase("inv", "validpas1@", "71111111111")]  //Логин есть, но <4 символов
        [TestCase("validlog", "inv", "71111111111")]    //Пароль есть, но <6 символов
        public async Task RegisterWithInvalidDataTest(String login, String password, String phoneNumber)
        {
            //prepare
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ElectronicMenuApiEndpoints.RegisterUserEndpoint);
            request.Content = JsonContent.Create(new RegisterUserRequest
            {
                Login = login,
                Password = password,
                PhoneNumber = phoneNumber
            });
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest); // with some message
        }
    }
}
