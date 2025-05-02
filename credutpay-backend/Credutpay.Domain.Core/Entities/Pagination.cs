using System.Collections.Generic;

namespace Credutpay.Domain.Core.Entities
{
    public class Pagination<TSource> where TSource : class
    {
        public Pagination(IEnumerable<TSource> items, long total)
            => (Items, Total) = (items, total);

        public IEnumerable<TSource> Items { get; set; }

        public long Total { get; set; }

        public static Pagination<TSource> Create(IEnumerable<TSource> items, long total)
            => new Pagination<TSource>(items, total);
    }
}
