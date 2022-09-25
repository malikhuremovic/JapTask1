using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.DTOs.Student;
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
                selection.Status = modifiedSelection.Status;
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
        public ServiceResponse<List<GetSelectionDto>> GetSelectionsWithParams(int pageNumber, int pageSize, string? name, int? japProgramId, SelectionStatus? status, int sort, bool descending)
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            Filters<Selection> filters = new Filters<Selection>();
            filters.Add(!string.IsNullOrEmpty(name), s => s.Name.Contains(name));
            filters.Add(japProgramId > 0, s => s.JapProgramId == japProgramId);
            filters.Add(status.HasValue, s => s.Status == status);

            Sorts<Selection> sorts = new Sorts<Selection>();
            sorts.Add(sort.Equals("name"), s => s.Name, descending);
            sorts.Add(sort.Equals("dateStart"), s => s.DateStart, descending);
            sorts.Add(sort.Equals("dateEnd"), s => s.DateEnd, descending);
            sorts.Add(sort.Equals("status"), s => s.Status, descending);
            sorts.Add(sort.Equals("jaProgram"), s => s.JapProgramId, descending);

            try
            {
                var selections = _context.Selections.Include("JapProgram").Paginate(pageNumber, pageSize, sorts, filters);
                response.Data = selections.Results.Select(s => _mapper.Map<GetSelectionDto>(s)).ToList();
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
                var selection = await _context.Selections.FirstOrDefaultAsync(s => s.Id == id);
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

