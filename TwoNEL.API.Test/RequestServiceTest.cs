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
    public class RequestServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoRequestFoundReturnsRequestNotFoundResponse()
        {
            // Arrange
            var mockRequestRepository = GetDefaultIRequestRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var requestId = 1;
            mockRequestRepository.Setup(r => r.FindById(requestId))
                .Returns(Task.FromResult<Request>(null));

            var service = new RequestService(mockRequestRepository.Object, mockUnitOfWork.Object);

            // Act
            RequestResponse result = await service.GetByIdAsync(requestId);
            var message = result.Message;

            // Assert
            message.Should().Be("Request not found");
        }

        private Mock<IRequestRepository> GetDefaultIRequestRepositoryInstance()
        {
            return new Mock<IRequestRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
