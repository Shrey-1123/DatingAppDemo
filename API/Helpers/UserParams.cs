namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        

      
        // public int PageSize
        // {
        //     get { return _pageSize; }
        //     set
        //     { 
        //         _pageSize = value; 
        //         if(value>MaxPageSize)
        //         {
        //             PageSize = MaxPageSize;
        //         }
        //         else
        //         {
        //             PageSize = value;
        //         }
        //     }
        // }

        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;

        public string OrderBy{ get; set;}
        
    }
}