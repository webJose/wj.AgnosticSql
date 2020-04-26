using System.Collections.Generic;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics SQL objects that represent UPDATE statements must expose.
    /// </summary>
    public interface IUpdate : ICollection<SetField>, IFrom, IWhere
    {
    }
}
