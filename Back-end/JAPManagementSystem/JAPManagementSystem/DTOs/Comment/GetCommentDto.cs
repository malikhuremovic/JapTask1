﻿using JAPManagementSystem.DTOs.StudentDto;

namespace JAPManagementSystem.DTOs.Comment
{
    public class GetCommentDto
    {
        public string Text { get; set; }
        public int StudentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetStudentDto Student { get; set; }
    }
}
