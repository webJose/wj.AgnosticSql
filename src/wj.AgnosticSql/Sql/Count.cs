namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Expression value-counting function.
    /// </summary>
    public class Count : Function
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="expr">Expression to count.  If no expression is provided, then 
        /// it defaults to counting records.</param>
        public Count(IExpression expr = null)
            : base()
        {
            expr = expr ?? new Field(Field.AllFieldsName);
            Arguments.Add(expr);
        }
        #endregion
    }
}
