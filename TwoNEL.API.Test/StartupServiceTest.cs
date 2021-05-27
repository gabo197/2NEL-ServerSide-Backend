using NUnit.Framework;
using Moq;
using FluentAssertions;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;
using TwoNEL.API.Domain.Persistence.Repositories;
using TwoNEL.API.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TwoNEL.API.Test
{
    public class StartupServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoStartupFoundReturnsStartupNotFoundResponse()
        {
            // Arrange
            var mockStartupRepository = GetDefaultIStartupRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var startupId = 1;
            mockStartupRepository.Setup(r => r.FindById(startupId))
                .Returns(Task.FromResult<Domain.Models.Startup>(null));

            var service = new StartupService(mockStartupRepository.Object, mockUnitOfWork.Object);

            // Act
            StartupResponse result = await service.GetByIdAsync(startupId);
            var message = result.Message;

            // Assert
            message.Should().Be("Startup not found");
        }

        private Mock<IStartupRepository> GetDefaultIStartupRepositoryInstance()
        {
            return new Mock<IStartupRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
