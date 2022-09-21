﻿using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs
{
    public class AddSelectionDto
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? JapProgramId { get; set; }
        public List<int>? StudentIds { get; set; } = new List<int>();
    }
}
