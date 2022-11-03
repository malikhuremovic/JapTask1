namespace JAPManagement.Core.DTOs.JapItemDTOs
{
    public class AddItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public bool IsEvent { get; set; }
        public int ExpectedHours { get; set; }
    }
}
