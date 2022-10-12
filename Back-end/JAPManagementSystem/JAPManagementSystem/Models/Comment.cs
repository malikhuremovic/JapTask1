﻿using JAPManagementSystem.Models.StudentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAPManagementSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Student")]
        public string SId { get; set; }
        public Student? Student { get; set; }
    }
}
