namespace JAPManagement.Core.DTOs.ProgramDTOs

{
    public class AddProgramItemsDto
    {
        public int ProgramId { get; set; }
        public List<int> LectureIds { get; set; }
    }
}
