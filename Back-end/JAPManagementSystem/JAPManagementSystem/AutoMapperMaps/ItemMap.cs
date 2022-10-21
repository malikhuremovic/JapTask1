using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.LectureDTOs;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class ItemMap : Profile
    {
        public ItemMap() {
            CreateMap<AddItemDto, Item>();
            CreateMap<Item, GetItemDto>();
            CreateMap<ModifyItemDto, Item>();
            CreateMap<Page<Item>, GetItemPageDto>();
        }
    }
}
