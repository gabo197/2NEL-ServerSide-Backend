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
    public class EnterpriseServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoEnterpriseFoundReturnsEnterpriseNotFoundResponse()
        {
            // Arrange
            var mockEnterpriseRepository = GetDefaultIEnterpriseRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var enterpriseId = 1;
            mockEnterpriseRepository.Setup(r => r.FindById(enterpriseId))
                .Returns(Task.FromResult<Enterprise>(null));

            var service = new EnterpriseService(mockEnterpriseRepository.Object, mockUnitOfWork.Object);

            // Act
            EnterpriseResponse result = await service.GetByIdAsync(enterpriseId);
            var message = result.Message;

            // Assert
            message.Should().Be("Enterprise not found");
        }

        private Mock<IEnterpriseRepository> GetDefaultIEnterpriseRepositoryInstance()
        {
            return new Mock<IEnterpriseRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
