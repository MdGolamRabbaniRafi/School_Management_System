using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string? Password { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string? BloodGroup { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string? UserType { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)] // increase from 50
        public string? ProfilePicture { get; set; }
    }
}
