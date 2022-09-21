using AutoMapper;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs;
using JAPManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace JAPManagementSystem.Services.SelectionService
{
    public class SelectionService : ISelectionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SelectionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            try
            {
                var selection = _mapper.Map<Selection>(newSelection);
                var students = await _context.Students.Where(u => newSelection.StudentIds.Contains(u.Id)).ToListAsync();
                foreach (var student in students)
                {
                    selection.Students.Add(student);
                }
                _context.Selections.Add(selection);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetSelectionDto>(selection);
                response.Message = "You have successfully created a new selection: " + selection.Name + ".";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections()
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            try
            {
                var selections = await _context.Selections.Include(s => s.JapProgram).Include(s => s.Students).ToListAsync();
                response.Data = selections.Select(s => _mapper.Map<GetSelectionDto>(s)).ToList();
            }
            catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetSelectionDto>> GetSelectionById(int selectionId)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            try
            {
                var selection = await _context.Selections.FirstOrDefaultAsync(s => s.Id == selectionId);
                if(selection == null)
                {
                    response.Message = "No selection found with the Id of: " + selectionId;
                }
                else
                {
                    response.Data = _mapper.Map<GetSelectionDto>(selection);
                }
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetSelectionDto>> GetSelectionByName(string selectionName)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            try
            {
                var selection = await _context.Selections.FirstOrDefaultAsync(s => s.Name.Equals(selectionName));
                if (selection == null)
                {
                    response.Message = "No selection found with the name of: " + selectionName;
                }
                else
                {
                    response.Data = _mapper.Map<GetSelectionDto>(selection);
                }
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetSelectionDto>>> DeleteSelectionByName(string selectionName)
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            try
            {
                var selection = await _context.Selections.FirstOrDefaultAsync(s => s.Name.Equals(selectionName));
                if(selection == null)
                {
                    response.Message = "There is no selection with name: " + selectionName;
                    response.Success = false;
                }
                _context.Selections.Remove(selection);
                await _context.SaveChangesAsync();
                var selections = await _context.Selections.Include(s => s.JapProgram).Include(s => s.Students).ToListAsync();
                response.Data = selections.Select(s => _mapper.Map<GetSelectionDto>(s)).ToList();
            }
            catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
        public async Task<ServiceResponse<GetSelectionDto>> ModifySelection(ModifySelectionDto modifiedSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            try
            {
                var selection = await _context.Selections.Include(s => s.JapProgram).Include(s => s.Students).FirstOrDefaultAsync(s => s.Id == modifiedSelection.Id);
                if(selection == null)
                {
                    throw new Exception("There is no selection " + modifiedSelection.Name + " with the id of: " + modifiedSelection.Id + ".");
                }
                selection.Name = modifiedSelection.Name;
                selection.DateStart = modifiedSelection.DateStart;
                selection.DateEnd = modifiedSelection.DateEnd;
                var studentsToRemove = await _context.Students.Where(s => modifiedSelection.StudentsToRemove.Contains(s.Id)).ToListAsync();
                var studentsToAdd = await _context.Students.Where(s => modifiedSelection.StudentsToAdd.Contains(s.Id)).ToListAsync();
                foreach (var student in studentsToRemove)
                {
                    selection.Students.Remove(student);
                }
                foreach(var student in studentsToAdd)
                {
                    selection.Students.Add(student);
                }
                if (selection.JapProgramId != modifiedSelection.JapProgramId)
                {
                    var japProgram = await _context.JapPrograms.FirstOrDefaultAsync(jp => jp.Id == modifiedSelection.JapProgramId);
                    selection.JapProgramId = modifiedSelection.JapProgramId;
                    selection.JapProgram = japProgram; 
                }
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetSelectionDto>(selection);
                response.Message = "You have successfully modified a selection: " + selection.Name + ".";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

    }
}
