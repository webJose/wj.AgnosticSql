using System.Collections.ObjectModel;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines a collection of SQL statements.
    /// </summary>
    public class Batch : Collection<IStatement>, IStatement
    {
    }
}
