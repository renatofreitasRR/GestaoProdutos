using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Models
{
    public class PagedResult<T> where T : class
    {
        public PagedResult(){}

        public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = pageSize != 0 ? (int)Math.Ceiling(count / (double)pageSize) : 0;
            PageSize = pageSize;
            TotalCount = count;

            this.Items = new PagedList<T>();

            this.Items.AddRange(items);
        }

        public PagedList<T> Items { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
