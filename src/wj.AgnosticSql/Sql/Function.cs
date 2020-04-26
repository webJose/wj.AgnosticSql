using System.Collections.Generic;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// SQL objects that can receive expressions as arguments to produce a scalar result.
    /// </summary>
    public class Function : Expression
    {
        #region Properties
        /// <summary>
        /// Gtes the list of expressions used as arguments.  The arguments' order in which they 
        /// are added matter.
        /// </summary>
        public List<IExpression> Arguments { get; } = new List<IExpression>();
        #endregion
    }
}
