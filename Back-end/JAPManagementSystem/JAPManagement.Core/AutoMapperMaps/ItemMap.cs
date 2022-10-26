using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Core.AutoMapperMaps
{
    public class ItemMap : Profile
    {
        public ItemMap()
        {
            CreateMap<AddItemDto, JapItem>();
            CreateMap<JapItem, GetItemDto>();
            CreateMap<ModifyItemDto, JapItem>();
            CreateMap<Page<JapItem>, GetItemPageDto>();
        }
    }
}
