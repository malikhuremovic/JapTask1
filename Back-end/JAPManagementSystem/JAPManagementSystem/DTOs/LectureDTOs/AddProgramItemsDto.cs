namespace JAPManagementSystem.DTOs.LectureDTOs

{
    public class AddProgramItemsDto
    {
        public int ProgramId { get; set; }
        public List<int> LectureIds { get; set; }
    }
}
