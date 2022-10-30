using AutoMapper;
using JAPManagement.Common;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.DTOs.ProgramDTOs;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.Response;
using JAPManagement.Database.Data;
using JAPManagement.Exceptions.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace JAPManagement.Services.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramItemRepository _programItems;
        private readonly IProgramRepository _programs;
        private readonly IItemRepository _items;
        private readonly IMapper _mapper;
        public ProgramService(IProgramItemRepository programItems, IProgramRepository programs, IItemRepository items, IMapper mapper)
        {
            _programItems = programItems;
            _programs = programs;
            _items = items;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetProgramDto>> AddProgram(AddProgramDto newProgram)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();

            if (newProgram.Lectures.Count < 1)
            {
                throw new BadRequestException("Request not valid. At least one item must be allocated.");
            }

            var program = _mapper.Map<JapProgram>(newProgram);
            var lectures = await _items.GetByIdInAsync(newProgram.Lectures);
            program.Items.AddRange(lectures);
            await _programs.Add(program);
            response.Data = _mapper.Map<GetProgramDto>(program);
            response.Message = "You have successfully created a new JAP program: " + program.Name + ".";

            return response;
        }
        public async Task<ServiceResponse<GetProgramDto>> AddProgramItem(AddProgramItemsDto newProgramLectures)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();

            var lectures = await _items.GetByIdInAsync(newProgramLectures.LectureIds);
            var program = await _programs.GetByIdWithItemsAsync(newProgramLectures.ProgramId);
            if (program == null)
            {
                throw new EntityNotFoundException("Program was not found");
            }
            program.Items.AddRange(lectures);
            await _programs.SaveChangesAsync();
            response.Data = _mapper.Map<GetProgramDto>(program);
            StringBuilder builder = new StringBuilder();
            lectures.ForEach(lecture =>
            {
                builder.Append(lecture.Name + "\n");
            });
            response.Message = "You have successfully added lecture(s): \n" + builder.ToString() + " to a program " + program.Name + ".";

            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> RemoveProgramItem(DeleteProgramItemsDto programLectures)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();

            var lectures = await _items.GetByIdInAsync(programLectures.LectureIds);
            var program = await _programs.GetByIdWithItemsAsync(programLectures.ProgramId);
            if (program == null)
            {
                throw new EntityNotFoundException("Program was not found");
            }
            lectures.ForEach(lecture =>
            {
                int index = program.Items.FindIndex(item => item.Id == lecture.Id);
                program.Items.RemoveAt(index);
            });
            Console.WriteLine(program.Items.Count());
            await _programs.SaveChangesAsync();
            response.Data = _mapper.Map<GetProgramDto>(program);
            StringBuilder builder = new StringBuilder();
            lectures.ForEach(lecture =>
            {
                builder.Append(lecture.Name + "\n");
            });
            response.Message = "You have successfully removed lecture(s): \n" + builder.ToString() + " from a program " + program.Name + ".";

            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> ModifyProgram(ModifyProgramDto modifiedProgram)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();

            var program = await _programs.GetByIdWithItemsAsync(modifiedProgram.Id);
            if (program == null)
            {
                throw new EntityNotFoundException("Program was not found");
            }

            program.Name = modifiedProgram.Name;
            program.Content = modifiedProgram.Content;
            await _programs.Update(_mapper.Map<JapProgram>(program));
            response.Data = _mapper.Map<GetProgramDto>(program);
            response.Message = "You have successfully modified a program: " + program.Name + ".";

            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> DeleteProgram(int id)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();

            var program = await _programs.GetByIdAsync(id);
            if (program == null)
            {
                throw new EntityNotFoundException("Program was not found");
            }
            await _programs.Delete(id);
            response.Data = _mapper.Map<GetProgramDto>(program);
            response.Message = "You have deleted a program " + program.Name;

            return response;
        }
        public async Task<ServiceResponse<List<GetItemDto>>> GetProgramItems(int id)
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();

            var orderedProgramItems = await _programItems.GetProgramItemsAsync(id);
            var items = orderedProgramItems.Select(pt => pt.Item).ToList();
            var itemsDto = items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
            response.Data = itemsDto;
            response.Message = "You have successfully fetched ordered program items";

            return response;
        }

        public async Task<ServiceResponse<List<ProgramItem>>> ModifyProgramItemsOrder(AddProgramItemsOrder programItemsOrder)
        {
            ServiceResponse<List<ProgramItem>> response = new ServiceResponse<List<ProgramItem>>();

            var programItems = programItemsOrder.ItemOrders.Select(i => _mapper.Map<ProgramItem>(i)).ToList();
            await _programItems.ModifyProgramItemsOrderAsync(programItems);
            response.Data = programItems;
            response.Message = "You have updated items and orders.";

            return response;
        }

        public ServiceResponse<GetProgramPageDto> GetProgramsWithParams(int pageNumber, int pageSize, string? name, string? content, string sort, bool descending)
        {

            ServiceResponse<GetProgramPageDto> response = new ServiceResponse<GetProgramPageDto>();
            ProgramFetchConfig.Initialize(name, content, sort, descending);

            var programs = _programs.GetProgramsWithParams(pageNumber, pageSize, ProgramFetchConfig.sorts, ProgramFetchConfig.filters);
            response.Data = _mapper.Map<GetProgramPageDto>(programs);
            response.Message = "You have fetched a page no. " + pageNumber + " with " + programs.Results.Count() + " program(s).";

            return response;
        }

        public async Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms()
        {
            ServiceResponse<List<GetProgramDto>> response = new ServiceResponse<List<GetProgramDto>>();

            var programs = await _programs.GetAllWithItemsAsync();
            response.Data = programs.Select(p => _mapper.Map<GetProgramDto>(p)).ToList();
            response.Message = "You have successfully fetched all programs.";

            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> GetProgramById(int id)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();

            var program = await _programs.GetByIdWithItemsAsync(id);
            if (program == null)
            {
                throw new EntityNotFoundException("Program was not found");
            }
            response.Data = _mapper.Map<GetProgramDto>(program);
            response.Message = "You have successfully fetched a program with ID: " + id + ".";

            return response;
        }
    }
}
