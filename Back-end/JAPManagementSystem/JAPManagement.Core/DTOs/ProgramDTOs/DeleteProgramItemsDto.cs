namespace JAPManagement.Core.DTOs.ProgramDTOs
{
    public class DeleteProgramItemsDto
    {
        public int ProgramId { get; set; }
        public List<int> LectureIds { get; set; }
    }
}
