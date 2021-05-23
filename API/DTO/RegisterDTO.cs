using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    /// For DTO reference
    /// https://www.codeproject.com/Articles/1050468/Data-Transfer-Object-Design-Pattern-in-Csharp
    /// We implemented DTO for denormalized user registration, as we will recieve username and password as single entity
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } // we can choose this case sesttve as we like, it wont affect anywhere
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        
        [Required]
        [StringLength(8,MinimumLength=4)]
        public string Password { get; set; }
    }
}