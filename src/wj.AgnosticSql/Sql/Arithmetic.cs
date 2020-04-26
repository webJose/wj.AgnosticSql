namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Represents an arithmetic binary operation.
    /// </summary>
    public class Arithmetic : Expression
    {
        #region Properties
        /// <summary>
        /// Gets or sets the operation's left operand.
        /// </summary>
        public IExpression Left { get; set; }

        /// <summary>
        /// Gets or sets the operation's right operand.
        /// </summary>
        public IExpression Right { get; set; }

        /// <summary>
        /// Gets or sets the arithmetic operator to use with the two operands.
        /// </summary>
        public ArithmeticOperations Operator { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <param name="opr">Binary arithmetic operator.</param>
        public Arithmetic(IExpression left, IExpression right, ArithmeticOperations opr)
        {
            Left = left;
            Right = right;
            Operator = opr;
        }
        #endregion
    }
}
