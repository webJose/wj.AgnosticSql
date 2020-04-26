namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Represents the Boolean unary operator NOT.
    /// </summary>
    public class Not : Function
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="expr">Expression to negate.</param>
        public Not(IExpression expr)
            : base()
        {
            Arguments.Add(expr);
        }
        #endregion
    }
}
