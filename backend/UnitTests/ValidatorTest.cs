using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTests.DataGenerator;
using WebApi.Controllers;
using WebApi.Handlers;
using WebApi.Models;
using WebApi.Validators;

namespace UnitTests
{
    public class ValidatorTest
    {
        private readonly SystemsValidator _validator;
        public ValidatorTest()  
        {
            _validator = new SystemsValidator();
        }

        [Theory]
        [InlineData("ABC112")]
        [InlineData("ABC306")]
        [InlineData("ABC909")]
        [InlineData("ABC444")]
        public async Task Validator_IsValid_WhenValidCostCenter(string costCenter)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), costCenter: costCenter);

            var result = await _validator.ValidateAsync(newSystem);
            
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("ABCD")]
        [InlineData("1234")]
        [InlineData("ABC123")]
        [InlineData("ABC#$%")]
        public async Task Validator_IsInvalid_WhenInvalidCostCenter(string costCenter)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), costCenter: costCenter);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Please provide a valid Cost Center ('ABC112', 'ABC306', 'ABC909', 'ABC444')", result.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("Ativo")]
        [InlineData("Inativo")]
        [InlineData("Bloqueada")]
        public async Task Validator_IsValid_WhenValidStatus(string status)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), status: status);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.True(result.IsValid);
        }
        
        [Theory]
        [InlineData("Ativado")]
        [InlineData("Inativado")]
        [InlineData("Bloqueado")]
        public async Task Validator_IsInvalid_WhenInvalidStatus(string status)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), status: status);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Please provide a valid Status ('Ativo', 'Inativo', 'Bloqueada')", result.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("SQL Server")]
        [InlineData("Oracle")]
        [InlineData("MySQL")]
        public async Task Validator_IsValid_WhenValidDatabase(string database)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), database: database);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("MongoDb")]
        [InlineData("MariaDb")]
        [InlineData("SqLite")]
        public async Task Validator_IsInvalid_WhenInvalidDatabase(string database)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), database: database);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Please provide a valid Database ('SQL Server', 'Oracle', 'MySQL')", result.Errors.First().ErrorMessage);
        }
        
        [Theory]
        [InlineData("email1@company.com", "email2@company.com.br")]
        [InlineData("email@company.gov.br")]
        public async Task Validator_IsValid_WhenValidEmailList(params string[] emailList)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid());
            newSystem.EmailSupport = emailList;

            var result = await _validator.ValidateAsync(newSystem);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("email1@company", "email")]
        [InlineData("email2-company.com.br")]
        public async Task Validator_IsInvalid_WhenInvalidEmailList(params string[] emailList)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid());
            newSystem.EmailSupport = emailList;

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Please provide a valid list of email addresses", result.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Validator_IsValid_WhenValidApplicationName()
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: "Some name");

            var result = await _validator.ValidateAsync(newSystem);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Validator_IsInvalid_WhenInvalidApplicationCode(string appName)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appName: appName);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Application Name is required", result.Errors.First().ErrorMessage);
        }
        
        [Fact]
        public async Task Validator_IsValid_WhenValidApplicationCode()
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appCode: "Some code");

            var result = await _validator.ValidateAsync(newSystem);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Validator_IsInvalid_WhenInvalidApplicationName(string appCode)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), appCode: appCode);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Application Code is required", result.Errors.First().ErrorMessage);
        }
        
        [Fact]
        public async Task Validator_IsValid_WhenValidInstallationLocation()
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), location: "Some location");

            var result = await _validator.ValidateAsync(newSystem);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Validator_IsInvalid_WhenInvalidInstallationLocation(string location)
        {
            SystemsDTO newSystem = FactorySystemsDataGenerator.CreateSystem(Guid.NewGuid(), location: location);

            var result = await _validator.ValidateAsync(newSystem);

            Assert.False(result.IsValid);
            Assert.Equal("Installation Location is required", result.Errors.First().ErrorMessage);
        }
    }
}
