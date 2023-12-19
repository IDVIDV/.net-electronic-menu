using ElectronicMenu.DataAccess;
using ElectronicMenu.DataAccess.Entities;
using FluentAssertions;

namespace ElectronicMenu.UnitTests.Repository
{
    [TestFixture]
    [Category("Integration")]
    public class UserRepositoryTests : RepositoryTestsBase
    {
        [Test]
        public void GetAllUsersTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var users = new UserEntity[]
            {
            new UserEntity()
            {
                Login = "Test1",
                PhoneNumber = "TestNumber1",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "Test2",
                PhoneNumber = "TestNumber2",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "Test3",
                PhoneNumber = "TestNumber3",
                ExternalId = Guid.NewGuid()
            },
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            //execute
            var repository = new Repository<UserEntity>(DbContextFactory);
            var actualUsers = repository.GetAll();

            //assert        
            actualUsers.Should().BeEquivalentTo(users, options => options.Excluding(user => user.Orders));
        }

        [Test]
        public void GetAllUsersWithFilterTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var users = new UserEntity[]
            {
            new UserEntity()
            {
                Login = "YesTest1",
                PhoneNumber = "TestNumber1",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "YesTest2",
                PhoneNumber = "TestNumber2",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "NoTest3",
                PhoneNumber = "TestNumber3",
                ExternalId = Guid.NewGuid()
            },
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            //execute
            var repository = new Repository<UserEntity>(DbContextFactory);
            var actualUsers = repository.GetAll(user => user.Login.StartsWith("Yes"));

            //assert
            actualUsers.Should().BeEquivalentTo(users.Where(user => user.Login.StartsWith("Yes")), options => options.Excluding(user => user.Orders));
        }

        [Test]
        public void GetAllUsersSortedByLoginTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var users = new UserEntity[]
            {
            new UserEntity()
            {
                Login = "2Test1",
                PhoneNumber = "TestNumber1",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "3Test2",
                PhoneNumber = "TestNumber2",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "1Test3",
                PhoneNumber = "TestNumber3",
                ExternalId = Guid.NewGuid()
            },
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            //execute
            var repository = new Repository<UserEntity>(DbContextFactory);
            //var actualUsersUsingComparer = repository.GetAll(Comparer<UserEntity>.Create((user1, user2) => user1.Login.ToLower().CompareTo(user2.Login.ToLower())));
            var actualUsersUsingKeySelector = repository.GetAll(user => user.Login.ToLower());

            //assert
            //actualUsersUsingComparer.Should().BeEquivalentTo(users.OrderBy(user => user.Login.ToLower()), options => options.Excluding(user => user.Orders));
            actualUsersUsingKeySelector.Should().BeEquivalentTo(users.OrderBy(user => user.Login.ToLower()), options => options.Excluding(user => user.Orders));
            //actualUsersUsingKeySelector.Should().BeEquivalentTo(actualUsersUsingComparer, options => options.Excluding(user => user.Orders));
        }

        [Test]
        public void SaveNewUserTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var user = new UserEntity()
            {
                Login = "TestNewUser",
                PhoneNumber = "TestPhoneNumber",
                ExternalId = Guid.NewGuid()
            };
            var repository = new Repository<UserEntity>(DbContextFactory);

            //execute
            repository.Save(user);

            //assert
            var actualUser = context.Users.SingleOrDefault();
            actualUser.Should().BeEquivalentTo(user, options => options.Excluding(user => user.Orders)
                .Excluding(user => user.Id)
                .Excluding(user => user.ModificationTime)
                .Excluding(user => user.CreationTime)
                .Excluding(user => user.ExternalId));
            actualUser.Id.Should().NotBe(default);
            actualUser.ModificationTime.Should().NotBe(default);
            actualUser.CreationTime.Should().NotBe(default);
            actualUser.ExternalId.Should().NotBe(Guid.Empty);
        }

        [Test]
        public void UpdateUserTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();;

            var user = new UserEntity()
            {
                Login = "TestLogin",
                PhoneNumber = "TestPhoneNumber",
                ExternalId = Guid.NewGuid()
            };
            context.Users.Add(user);
            context.SaveChanges();

            //execute

            user.Login = "TestNewLogin";
            user.PhoneNumber= "TestNewNumber";
            var repository = new Repository<UserEntity>(DbContextFactory);
            repository.Save(user);

            //assert
            var actualUser = context.Users.SingleOrDefault();
            actualUser.Should().BeEquivalentTo(user, options => options.Excluding(user => user.Orders));
        }

        [Test]
        public void DeleteUserTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var user = new UserEntity()
            {
                Login = "TestLogin",
                PhoneNumber = "TestNumber",
                ExternalId = Guid.NewGuid()
            };
            context.Users.Add(user);
            context.SaveChanges();

            //execute

            var repository = new Repository<UserEntity>(DbContextFactory);
            repository.Delete(user);

            //assert
            context.Users.Count().Should().Be(0);
        }

        [SetUp]
        public void SetUp()
        {
            CleanUp();
        }

        [TearDown]
        public void TearDown()
        {
            CleanUp();
        }

        public void CleanUp()
        {
            using (var context = DbContextFactory.CreateDbContext())
            {
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
            }
        }
    }
}
