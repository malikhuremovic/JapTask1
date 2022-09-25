using AutoMapper;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms()
        {
            ServiceResponse<List<GetProgramDto>> response = new ServiceResponse<List<GetProgramDto>>();
            try
            {
                var programs = await _context.JapPrograms.ToListAsync();
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
                var program = await _context.JapPrograms.FirstOrDefaultAsync(p => p.Id == id);
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
