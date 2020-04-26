namespace wj.AgnosticSql.Extensions
{
    /// <summary>
    /// Defines convenience methods to create expressions.
    /// </summary>
    public class ExpressionBuilderHelper
    {
        #region Methods
        /// <summary>
        /// Creates a new constant expression using the given value.
        /// </summary>
        /// <typeparam name="TConst">Type of value.</typeparam>
        /// <param name="value">Constant value.</param>
        /// <returns>A newly created <see cref="Sql.Constant{TConst}"/> object with its 
        /// <see cref="Sql.Constant{TConst}.Value"/> set to the given <paramref name="value"/>.</returns>
        public Sql.Constant<TConst> Constant<TConst>(TConst value)
            => new Sql.Constant<TConst>(value);

        /// <summary>
        /// Creates a new parameter expression using the given name.
        /// </summary>
        /// <param name="paramName">The parameter's name.</param>
        /// <returns>A newly created <see cref="Sql.Parameter"/> object with its 
        /// <see cref="Sql.Parameter.Name"/> property set to the given name.</returns>
        public Sql.Parameter Parameter(string paramName)
        {
            Guard.IsNotNull(paramName, nameof(paramName));
            return new Sql.Parameter(paramName);
        }

        /// <summary>
        /// Makes sure that the given expression is grouped.  Grouped expressions normally show up 
        /// in SQL as expressions surrounded with parenthesis.
        /// </summary>
        /// <typeparam name="TExpression">Type of expression.</typeparam>
        /// <param name="expression">Expression object to be marked as grouped.</param>
        /// <returns>The given expression object to enable fluent syntax.</returns>
        public TExpression Group<TExpression>(TExpression expression)
            where TExpression : Sql.IExpression
        {
            expression.Grouped = true;
            return expression;
        }

        /// <summary>
        /// Creates a condition expression using the provided expressions and binary operator.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="opr">Binary Boolean operator.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>A newly created <see cref="Sql.Condition"/> object with the operands and 
        /// operator set to the given expressions and operator.</returns>
        public Sql.Condition Condition(Sql.IExpression left, Sql.BooleanOperators opr = Sql.BooleanOperators.None, Sql.IExpression right = null)
            => new Sql.Condition(left, opr, right);
        #endregion
    }
}
