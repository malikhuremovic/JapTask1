using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Common;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.Response;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace JAPManagement.Services.Services
{
    public class ProgramService : IProgramService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProgramService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetProgramDto>> AddProgram(AddProgramDto newProgram)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            try
            {
                var program = _mapper.Map<JapProgram>(newProgram);
                var lectures = await _context.Items.Where(l => newProgram.Lectures.Contains(l.Id)).ToListAsync();
                program.Items.AddRange(lectures);
                _context.JapPrograms.Add(program);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProgramDto>(program);
                response.Message = "You have successfully created a new JAP program: " + program.Name + ".";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }

            return response;
        }
        public async Task<ServiceResponse<GetProgramDto>> AddProgramItem(AddProgramItemsDto newProgramLectures)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            try
            {
                var lectures = await _context.Items.Where(l => newProgramLectures.LectureIds.Contains(l.Id)).ToListAsync();
                var program = await _context.JapPrograms.Include(j => j.Items).FirstOrDefaultAsync(p => p.Id == newProgramLectures.ProgramId);
                if (program == null)
                {
                    throw new Exception("There is no program with the ID of: " + newProgramLectures.ProgramId);
                }
                program.Items.AddRange(lectures);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProgramDto>(program);
                StringBuilder builder = new StringBuilder();
                lectures.ForEach(lecture =>
                {
                    builder.Append(lecture.Name + "\n");
                });
                response.Message = "You have successfully added lecture(s): \n" + builder.ToString() + " to a program " + program.Name + ".";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<GetProgramDto>> RemoveProgramItem(DeleteProgramItemsDto programLectures)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            try
            {
                var lectures = await _context.Items.Where(l => programLectures.LectureIds.Contains(l.Id)).ToListAsync();
                var program = await _context.JapPrograms.Include(j => j.Items).FirstOrDefaultAsync(p => p.Id == programLectures.ProgramId);
                if (program == null)
                {
                    throw new Exception("There is no program with the ID of: " + programLectures.ProgramId);
                }
                lectures.ForEach(lecture =>
                {
                    program.Items.Remove(lecture);
                });
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProgramDto>(program);
                StringBuilder builder = new StringBuilder();
                lectures.ForEach(lecture =>
                {
                    builder.Append(lecture.Name + "\n");
                });
                response.Message = "You have successfully removed lecture(s): \n" + builder.ToString() + " from a program " + program.Name + ".";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> ModifyProgram(ModifyProgramDto modifiedProgram)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            try
            {
                var program = await _context.JapPrograms
                        .FirstOrDefaultAsync(p => p.Id == modifiedProgram.Id);
                try
                {
                    List<int> lectureIds = modifiedProgram.AddLectureIds.ToList();
                    AddProgramItemsDto addProgramItems = new AddProgramItemsDto()
                    {
                        ProgramId = modifiedProgram.Id,
                        LectureIds = lectureIds
                    };
                    await AddProgramItem(addProgramItems);
                    List<int> removeLectureIds = modifiedProgram.RemoveLectureIds.ToList();
                    DeleteProgramItemsDto removeProgramItems = new DeleteProgramItemsDto()
                    {
                        ProgramId = modifiedProgram.Id,
                        LectureIds = removeLectureIds
                    };
                    await RemoveProgramItem(removeProgramItems);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }

                if (program == null)
                {
                    throw new Exception("There is no program " + modifiedProgram.Name + " with the id of: " + modifiedProgram.Id + ".");
                }
                program.Name = modifiedProgram.Name;
                program.Content = modifiedProgram.Content;

                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProgramDto>(program);
                response.Message = "You have successfully modified a program: " + program.Name + ".";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> DeleteProgram(int id)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            try
            {
                var program = await _context.JapPrograms.FirstOrDefaultAsync(p => p.Id == id);
                if (program == null)
                {
                    throw new Exception("There is no program with the id of: " + id);
                }
                _context.JapPrograms.Remove(program);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProgramDto>(program);
                response.Message = "You have deleted a program " + program.Name;
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetItemDto>>> GetProgramItems(int id)
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();
            try
            {
                var orderedProgramItems = await _context.ProgramItems
                .Where(pt => pt.ProgramId == id)
                .OrderBy(pt => pt.Order)
                .Include(pt => pt.Item)
                .ToListAsync();
                var items = orderedProgramItems.Select(pt => pt.Item).ToList();
                var itemsDto = items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
                response.Data = itemsDto;
                response.Message = "You have successfully fetched ordered program items";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<ProgramItem>>> ModifyProgramItemsOrder(AddProgramItemsOrder programItemsOrder)
        {
            ServiceResponse<List<ProgramItem>> response = new ServiceResponse<List<ProgramItem>>();
            try
            {
                var programItems = programItemsOrder.ItemOrders.Select(i => _mapper.Map<ProgramItem>(i)).ToList();
                _context.ProgramItems.UpdateRange(programItems);
                await _context.SaveChangesAsync();
                response.Data = programItems;
                response.Message = "You have updated items and orders.";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public ServiceResponse<GetProgramPageDto> GetProgramsWithParams(int pageNumber, int pageSize, string? name, string? content, string sort, bool descending)
        {

            ServiceResponse<GetProgramPageDto> response = new ServiceResponse<GetProgramPageDto>();
            ProgramFetchConfig.Initialize(name, content, sort, descending);
            try
            {
                var programs = _context.JapPrograms
                    .Paginate(
                    pageNumber,
                    pageSize,
                    ProgramFetchConfig.sorts,
                    ProgramFetchConfig.filters);
                response.Data = _mapper.Map<GetProgramPageDto>(programs);
                response.Message = "You have fetched a page no. " + pageNumber + " with " + programs.Results.Count() + " program(s).";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms()
        {
            ServiceResponse<List<GetProgramDto>> response = new ServiceResponse<List<GetProgramDto>>();
            try
            {
                var programs = await _context.JapPrograms.Include(p => p.Items).ToListAsync();
                response.Data = programs.Select(p => _mapper.Map<GetProgramDto>(p)).ToList();
                response.Message = "You have successfully fetched all programs.";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetProgramDto>> GetProgramById(int id)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            try
            {
                var program = await _context.JapPrograms.Include(p => p.Items).FirstOrDefaultAsync(p => p.Id == id);
                if (program == null)
                {
                    throw new Exception("Program not found");
                }
                response.Data = _mapper.Map<GetProgramDto>(program);
                response.Message = "You have successfully fetched a program with ID: " + id + ".";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }
    }
}
