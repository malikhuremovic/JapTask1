namespace JAPManagementSystem.DTOs.LectureDTOs
{
    public class AddItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public bool isEvent { get; set; }
        public int ExpectedHours { get; set; }
    }
}
