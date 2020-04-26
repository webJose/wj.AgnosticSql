namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines all possible binary arithmetic operators that can be used when creating 
    /// <see cref="Arithmetic"/> objects.
    /// </summary>
    public enum ArithmeticOperations
    {
        /// <summary>
        /// No operator.  Do not use.  Only defined for variable initialization and comparison.
        /// </summary>
        None = 0,
        /// <summary>
        /// Sumation.
        /// </summary>
        Sum = 1,
        /// <summary>
        /// Subtraction.
        /// </summary>
        Subtract = 2,
        /// <summary>
        /// Multiplication.
        /// </summary>
        Multiply = 3,
        /// <summary>
        /// Division.
        /// </summary>
        Divide = 4,
        /// <summary>
        /// Modulus.
        /// </summary>
        Modulus = 5
    }
}