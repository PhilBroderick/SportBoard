using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class DeletionRequestRepository : Repository<DeletionRequest>, IDeletionRequestRepository
    {
        public DeletionRequestRepository(SportboardDbContext context) : base(context)
        {
        }

        public SportboardDbContext SportboardDbContext
        {
            get { return Context as SportboardDbContext; }
        }

        public IQueryable<DeletionRequest> OpenRequests()
        {
            var requests = SportboardDbContext.DeletionRequest.Where(r => r.RequestFulfilled == false);

            return requests;
        }
    }
}
