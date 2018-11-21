﻿using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IFeedRepository Feeds { get; }
        int Complete();
    }
}
