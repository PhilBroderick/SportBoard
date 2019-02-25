using AutoMapper;
using SportBoard.Data.DAL;
using SportBoard.Web.Models.DTOs;
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
            CreateMap<Feed, FeedSearchDto>();
            CreateMap<Post, PostDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<DeletionRequest, DeletionRequestNotificationDto>()
                .ForMember(dest => dest.Message, 
                           opts => opts.MapFrom(src => src.ReasonForDeletion))
                .ForMember(dest => dest.NotificationType,
                            opts => opts.MapFrom(NotificationTypes.FeedDeletionRequest.ToString()));
        }
    }
}