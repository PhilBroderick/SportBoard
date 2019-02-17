using AutoMapper;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.Models.WebApiModels;
using System.Linq;
using System.Web.Http;

namespace SportBoard.Web.Controllers.WebApi
{
    [RoutePrefix("api/feeds")]
    public class FeedController : ApiController
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private FeedRepository _feedRepository;

        public FeedController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _feedRepository = new FeedRepository(_context);
        }

        [HttpGet]
        [Route("")]       
        public IHttpActionResult GetAllActiveFeeds()
        {
            var feeds = _feedRepository.Find(f => f.IsActive == true).ToList();

            if(feeds != null)
                return Ok(new { results = feeds.Select(Mapper.Map<Feed, FeedSearchDto>)});

            return NotFound();
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetFeedById(int id)
        { 
            var feed = _feedRepository.Get(id);

            if (feed != null)
                return Ok(Mapper.Map<FeedSearchDto>(feed));

            return NotFound();
        }
    }
}