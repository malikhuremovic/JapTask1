using AutoMapper;
using JAPManagement.Core.DTOs.Comment;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.AutoMapperMaps
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
