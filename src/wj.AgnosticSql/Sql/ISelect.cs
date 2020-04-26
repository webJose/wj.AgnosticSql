using System.Collections.Generic;
using System.Collections.Specialized;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics SQL objects that can return tabular data must expose.
    /// </summary>
    public interface ISelect : ICollection<IExpression>, INotifyCollectionChanged, ITable, IExpression
    {
        /// <summary>
        /// Gets or sets a pagination object containing paging information.
        /// </summary>
        /// <remarks>Some relational SQL engines may require an explicit order to allow 
        /// pagination while some do not.</remarks>
        Pagination Pagination { get; set; }

        /// <summary>
        /// Gets a list of field names returned by the SQL object.
        /// </summary>
        IList<string> FieldNames { get; }
    }
}
