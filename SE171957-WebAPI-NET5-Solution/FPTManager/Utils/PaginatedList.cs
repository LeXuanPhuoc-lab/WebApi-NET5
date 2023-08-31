using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Utils
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public PaginatedList(List<T> items, int count ,int pageIndex, int pageSize)
        {
            // generate total page by total items
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            // set page index
            PageIndex = pageIndex;
            // Add List Items
            AddRange(items);
        }

        public static PaginatedList<T> CreateByQueryable(IQueryable<T> source,int count, int pageIndex, int pageSize)
        {
            var totalPage = (int)Math.Ceiling(source.Count() / (double)pageIndex);

            // check if invalid pageIndex -> default = 1
            if (pageIndex > totalPage)
            {
                pageIndex = 1;
            }
            // get List<T> object from Skip(offset).Take(pageSize)
            var sourceItems 
                = source.Skip((pageIndex - 1) * pageSize) // get items from offset
                        .Take(pageSize); // get pagesize items
            // generate PagenitedList obj
            return new PaginatedList<T>(sourceItems.ToList(), source.Count(), pageIndex, pageSize);
        }

        public static PaginatedList<T> CreateByList(List<T> items, int pageIndex, int pageSize)
        {
            var totalPage = (int)Math.Ceiling(items.Count / (double)pageIndex);

            // check if invalid pageIndex -> default = 1
           if(pageIndex > totalPage)
            {
                pageIndex = 1;
            }

            // get List<T> object from Skip(offset).Take(pageSize)
            var sourceItems
                = items.Skip((pageIndex - 1) * pageSize) // get items from offset
                        .Take(pageSize); // get pagesize items
            // generate PagenitedList obj
            return new PaginatedList<T>(sourceItems.ToList(), items.Count, pageIndex, pageSize);
        }
    }
}
