using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace JAPManagementSystem.Services.LectureService
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ItemService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetItemDto>> AddLecture(AddItemDto newLecture)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            try
            {
                var lecture = _mapper.Map<JapItem>(newLecture);
                _context.Items.Add(lecture);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetItemDto>(lecture);
                response.Message = "You have successfully added a new " + (newLecture.IsEvent ? "event " : "lecture ") + newLecture.Name;
            }catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> GetLecture(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            try
            {
                var lecture = await _context.Items.FirstOrDefaultAsync(l => l.Id == id);
                if(lecture == null)
                {
                    throw new Exception("No lecture with the id of: " + id);
                }
                response.Data = _mapper.Map<GetItemDto>(lecture);
                response.Message = "You have successfully fetched a lecture";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> DeleteLecture(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            try
            { 
                var lecture = await _context.Items.FirstOrDefaultAsync(l => l.Id == id);
                if(lecture == null)
                {
                    throw new Exception("There is no lecture with the id of: " + id);
                }
                _context.Items.Remove(lecture);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetItemDto>(lecture);
                response.Message = "You have deleted a lecture " + lecture.Name;
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> ModifyLecture(ModifyItemDto modifiedLecture)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            try
            {
                var lecture = await _context.Items
                    .FirstOrDefaultAsync(l => l.Id == modifiedLecture.Id);
                if (lecture == null)
                {
                    throw new Exception("There is no lecture " + modifiedLecture.Name + " with the id of: " + modifiedLecture.Id + ".");
                }
                lecture.Name = modifiedLecture.Name;
                lecture.URL = modifiedLecture.URL;
                lecture.Description = modifiedLecture.Description;
                lecture.ExpectedHours = modifiedLecture.ExpectedHours;
                lecture.IsEvent = modifiedLecture.IsEvent;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetItemDto>(lecture);
                response.Message = "You have successfully modified a selection: " + lecture.Name + ".";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public ServiceResponse<GetItemPageDto> GetLecturesWithParams(int pageNumber, int pageSize, string? name, string? description, string? URL, int? expectedHours, string sort, bool descending)
        {

            ServiceResponse<GetItemPageDto> response = new ServiceResponse<GetItemPageDto>();
            ItemFetchConfig.Initialize(name, description, URL, expectedHours, sort, descending);
            try
            {
                var lectures = _context.Items
                    .Paginate(
                    pageNumber,
                    pageSize,
                    ItemFetchConfig.sorts,
                    ItemFetchConfig.filters);
                response.Data = _mapper.Map<GetItemPageDto>(lectures);
                response.Message = "You have fetched a page no. " + pageNumber + " with " + lectures.Results.Count() + " lecture(s).";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
