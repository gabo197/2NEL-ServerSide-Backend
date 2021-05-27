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
    public class FreelancerServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoFreelancerFoundReturnsFreelancerNotFoundResponse()
        {
            // Arrange
            var mockFreelancerRepository = GetDefaultIFreelancerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var freelancerId = 1;
            mockFreelancerRepository.Setup(r => r.FindById(freelancerId))
                .Returns(Task.FromResult<Freelancer>(null));

            var service = new FreelancerService(mockFreelancerRepository.Object, mockUnitOfWork.Object);

            // Act
            FreelancerResponse result = await service.GetByIdAsync(freelancerId);
            var message = result.Message;

            // Assert
            message.Should().Be("Freelancer not found");
        }

        private Mock<IFreelancerRepository> GetDefaultIFreelancerRepositoryInstance()
        {
            return new Mock<IFreelancerRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
