using AutoMapper;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace JAPManagementSystem.Services.ProgramService
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
        public async Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms()
        {
            ServiceResponse<List<GetProgramDto>> response = new ServiceResponse<List<GetProgramDto>>();
            try
            {
                var programs = await _context.JapPrograms.Include(p => p.Items).ToListAsync();
                response.Data = programs.Select(p => _mapper.Map<GetProgramDto>(p)).ToList();
                response.Message = "You have successfully fetched all programs.";
            }catch(Exception exc)
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
                if(program == null)
                {
                    throw new Exception("Program not found");
                }
                response.Data = _mapper.Map<GetProgramDto>(program);
                response.Message = "You have successfully fetched a program with ID: " + id + ".";
            }catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }
    }
}
