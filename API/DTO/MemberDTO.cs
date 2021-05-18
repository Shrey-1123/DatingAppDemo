using System;
using System.Collections.Generic;

namespace API.DTO
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; } // this is used as Key for PasswordHash
        public int Age{get; set;}

        public string KnownAs { get; set; }
        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }
        public String Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }
    }
}