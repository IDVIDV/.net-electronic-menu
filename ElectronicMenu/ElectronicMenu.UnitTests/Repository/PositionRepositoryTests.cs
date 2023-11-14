using ElectronicMenu.DataAccess.Entities;
using ElectronicMenu.DataAccess;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMenu.UnitTests.Repository
{
    [TestFixture]
    [Category("Integration")]
    public class PositionRepositoryTests : RepositoryTestsBase
    {
        [Test]
        public void GetAllPositionsTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var positions = new PositionEntity[]
            {
            new PositionEntity()
            {
                PositionName = "TestName1",
                ImgLink = "TestLink1",
                Price = 100,
                Weight = 100,
                Calories = 100,
                IsVegan = 0,
                Ingridients = "TestIng1",
                ExternalId = Guid.NewGuid()
            },
            new PositionEntity()
            {
                PositionName = "TestName2",
                ImgLink = "TestLink2",
                Price = 200,
                Weight = 200,
                Calories = 200,
                IsVegan = 0,
                Ingridients = "TestIng2",
                ExternalId = Guid.NewGuid()
            },
            new PositionEntity()
            {
                PositionName = "TestName3",
                ImgLink = "TestLink3",
                Price = 300,
                Weight = 300,
                Calories = 300,
                IsVegan = 1,
                Ingridients = "TestIng3",
                ExternalId = Guid.NewGuid()
            },
            };
            context.Positions.AddRange(positions);
            context.SaveChanges();

            //execute
            var repository = new Repository<PositionEntity>(DbContextFactory);
            var actualPositions = repository.GetAll();

            //assert        
            actualPositions.Should().BeEquivalentTo(positions, options => options.Excluding(position => position.PositionInOrders));
        }

        [Test]
        public void GetAllPositionsWithFilterTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var positions = new PositionEntity[]
            {
            new PositionEntity()
            {
                PositionName = "TestName1",
                ImgLink = "TestLink1",
                Price = 100,
                Weight = 100,
                Calories = 100,
                IsVegan = 0,
                Ingridients = "TestIng1",
                ExternalId = Guid.NewGuid()
            },
            new PositionEntity()
            {
                PositionName = "TestName2",
                ImgLink = "TestLink2",
                Price = 200,
                Weight = 200,
                Calories = 200,
                IsVegan = 1,
                Ingridients = "TestIng2",
                ExternalId = Guid.NewGuid()
            },
            new PositionEntity()
            {
                PositionName = "TestName3",
                ImgLink = "TestLink3",
                Price = 300,
                Weight = 300,
                Calories = 300,
                IsVegan = 0,
                Ingridients = "TestIng3",
                ExternalId = Guid.NewGuid()
            },
            };
            context.Positions.AddRange(positions);
            context.SaveChanges();

            //execute
            var repository = new Repository<PositionEntity>(DbContextFactory);
            var actualPositions = repository.GetAll(position => position.IsVegan == 0);

            //assert
            actualPositions.Should().BeEquivalentTo(positions.Where(position => position.IsVegan == 0), options => options.Excluding(position => position.PositionInOrders));
        }

        [Test]
        public void GetAllPositionsSortedByPriceTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var positions = new PositionEntity[]
            {
            new PositionEntity()
            {
                PositionName = "TestName1",
                ImgLink = "TestLink1",
                Price = 100,
                Weight = 100,
                Calories = 100,
                IsVegan = 0,
                Ingridients = "TestIng1",
                ExternalId = Guid.NewGuid()
            },
            new PositionEntity()
            {
                PositionName = "TestName2",
                ImgLink = "TestLink2",
                Price = 200,
                Weight = 200,
                Calories = 200,
                IsVegan = 1,
                Ingridients = "TestIng2",
                ExternalId = Guid.NewGuid()
            },
            new PositionEntity()
            {
                PositionName = "TestName3",
                ImgLink = "TestLink3",
                Price = 300,
                Weight = 300,
                Calories = 300,
                IsVegan = 0,
                Ingridients = "TestIng3",
                ExternalId = Guid.NewGuid()
            },
            };
            context.Positions.AddRange(positions);
            context.SaveChanges();

            //execute
            var repository = new Repository<PositionEntity>(DbContextFactory);
            //var actualPositionsUsingComparer = repository.GetAll(Comparer<PositionEntity>.Create((position1, position2) =>
            //                                                                                      position1.Price.CompareTo(position2.Price)));
            var actualPositionsUsingKeySelector = repository.GetAll(position => position.Price);

            //assert
            //actualPositionsUsingComparer.Should().BeEquivalentTo(positions.OrderBy(position => position.Price), options => options.Excluding(position => position.PositionInOrders));
            actualPositionsUsingKeySelector.Should().BeEquivalentTo(positions.OrderBy(position => position.Price), options => options.Excluding(position => position.PositionInOrders));
            //actualPositionsUsingKeySelector.Should().BeEquivalentTo(actualPositionsUsingComparer, options => options.Excluding(position => position.PositionInOrders));
        }

        [Test]
        public void SaveNewPositionTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var position = new PositionEntity()
            {
                PositionName = "TestName",
                ImgLink = "TestLink",
                Price = 100,
                Weight = 100,
                Calories = 100,
                IsVegan = 0,
                Ingridients = "TestIng",
                ExternalId = Guid.NewGuid()
            };
            var repository = new Repository<PositionEntity>(DbContextFactory);

            //execute
            repository.Save(position);

            //assert
            var actualPosition = context.Positions.SingleOrDefault();
            actualPosition.Should().BeEquivalentTo(position, options => options.Excluding(position => position.PositionInOrders)
                .Excluding(position => position.Id)
                .Excluding(position => position.ModificationTime)
                .Excluding(position => position.CreationTime)
                .Excluding(position => position.ExternalId));
            actualPosition.Id.Should().NotBe(default);
            actualPosition.ModificationTime.Should().NotBe(default);
            actualPosition.CreationTime.Should().NotBe(default);
            actualPosition.ExternalId.Should().NotBe(Guid.Empty);
        }

        [Test]
        public void UpdatePositionTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext(); ;

            var position = new PositionEntity()
            {
                PositionName = "TestName",
                ImgLink = "TestLink",
                Price = 100,
                Weight = 100,
                Calories = 100,
                IsVegan = 0,
                Ingridients = "TestIng",
                ExternalId = Guid.NewGuid()
            };
            context.Positions.Add(position);
            context.SaveChanges();

            //execute
            position.PositionName= "TestNewName";
            position.Price = 1000;
            var repository = new Repository<PositionEntity>(DbContextFactory);
            repository.Save(position);

            //assert
            var actualPosition = context.Positions.SingleOrDefault();
            actualPosition.Should().BeEquivalentTo(position, options => options.Excluding(position => position.PositionInOrders));
        }

        [Test]
        public void DeletePositionTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            var position = new PositionEntity()
            {
                PositionName = "TestName",
                ImgLink = "TestLink",
                Price = 100,
                Weight = 100,
                Calories = 100,
                IsVegan = 0,
                Ingridients = "TestIng",
                ExternalId = Guid.NewGuid()
            };
            context.Positions.Add(position);
            context.SaveChanges();

            //execute

            var repository = new Repository<PositionEntity>(DbContextFactory);
            repository.Delete(position);

            //assert
            context.Positions.Count().Should().Be(0);
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
                context.Positions.RemoveRange(context.Positions);
                context.SaveChanges();
            }
        }
    }
}
