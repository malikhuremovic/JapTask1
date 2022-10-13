using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.Models.SelectionModel;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class SelectionMap : Profile
    {
        public SelectionMap()
        {
            CreateMap<AddSelectionDto, Selection>();
            CreateMap<Selection, GetSelectionDto>();
            CreateMap<Page<Selection>, GetSelectionPageDto>();
            CreateMap<ModifySelectionDto, Selection>().ForMember(dest => dest.Students, act => act.Ignore());
            CreateMap<Selection, SelectionReportDto>().ForMember(dest => dest.JapProgramName, opt => opt.MapFrom(src => src.JapProgram.Name));
        }
    }
}
