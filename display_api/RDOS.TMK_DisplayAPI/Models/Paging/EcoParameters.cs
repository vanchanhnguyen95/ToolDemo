using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Paging
{
    public class EcoParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public int? Skip { get; set; }
        public int? Top { get; set; }
        public string OrderBy { get; set; }
        public string Filter { get; set; }
        public bool IsDropdown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SearchValue { get; set; }
        public string SearchText { get; set; }
        public string PrincipleCode { get; set; }
        public int? LogLevel { get; set; }
        public string UserName { get; set; }
        public string FeatureCode { get; set; }
        public bool HasOrderBy => OrderBy != null && OrderBy.Trim() != string.Empty && OrderBy.Trim() != "NA_EMPTY";
        public bool HasQuerySearching => Filter != null && Filter.Trim() != string.Empty && Filter.Trim() != "NA_EMPTY";
        public Task<Expression<Func<T, bool>>> Predicate<T>()
        {
            if (HasQuerySearching)
            {
                return LambdaHelper.ToExpression<T>(Filter);
            }

            Expression<Func<T, bool>> predicate = x => true;
            return Task.FromResult(predicate);
        }
    }

    public static class LambdaHelper
    {
        public static Task<Expression<Func<T, bool>>> ToExpression<T>(this string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var options = Microsoft.CodeAnalysis.Scripting.ScriptOptions.Default.AddReferences(typeof(T).Assembly);
            return CSharpScript.EvaluateAsync<Expression<Func<T, bool>>>(($"s => {expression}"), options);
        }
    }
}