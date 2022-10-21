using AutoMapper;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models.StudentModel;

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
