using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; } // this is used as Key for PasswordHash
        public DateTime DateOfBirth{get; set;}

        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }
        public String Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        // public int GetAge(){
        //     return DateOfBirth.CalculateAge();
        // }

        public ICollection<UserLike> LikedByUsers { get; set; } // this is the list of users that liked currently logged in user
        public ICollection<UserLike> LikedUsers { get; set; } // this is the list of user that the currenlty logged in user has liked

        public ICollection<Message> MessagesSent { get; set; }
         public ICollection<Message> MessagesReceived { get; set; }

        

    }
}