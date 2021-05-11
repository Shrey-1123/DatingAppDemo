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
        public string Password { get; set; }
    }
}