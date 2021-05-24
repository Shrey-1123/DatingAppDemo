using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PageList<T> : List<T>
    { 
        // count is total number of recored in database, pageNumber currentPage, pageSize number of items in one page, items no of items to displayy

        // eg if dabatabse has total row count of 100 and pageSize = 5 then,
        // TotalPages = 100/5 = 20 pages.
        // 5 items per page
        public PageList(IEnumerable<T> items, int count, int pageNumber,int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/ (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } // based on our query what amount of row we want from database

        // this method will receive a Iqueryable query with where clause and predicate we are using inside that where clause
        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); // potential total rows/ items that can be returned from database

            // no of items to display on current page
            // e.g if we are in page 1 then skip(0) means we do not skip any records from database and we select upto
            // pageSize( say if pageSize is 5) we select upto 5 records from database at once.
            var items = await source.Skip((pageNumber-1)* pageSize).Take(pageSize).ToListAsync(); 

            return new PageList<T>(items, count, pageNumber, pageSize);
        }   
    }
}