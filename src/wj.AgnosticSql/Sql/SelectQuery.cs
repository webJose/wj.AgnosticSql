using System.Collections.Generic;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines a SQL SELECT statement.
    /// </summary>
    public class SelectQuery : Select, IStatement, IFrom, IWhere, ISortable
    {
        #region IFrom
        /// <inheritdoc />
        public ITable From { get; set; }
        #endregion

        #region IWhere
        /// <inheritdoc />
        public IExpression Where { get; set; }
        #endregion

        #region ISortable
        /// <inheritdoc />
        public List<SortExpression> OrderBy { get; } = new List<SortExpression>();
        #endregion
    }
}
