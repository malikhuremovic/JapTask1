﻿using JAPManagementSystem.Models.UserModel;

namespace JAPManagementSystem.DTOs.User
{
    public class AddAdminDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
