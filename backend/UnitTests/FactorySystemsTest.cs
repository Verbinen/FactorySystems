using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using UnitTests.DataGenerator;
using WebApi.Controllers;
using WebApi.Handlers;
using WebApi.Models;

namespace UnitTests
{
    public class FactorySystemsTest
    {
        private readonly IValidator<SystemsDTO> _validatorMock;

        public FactorySystemsTest()
        {
            _validatorMock = Substitute.For<IValidator<SystemsDTO>>();
            _validatorMock.ValidateAsync(Arg.Any<SystemsDTO>()).Returns(new ValidationResult());
        }

        private FactorySystemsDbContext CreateInMemoryDbContext(string databaseName)
        {
            DbContextOptionsBuilder<FactorySystemsDbContext> options = new DbContextOptionsBuilder<FactorySystemsDbContext>()
                .UseInMemoryDatabase(databaseName);

            return new FactorySystemsDbContext(options.Options);
        }

        [Fact]
        public async Task GetAllSystems_ReturnsAllSystems()
        {
            var dbContext = CreateInMemoryDbContext("db_GetAllSystems_ReturnsAllSystems");

            dbContext.Database.EnsureCreated();

            SystemsDTO[] systems = [
                FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "CustomerSystem", costCenter: "ABC909"),
                FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "OrderSystem", costCenter: "ABC444")
                ];

            dbContext.SystemsDbSet.AddRange(systems);

            await dbContext.SaveChangesAsync();

            FactorySystemsController controller = new(dbContext, _validatorMock);

            var result = await controller.GetAllSystems();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            List<SystemsDTO> retrievedSystems = (List<SystemsDTO>)okResult.Value;

            Assert.NotNull(retrievedSystems);
            Assert.Equal(3, retrievedSystems.Count);
            Assert.Distinct(retrievedSystems);
        }

        [Fact]
        public async Task GetAllSystems_ReturnsEmptyResponse()
        {
            var dbContext = CreateInMemoryDbContext("db_GetAllSystems_ReturnsEmptyResponse");

            FactorySystemsController controller = new(dbContext, _validatorMock);

            var result = await controller.GetAllSystems();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            List<SystemsDTO> retrievedSystems = (List<SystemsDTO>)okResult.Value;

            Assert.Empty(retrievedSystems);
        }

        [Fact]
        public async Task GetSystems_ReturnsSpecificSystem()
        {
            var dbContext = CreateInMemoryDbContext("db_GetSystems_ReturnsSpecificSystem");

            dbContext.Database.EnsureCreated();

            SystemsDTO targetSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "CustomerSystem", costCenter: "ABC909");

            dbContext.SystemsDbSet.Add(targetSystem);

            await dbContext.SaveChangesAsync();

            var controller = new FactorySystemsController(dbContext, _validatorMock);

            var result = await controller.GetSystem(targetSystem.Id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var retrievedSystem = Assert.IsType<SystemsDTO>(okResult.Value);

            Assert.NotNull(retrievedSystem);
            Assert.Equivalent(targetSystem, retrievedSystem);
        }

        [Fact]
        public async Task GetSystems_ThrowsNotFoundException()
        {
            var dbContext = CreateInMemoryDbContext("db_GetSystems_ThrowsNotFoundException");

            dbContext.Database.EnsureCreated();

            await dbContext.SaveChangesAsync();

            var controller = new FactorySystemsController(dbContext, _validatorMock);

            var exception = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await controller.GetSystem(Guid.NewGuid());
            });

            AssertNotFoundException(exception);
        }
            
        [Fact]
        public async Task PostSystem_ReturnsCreated()
        {
            var dbContext = CreateInMemoryDbContext("db_PostSystem_ReturnsCreated");

            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "CustomerSystem", costCenter: "ABC909");

            FactorySystemsController controller = new(dbContext, _validatorMock);

            var result = await controller.PostSystem(newSystem);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            SystemsDTO createdSystem = (SystemsDTO)createdResult.Value;

            Assert.NotNull(createdSystem);
            newSystem.Id = createdSystem.Id;

            Assert.Equivalent(newSystem, createdSystem);
        }

        [Fact]
        public async Task PutSystem_ReturnsUpdatedSystem()
        {
            var dbContext = CreateInMemoryDbContext("db_PutSystem_ReturnsUpdatedSystem");

            SystemsDTO existentSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid());

            dbContext.SystemsDbSet.Add(existentSystem);

            await dbContext.SaveChangesAsync();

            FactorySystemsController controller = new(dbContext, _validatorMock);
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(existentSystem.Id, appName: "OrderSystem", status: "Inativo", database: "Oracle");

            var result = await controller.PutSystem(newSystem.Id, newSystem);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            SystemsDTO updatedSystem = (SystemsDTO)okResult.Value;

            Assert.NotNull(updatedSystem);
            Assert.StrictEqual(updatedSystem, newSystem);
            Assert.NotSame(updatedSystem, existentSystem);
        }

        [Fact]
        public async Task PutSystem_ThrowsNotFoundException()
        {
            var dbContext = CreateInMemoryDbContext("db_PutSystem_ThrowsNotFoundException");

            dbContext.Database.EnsureCreated();

            FactorySystemsController controller = new(dbContext, _validatorMock);
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "OrderSystem", status: "Inativo", database: "Oracle");


            var exception = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await controller.PutSystem(Guid.NewGuid(), newSystem);
            });

            AssertNotFoundException(exception);
        }

        [Fact]
        public async Task DeleteSystem_ReturnsNoContent()
        {
            var dbContext = CreateInMemoryDbContext("db_DeleteSystem_ReturnsNoContent");

            dbContext.Database.EnsureCreated();

            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "OrderSystem", status: "Inativo", database: "Oracle");

            dbContext.SystemsDbSet.Add(newSystem);
            await dbContext.SaveChangesAsync();

            Assert.Contains(newSystem.Id, dbContext.SystemsDbSet.Select(x => x.Id));

            FactorySystemsController controller = new(dbContext, _validatorMock);

            var result = await controller.DeleteSystem(newSystem.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.DoesNotContain(newSystem.Id, dbContext.SystemsDbSet.Select(x => x.Id));
        }

        [Fact]
        public async Task DeleteSystem_ThrowsNotFoundException()
        {
            var dbContext = CreateInMemoryDbContext("db_DeleteSystem_ThrowsNotFoundException");

            dbContext.Database.EnsureCreated();

            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "OrderSystem", status: "Inativo", database: "Oracle");

            dbContext.SystemsDbSet.Add(newSystem);
            await dbContext.SaveChangesAsync();

            Assert.Contains(newSystem.Id, dbContext.SystemsDbSet.Select(x => x.Id));

            FactorySystemsController controller = new(dbContext, _validatorMock);

            var exception = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await controller.DeleteSystem(Guid.NewGuid());
            });

            AssertNotFoundException(exception);
        }

        private void AssertNotFoundException(ApiException exception)
        {
            Assert.Equal("Entity not found", exception.Message);
            Assert.Equal(StatusCodes.Status404NotFound, exception.StatusCode);
        }
    }
}