using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(SportboardDbContext context) : base(context)
        {
        }
    }
}
