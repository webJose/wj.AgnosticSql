namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines how an expression is used to sort data.
    /// </summary>
    public class SortExpression
    {
        #region Properties
        /// <summary>
        /// Gets or sets the expression to use when sorting data.
        /// </summary>
        public IExpression Expression { get; set; }

        /// <summary>
        /// Gets or sets the sorting direction.
        /// </summary>
        public SortDirections Direction { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="expression">Expression to use when sorting data.</param>
        /// <param name="direction">Sorting direction.</param>
        public SortExpression(IExpression expression, SortDirections direction = SortDirections.Ascending)
        {
            Expression = expression;
            Direction = direction;
        }
        #endregion
    }
}
