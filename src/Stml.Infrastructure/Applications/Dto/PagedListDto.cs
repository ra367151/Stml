using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Stml.Infrastructure.Applications.Dto
{
    public class PagedListDto<T> where T : class
    {
        public PagedListDto()
        {
            Total = 0;
            Rows = new List<T>().ToImmutableList();
        }

        public PagedListDto(int total, [NotNull]IEnumerable<T> rows)
            : this()
        {
            Check.NotNull(rows, nameof(rows));
            Total = total;
            if (total != 0)
                Rows = rows.ToImmutableList();
        }

        public static PagedListDto<T> Null => new PagedListDto<T>(0, new List<T>());

        public int Total { get; set; }
        public IReadOnlyList<T> Rows { get; set; }
    }
}
