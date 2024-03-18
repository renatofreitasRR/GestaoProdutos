
namespace GestaoProdutos.Domain.Models
{
    public class PagedList<T> : List<T> where T : class
    {
        public PagedList() { } 

        public static PagedResult<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }
    }
}
