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
    public class InvestorServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoInvestorFoundReturnsInvestorNotFoundResponse()
        {
            // Arrange
            var mockInvestorRepository = GetDefaultIInvestorRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var investorId = 1;
            mockInvestorRepository.Setup(r => r.FindById(investorId))
                .Returns(Task.FromResult<Investor>(null));

            var service = new InvestorService(mockInvestorRepository.Object, mockUnitOfWork.Object);

            // Act
            InvestorResponse result = await service.GetByIdAsync(investorId);
            var message = result.Message;

            // Assert
            message.Should().Be("Investor not found");
        }

        private Mock<IInvestorRepository> GetDefaultIInvestorRepositoryInstance()
        {
            return new Mock<IInvestorRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
