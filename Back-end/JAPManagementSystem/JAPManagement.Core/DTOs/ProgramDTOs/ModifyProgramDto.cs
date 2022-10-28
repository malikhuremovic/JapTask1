namespace JAPManagement.Core.DTOs.Program
{
    public class ModifyProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int[] AddLectureIds { get; set; } = Array.Empty<int>();
        public int[] RemoveLectureIds { get; set; } = Array.Empty<int>();
    }
}
