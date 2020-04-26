namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// SQL object that defines a Boolean expression by comparing one expression 
    /// with another using a binary Boolean operator.
    /// </summary>
    public class Condition : Expression
    {
        #region Properties
        /// <summary>
        /// Gets or sets the expression to the left of the binary Boolean operator defined in 
        /// the <see cref="Operator"/> property.
        /// </summary>
        public IExpression Left { get; set; }

        /// <summary>
        /// Gets or sets the binary Boolean operator to use when evaluating the condition.
        /// </summary>
        public BooleanOperators Operator { get; set; }

        /// <summary>
        /// Gets or sets the expression to the right of the binary Boolean operator defined in 
        /// the <see cref="Operator"/> property.
        /// </summary>
        public IExpression Right { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="left">Expression to the left of the <paramref name="opr"/> operator.</param>
        /// <param name="opr">Binary Boolean operator to apply to the given expressions.</param>
        /// <param name="right">Expression to the right of the <paramref name="opr"/> operator.</param>
        public Condition(IExpression left, BooleanOperators opr = BooleanOperators.None, IExpression right = null)
        {
            Left = left;
            Operator = opr;
            Right = right;
        }
        #endregion
    }
}
