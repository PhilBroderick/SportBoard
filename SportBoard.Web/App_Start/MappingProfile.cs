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
                .ForMember(dest => dest.FeedId,
                            opts => opts.MapFrom(src => src.FeedId))
                .ForMember(dest => dest.UserIdToNotify,
                            opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CreatedOn,
                            opts => opts.MapFrom(src => DateTime.Now))
                .BeforeMap((s, d) => d.IsRead = false);

            CreateMap<Post, PostNotificationDto>()
                .ForMember(dest => dest.Message,
                          opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.UserIdToNotify,
                           opts => opts.MapFrom(src => src.Feed.UserId))
                .ForMember(dest => dest.UserIdCreatedNotification, 
                           opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.NotificationType,
                           opts => opts.MapFrom(src => NotificationTypes.NewPost.ToString()))
                .ForMember(dest => dest.CreatedOn,
                            opts => opts.MapFrom(src => DateTime.Now))
                .BeforeMap((s, d) => d.IsRead = false);

            CreateMap<Comment, CommentNotificationDto>()
                .ForMember(dest => dest.Message,
                            opts => opts.MapFrom(src => src.CommentText))
                .ForMember(dest => dest.UserIdToNotify,
                            opts => opts.MapFrom(src => src.Post.UserId))
                .ForMember(dest => dest.UserIdCreatedNotification,
                            opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.NotificationType,
                            opts => opts.MapFrom(src => NotificationTypes.NewComment.ToString()))
                .ForMember(dest => dest.CreatedOn,
                            opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CommentId,
                            opts => opts.MapFrom(src => src.CommentId))
                .BeforeMap((s, d) => d.IsRead = false);
        }
    }
}