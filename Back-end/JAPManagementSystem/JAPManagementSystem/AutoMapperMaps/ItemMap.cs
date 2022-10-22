using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.Models.ProgramModel;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class ItemMap : Profile
    {
        public ItemMap() {
            CreateMap<AddItemDto, JapItem>();
            CreateMap<JapItem, GetItemDto>();
            CreateMap<ModifyItemDto, JapItem>();
            CreateMap<Page<JapItem>, GetItemPageDto>();
        }
    }
}
