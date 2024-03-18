using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Models
{
    public class PaginationParams
    {
        const int maxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        public string? Search { get; set; }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value ;
            }
        }
    }
}
