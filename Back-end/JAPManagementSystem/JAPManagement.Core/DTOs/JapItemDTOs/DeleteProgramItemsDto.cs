namespace JAPManagement.Core.DTOs.JapItemDTOs
{
    public class DeleteProgramItemsDto
    {
        public int ProgramId { get; set; }
        public List<int> LectureIds { get; set; }
    }
}
