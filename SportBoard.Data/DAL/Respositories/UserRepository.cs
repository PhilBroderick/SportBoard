﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class UserRepository : Repository<AspNetUsers>, IUserRepository
    {
        public UserRepository(SportboardDbContext context) : base(context)
        {
        }
    }
}
