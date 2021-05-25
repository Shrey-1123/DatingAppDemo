namespace API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber{get;set;}
        private int _pageSize = 10;

      
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

        public int PageSize
        {
            get=> _pageSize;
            set=> _pageSize = (value>MaxPageSize) ? MaxPageSize : value;
        }

        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;

        public string OrderBy{ get; set;}
        
    }
}