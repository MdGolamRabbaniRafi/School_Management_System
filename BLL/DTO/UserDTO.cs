using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BLL.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required, EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must be at least 6 characters and contain a letter and a number.")]
        public string Password { get; set; }

        [Required]
        public BloodGroupEnum BloodGroup { get; set; }

        [Required]
        public UserTypeEnum UserType { get; set; }

        public IFormFile? ProfilePicture { get; set; } // for receiving uploaded file
        public string? ProfileImagePath { get; set; }   // for returning image path
    }

    public enum UserTypeEnum
    {
        Administrator, Principal, Teacher, Student,
        Parent, Accountant, Librarian, TransportStaff, HRStaff
    }

    public enum BloodGroupEnum
    {
        [EnumMember(Value = "A+")]
        A_Positive,

        [EnumMember(Value = "A-")]
        A_Negative,

        [EnumMember(Value = "B+")]
        B_Positive,

        [EnumMember(Value = "B-")]
        B_Negative,

        [EnumMember(Value = "AB+")]
        AB_Positive,

        [EnumMember(Value = "AB-")]
        AB_Negative,

        [EnumMember(Value = "O+")]
        O_Positive,

        [EnumMember(Value = "O-")]
        O_Negative
    }
}
