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
    public class TagServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoTagFoundReturnsTagNotFoundResponse()
        {
            // Arrange
            var mockTagRepository = GetDefaultITagRepositoryInstance();
            var mockStartupTagRepository = GetDefaultIStartupTagRepositoryInstance();
            var mockProfileTagRepository = GetDefaultIProfileTagRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var tagId = 1;
            mockTagRepository.Setup(r => r.FindById(tagId))
                .Returns(Task.FromResult<Tag>(null));

            var service = new TagService(mockTagRepository.Object, mockStartupTagRepository.Object, mockProfileTagRepository.Object, mockUnitOfWork.Object);

            // Act
            TagResponse result = await service.GetByIdAsync(tagId);
            var message = result.Message;

            // Assert
            message.Should().Be("Tag not found");
        }

        private Mock<ITagRepository> GetDefaultITagRepositoryInstance()
        {
            return new Mock<ITagRepository>();
        }

        private Mock<IStartupTagRepository> GetDefaultIStartupTagRepositoryInstance()
        {
            return new Mock<IStartupTagRepository>();
        }

        private Mock<IProfileTagRepository> GetDefaultIProfileTagRepositoryInstance()
        {
            return new Mock<IProfileTagRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
