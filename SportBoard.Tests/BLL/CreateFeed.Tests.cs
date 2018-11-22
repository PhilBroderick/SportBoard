using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit;
using NUnit.Framework;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.BLL;

namespace SportBoard.Tests.BLL
{
    [TestFixture]
    public class CreateFeedTests
    {
        private Mock<IFeedRepository> _feedRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Feed _feedMock;

        [SetUp]
        public void SetUp()
        {
            _feedRepository = new Mock<IFeedRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _feedMock = new Feed
            {
                FeedId = 1,
                FeedName = "abc"
            };
        }

        [TestCase]

        public void TryCreateFeed_ExistingFeedWithSameName_ReturnFalse()
        {
            _feedRepository.Setup(fr => fr.SearchIfFeedExists(_feedMock.FeedName)).Returns(true);

            _unitOfWork.Setup(uow => uow.Feeds.SearchIfFeedExists(_feedMock.FeedName));
            
            var feed = new CreateFeed(_feedRepository.Object, _unitOfWork.Object);
            var result = feed.TryCreateFeed(new Feed { FeedName = "abc"});

            Assert.That(result, Is.False);
        }

        [TestCase]
        public void TryCreateFeed_NoExistingFeedsWithName_ReturnTrue()
        {
            _feedRepository.Setup(fr => fr.SearchIfFeedExists(_feedMock.FeedName)).Returns(false);

            _unitOfWork.Setup(uow => uow.Feeds.SearchIfFeedExists(_feedMock.FeedName));

            var feed = new CreateFeed(_feedRepository.Object, _unitOfWork.Object);
            var result = feed.TryCreateFeed(new Feed { FeedName = "def"});

            Assert.That(result, Is.True);
        }
    }
}
