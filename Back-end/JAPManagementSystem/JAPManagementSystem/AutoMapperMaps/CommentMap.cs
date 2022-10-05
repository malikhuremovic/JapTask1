using AutoMapper;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class CommentMap : Profile
    {
        public CommentMap()
        {            
            CreateMap<Comment, GetCommentDto>();
            CreateMap<AddCommentDto, Comment>();
        }
    }
}
