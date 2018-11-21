using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit;
using NUnit.Framework;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.BLL;

namespace SportBoard.Tests.BLL
{
    [TestFixture]
    public class CreateFeedTests
    {
        private Mock<IFeedRepository> _feedRepository;

        [SetUp]
        public void SetUp()
        {
            _feedRepository = new Mock<IFeedRepository>();
        }

        [TestCase]
        public void CheckFeedName_FeedNameDoesNotExist_CreatesFeed()
        {
            var feed = new CreateFeed(_feedRepository.Object);
            feed.CheckIfFileNameExists(feedName: "abc");

            var result = feed.CheckIfFileNameExists(feedName: "abc");

            Assert.That(result, Is.True);
        }
    }
}
