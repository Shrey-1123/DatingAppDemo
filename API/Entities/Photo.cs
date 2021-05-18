using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] // when we don't add DbSet for Photo class EF will build Migration even with Dbset declared in DataContet class
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        
        public AppUser AppUser { get; set; }

        public int AppUserId {get; set;} // this is fully defined relationship between photo and User
    }
}