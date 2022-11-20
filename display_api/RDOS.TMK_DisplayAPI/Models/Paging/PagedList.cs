using Microsoft.EntityFrameworkCore;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Paging
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList()
        {
            // Just come here in case Exception
        }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,// count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(List<T> source, int skip, int top)
        {
            if (top == 0) top = 99999;
            var count = source.Count();
            var items = source
              .Skip(skip)//((pageNumber - 1) * pageSize)
              .Take(top).ToList();
            return new PagedList<T>(items, count, (skip / top) + 1, top);
        }
    }

    public static class PaginatedQueryableExtensions
    {
        /// <summary>
        /// Asynchronously returns an <see cref="PagedList{TResult}" /> from an <see cref="IQueryable{T}" /> by enumerating it  asynchronously.
        /// </summary>
        /// <returns> An <see cref="PagedList{TResult}"/> whose elements are the result of invoking a projection function on each element of source.</returns>
        public static async Task<PagedList<TResult>> ToPaginatedAsync<TResult>(this IQueryable<TResult> source, int pageNumber, int pageSize, bool isDropdown = default)
        {
            var count = await source.CountAsync();
            int skip = 0, take = count;
            if (!isDropdown)
            {
                skip = (pageNumber - 1) * pageSize;
                take = pageSize;
            }

            var items = await source.Skip(skip).Take(take).ToListAsync();
            return new PagedList<TResult>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Asynchronously returns an <see cref="PagedList{TResult}" /> from an <see cref="IQueryable{T}" /> by enumerating it  asynchronously.
        /// </summary>
        /// <returns> An <see cref="PagedList{TResult}"/> whose elements are the result of invoking a projection function on each element of source.</returns>
        public static async Task<PagedList<TResult>> ToPaginatedAsync<TSource, TResult>(this IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TResult>> selector, int pageNumber, int pageSize, bool isDropdown = default)
        {
            var count = await source.CountAsync();
            int skip = 0, take = count;
            if (!isDropdown)
            {
                skip = (pageNumber - 1) * pageSize;
                take = pageSize;
            }

            var items = await source.Skip(skip).Take(take).Select(selector).ToListAsync();
            return new PagedList<TResult>(items, count, pageNumber, pageSize);
        }
    }
}