using AutoMapper;
using SportBoard.Data.DAL;
using SportBoard.Web.Models.WebApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Feed, FeedDto>());
        }
    }
}