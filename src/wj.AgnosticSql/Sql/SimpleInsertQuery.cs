using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines an INSERT statement.
    /// </summary>
    public class SimpleInsertQuery : Collection<Field>, IStatement
    {
        /// <summary>
        /// Gets or sets the destination table.
        /// </summary>
        public ITable Table { get; set; }

        /// <summary>
        /// Gets a collection of collection of values to be inserted as new records.
        /// </summary>
        /// <remarks>Each element in this collection is in itself a collection of values.  If 
        /// a SQL builder do not support addition of multiple rows in a single statement, it 
        /// should produce instead multiple INSERT statements, one for each item in this 
        /// collection.</remarks>
        public List<List<IExpression>> Values { get; } = new List<List<IExpression>>();
    }
}
