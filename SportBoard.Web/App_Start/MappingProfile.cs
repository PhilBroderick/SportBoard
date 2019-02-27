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
                            opts => opts.MapFrom(src => NotificationTypes.FeedDeletionRequest.ToString()))
                .BeforeMap((s, d) => d.IsRead = false);
            CreateMap<Post, PostNotificationDto>().
                ForMember(dest => dest.Message,
                          opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.NotificationType,
                           opts => opts.MapFrom(src => NotificationTypes.NewPost.ToString()))
                .BeforeMap((s, d) => d.IsRead = false);
        }
    }
}