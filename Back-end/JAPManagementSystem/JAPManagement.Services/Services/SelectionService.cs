using AutoMapper;
using JAPManagement.Common;
using JAPManagement.Core.DTOs.SelectionDTOs;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.Models.SelectionModel;
using JAPManagement.ExceptionHandler.Exceptions;

namespace JAPManagement.Services.Services
{
    public class SelectionService : ISelectionService
    {
        private readonly IStudentRepository _students;
        private readonly ISelectionRepository _selections;
        private readonly IProgramRepository _programs;
        private readonly IMapper _mapper;
        public SelectionService(IStudentRepository students, IProgramRepository programs, ISelectionRepository selections, IMapper mapper, IStudentService studentService)
        {
            _students = students;
            _selections = selections;
            _programs = programs;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();

            var selection = _mapper.Map<Selection>(newSelection);
            var students = await _students.GetByIdInAsync(newSelection.StudentIds);
            selection.Students.AddRange(students);
            var japProgram = await _programs.GetByIdAsync(newSelection.JapProgramId);
            if (japProgram != null)
            {
                selection.JapProgramId = japProgram.Id;
            }
            else
            {
                throw new EntityNotFoundException("Program was not found");
            }
            await _selections.Add(selection);
            response.Data = _mapper.Map<GetSelectionDto>(selection);
            response.Message = "You have successfully created a new selection: " + selection.Name + ".";
            return response;
        }

        public async Task<ServiceResponse<List<AdminReport>>> GetSelectionsReport()
        {
            ServiceResponse<List<AdminReport>> response = new ServiceResponse<List<AdminReport>>();

            var result = await _selections.GetAdminReportAsync();
            response.Data = result;
            response.Message = "You have successfully fetched the selection report";
            return response;
        }

        public async Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections()
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();

            var selections = await _selections.GetAllAsync();
            response.Data = selections.Select(s => _mapper.Map<GetSelectionDto>(s)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetSelectionDto>> GetSelectionById(int selectionId)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();

            var selection = await _selections.GetByIdWithProgramAsync(selectionId);
            if (selection == null)
            {
                throw new EntityNotFoundException("Selection was not found");
            }
            else
            {
                response.Data = _mapper.Map<GetSelectionDto>(selection);
            }

            return response;
        }

        public async Task<ServiceResponse<GetSelectionDto>> DeleteSelectionByName(string selectionName)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();

            var selection = await _selections.GetByName(selectionName);
            if (selection == null)
            {
                throw new EntityNotFoundException("Selection was not found");
            }
            await _selections.Delete(selection.Id);
            response.Data = _mapper.Map<GetSelectionDto>(selection);
            response.Message = "Selection " + selectionName + " deleted.";
            return response;
        }

        public async Task<ServiceResponse<GetSelectionDto>> ModifySelection(ModifySelectionDto modifiedSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();

            var selection = await _selections.GetByIdAsync(modifiedSelection.Id);
            if (selection == null)
            {
                throw new EntityNotFoundException("Selection was not found");
            }
            var program = await _programs.GetByIdAsync(modifiedSelection.JapProgramId);
            if(program == null)
            {
                throw new EntityNotFoundException("Program was not found");
            }
            selection.JapProgram = program;
            var updatedSelection = await _selections.Update(_mapper.Map<Selection>(modifiedSelection));
            response.Data = _mapper.Map<GetSelectionDto>(updatedSelection);
            response.Message = "You have successfully modified a selection: " + selection.Name + ".";

            return response;
        }

        public ServiceResponse<GetSelectionPageDto> GetSelectionsWithParams(int pageNumber, int pageSize, string? name, string? japProgramName, DateTime? dateStart, DateTime? dateEnd, SelectionStatus? status, string sort, bool descending)
        {
            ServiceResponse<GetSelectionPageDto> response = new ServiceResponse<GetSelectionPageDto>();
            SelectionFetchConfig.Initialize(name, status, japProgramName, dateStart, dateEnd, sort, descending);

            var selections = _selections.GetSelectionsWithParams(pageNumber, pageSize, SelectionFetchConfig.sorts, SelectionFetchConfig.filters);
            response.Data = _mapper.Map<GetSelectionPageDto>(selections);
            response.Message = "You have fetched a page no. " + pageNumber + " with " + selections.Results.Count() + " selection(s).";

            return response;
        }
        public async Task<ServiceResponse<string>> DeleteSelectionById(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var selection = await _selections.GetByIdAsync(id);
            if (selection == null)
            {
                throw new EntityNotFoundException("Selection was not found");
            }
            await _selections.Delete(id);
            response.Message = "You have deleted a selection: " + selection.Name + ".";

            return response;
        }

    }
}

