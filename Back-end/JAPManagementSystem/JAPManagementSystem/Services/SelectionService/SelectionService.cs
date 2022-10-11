using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.SelectionModel;
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
                var students = await _context.Students
                    .Where(u => newSelection.StudentIds
                    .Contains(u.Id))
                    .ToListAsync();
                foreach (var student in students)
                {
                    selection.Students.Add(student);
                }
                var japProgram = await _context.JapPrograms.FirstOrDefaultAsync(jp => jp.Id == newSelection.JapProgramId);
                if(japProgram != null)
                {
                    selection.JapProgramId = japProgram.Id;
                    selection.JapProgram = japProgram;
                }
                else
                {
                    throw new Exception("JAP program with the Id of " + newSelection.JapProgramId + " does not exist.");
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
                var selections = await _context.Selections
                    .Include(s => s.JapProgram)
                    .Include(s => s.Students)
                    .ToListAsync();
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

        public async Task<ServiceResponse<List<GetSelectionDto>>> DeleteSelectionByName(string selectionName)
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            try
            {
                var selection = await _context.Selections
                    .FirstOrDefaultAsync(s => s.Name
                    .Equals(selectionName));
                if(selection == null)
                {
                    response.Message = "There is no selection with name: " + selectionName;
                    response.Success = false;
                }
                _context.Selections.Remove(selection);
                await _context.SaveChangesAsync();
                var selections = await _context.Selections
                    .Include(s => s.JapProgram)
                    .Include(s => s.Students)
                    .ToListAsync();
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
                var selection = await _context.Selections
                    .Include(s => s.JapProgram)
                    .Include(s => s.Students)
                    .FirstOrDefaultAsync(s => s.Id == modifiedSelection.Id);
                if(selection == null)
                {
                    throw new Exception("There is no selection " + modifiedSelection.Name + " with the id of: " + modifiedSelection.Id + ".");
                }
                selection.Name = modifiedSelection.Name;
                selection.DateStart = modifiedSelection.DateStart;
                selection.DateEnd = modifiedSelection.DateEnd;
                selection.Status = modifiedSelection.Status;
              
                if (selection.JapProgramId != modifiedSelection.JapProgramId)
                {
                    var japProgram = await _context.JapPrograms
                        .FirstOrDefaultAsync(jp => jp.Id == modifiedSelection.JapProgramId);
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
        public ServiceResponse<GetSelectionPageDto> GetSelectionsWithParams(int pageNumber, int pageSize, string? name, string? japProgramName, DateTime? dateStart, DateTime? dateEnd, SelectionStatus? status, string sort, bool descending)
        {
            ServiceResponse<GetSelectionPageDto> response = new ServiceResponse<GetSelectionPageDto>();
            SelectionFetchConfig.Initialize(name, status, japProgramName, dateStart, dateEnd, sort, descending);
            try
            {
                var selections = _context.Selections
                    .Include("JapProgram")
                    .Paginate(
                    pageNumber,
                    pageSize,
                    SelectionFetchConfig.sorts,
                    SelectionFetchConfig.filters);
                response.Data = _mapper.Map<GetSelectionPageDto>(selections);
                response.Message = "You have fetched a page no. " + pageNumber + " with " + selections.RecordCount + " selection(s).";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
        public async Task<ServiceResponse<string>> DeleteSelectionById(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var selection = await _context.Selections
                    .Include(s => s.Students)
                    .Include(s => s.JapProgram)
                    .FirstOrDefaultAsync(s => s.Id == id);
                if(selection == null)
                {
                    throw new Exception("Selection not found");
                }
                _context.Selections.Remove(selection);
                await _context.SaveChangesAsync();
                response.Message = "You have deleted a selection: " + selection.Name +".";
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

