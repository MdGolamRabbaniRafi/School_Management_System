using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO 
    {
        public string Id { get; set; }  
        public DateTime? CreatedAt { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
            ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Password must be at least 6 characters and contain a letter and a number.")]
        public string Password { get; set; }

        [Required]
        public BloodGroupEnum BloodGroup { get; set; }

        [Required]
        public UserTypeEnum UserType { get; set; }
    }

    public enum UserTypeEnum
    {
        Administrator,
        Principal,
        Teacher,
        Student,
        Parent,
        Accountant,
        Librarian,
        TransportStaff,
        HRStaff
    }

    public enum BloodGroupEnum
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }
}
