using System.Collections.Generic;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics SQL objects that can sort data must expose.
    /// </summary>
    public interface ISortable
    {
        /// <summary>
        /// Gets a collection of expressions that dictate the order in which records are sorted.
        /// </summary>
        List<SortExpression> OrderBy { get; }
    }
}
