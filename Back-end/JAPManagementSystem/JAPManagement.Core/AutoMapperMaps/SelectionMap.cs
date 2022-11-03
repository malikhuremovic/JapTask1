using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.SelectionDTOs;
using JAPManagement.Core.Models.SelectionModel;

namespace JAPManagement.Core.AutoMapperMaps
{
    public class SelectionMap : Profile
    {
        public SelectionMap()
        {
            CreateMap<AddSelectionDto, Selection>();
            CreateMap<Selection, Selection>();
            CreateMap<Selection, GetSelectionDto>();
            CreateMap<Page<Selection>, GetSelectionPageDto>();
            CreateMap<ModifySelectionDto, Selection>().ForMember(dest => dest.Students, act => act.Ignore());
            CreateMap<Selection, SelectionReportDto>().ForMember(dest => dest.JapProgramName, opt => opt.MapFrom(src => src.JapProgram.Name));
        }
    }
}
