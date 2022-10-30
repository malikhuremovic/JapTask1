namespace JAPManagement.Core.DTOs.Program
{
    public class ModifyProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<int>? AddLectureIds { get; set; }
        public List<int>? RemoveLectureIds { get; set; }
    }
}
