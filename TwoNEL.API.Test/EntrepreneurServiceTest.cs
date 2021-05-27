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
    public class EntrepreneurServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoEntrepreneurFoundReturnsEntrepreneurNotFoundResponse()
        {
            // Arrange
            var mockEntrepreneurRepository = GetDefaultIEntrepreneurRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var entrepreneurId = 1;
            mockEntrepreneurRepository.Setup(r => r.FindById(entrepreneurId))
                .Returns(Task.FromResult<Entrepreneur>(null));

            var service = new EntrepreneurService(mockEntrepreneurRepository.Object, mockUnitOfWork.Object);

            // Act
            EntrepreneurResponse result = await service.GetByIdAsync(entrepreneurId);
            var message = result.Message;

            // Assert
            message.Should().Be("Entrepreneur not found");
        }

        private Mock<IEntrepreneurRepository> GetDefaultIEntrepreneurRepositoryInstance()
        {
            return new Mock<IEntrepreneurRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
