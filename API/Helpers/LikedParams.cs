namespace API.Helpers
{
    public class LikedParams : PaginationParams
    {
        public int UserId { get; set; }
        public string Predicate { get; set; }
        
    }
}