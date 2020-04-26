namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// A SQL object expression, or in other words, a SQL object that produces a scalar value.
    /// </summary>
#pragma warning disable 660, 661
    public class Expression : AliasedElement, IExpression
#pragma warning restore 660, 661
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="alias">Optional alias for the expression.</param>
        public Expression(string alias = null)
            : base(alias)
        { }
        #endregion

        #region IExpression
        /// <inheritdoc />
        public bool Grouped { get; set; }
        #endregion

        #region Operators
        /// <summary>
        /// Creates a new <see cref="Condition"/> object using the two operands and the 
        /// <see cref="BooleanOperators.Equals"/> operator.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Condition"/> object that checks for equality.</returns>
        public static Condition operator ==(Expression op1, IExpression op2)
            => new Condition(op1, BooleanOperators.Equals, op2);

        /// <summary>
        /// Creates a new <see cref="Condition"/> object using the two operands and the 
        /// <see cref="BooleanOperators.NotEquals"/> operator.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Condition"/> object that checks for inequality.</returns>
        public static Condition operator !=(Expression op1, IExpression op2)
            => new Condition(op1, BooleanOperators.NotEquals, op2);

        /// <summary>
        /// Creates a new <see cref="Condition"/> object using the two operands and the 
        /// <see cref="BooleanOperators.GreaterThan"/> operator.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Condition"/> object that checks for inequality.</returns>
        public static Condition operator >(Expression op1, IExpression op2)
            => new Condition(op1, BooleanOperators.GreaterThan, op2);

        /// <summary>
        /// Creates a new <see cref="Condition"/> object using the two operands and the 
        /// <see cref="BooleanOperators.GreaterThanOrEquals"/> operator.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Condition"/> object that checks for inequality.</returns>
        public static Condition operator >=(Expression op1, IExpression op2)
            => new Condition(op1, BooleanOperators.GreaterThanOrEquals, op2);

        /// <summary>
        /// Creates a new <see cref="Condition"/> object using the two operands and the 
        /// <see cref="BooleanOperators.LessThan"/> operator.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Condition"/> object that checks for inequality.</returns>
        public static Condition operator <(Expression op1, IExpression op2)
            => new Condition(op1, BooleanOperators.LessThan, op2);

        /// <summary>
        /// Creates a new <see cref="Condition"/> object using the two operands and the 
        /// <see cref="BooleanOperators.LessThanOrEquals"/> operator.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Condition"/> object that checks for inequality.</returns>
        public static Condition operator <=(Expression op1, IExpression op2)
            => new Condition(op1, BooleanOperators.LessThanOrEquals, op2);

        /// <summary>
        /// Creates a new <see cref="Not"/> expression object that negates the given expression.
        /// </summary>
        /// <param name="op">Expression to negate.</param>
        /// <returns>A newly created <see cref="Not"/> object that negates the given expression.</returns>
        public static Not operator !(Expression op)
            => new Not(op);

        /// <summary>
        /// Creates an <see cref="Arithmetic"/> expression that represents the sum of the given 
        /// expressions.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Arithmetic"/> object that sums the two expressions.</returns>
        public static Arithmetic operator +(Expression op1, IExpression op2)
            => new Arithmetic(op1, op2, ArithmeticOperations.Sum);

        /// <summary>
        /// Creates an <see cref="Arithmetic"/> expression that represents the subtraction of the 
        /// given expressions.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Arithmetic"/> object that subtracts the two 
        /// expressions.</returns>
        public static Arithmetic operator -(Expression op1, IExpression op2)
            => new Arithmetic(op1, op2, ArithmeticOperations.Subtract);

        /// <summary>
        /// Creates an <see cref="Arithmetic"/> expression that represents the multiplication of 
        /// the given expressions.
        /// </summary>
        /// <param name="op1">Left operand.</param>
        /// <param name="op2">Right operand.</param>
        /// <returns>A newly created <see cref="Arithmetic"/> object that multiplies the two 
        /// expressions.</returns>
        public static Arithmetic operator *(Expression op1, IExpression op2)
            => new Arithmetic(op1, op2, ArithmeticOperations.Multiply);

        /// <summary>
        /// Creates an <see cref="Arithmetic"/> expression that represents the division of the 
        /// given expressions.
        /// </summary>
        /// <param name="op1">Numerator.</param>
        /// <param name="op2">Denominator.</param>
        /// <returns>A newly created <see cref="Arithmetic"/> object that divides the numerator by 
        /// the denominator.</returns>
        public static Arithmetic operator /(Expression op1, IExpression op2)
            => new Arithmetic(op1, op2, ArithmeticOperations.Divide);

        /// <summary>
        /// Creates an <see cref="Arithmetic"/> expression that represents the modulus of the 
        /// integer division between the given expressions.
        /// </summary>
        /// <param name="op1">Numerator.</param>
        /// <param name="op2">Denominator.</param>
        /// <returns>A newly created <see cref="Arithmetic"/> object that calculates the modulus 
        /// (or remnant) of the integer division between the given expressions.</returns>
        public static Arithmetic operator %(Expression op1, IExpression op2)
            => new Arithmetic(op1, op2, ArithmeticOperations.Modulus);
        #endregion
    }
}
