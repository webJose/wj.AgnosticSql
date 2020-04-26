namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines all possible binary Boolean operators that can be used to construct
    /// <see cref="Condition"/> objects.
    /// </summary>
    public enum BooleanOperators
    {
        /// <summary>
        /// Invalid selection.  Defined for comparison and variable initialization purposes.
        /// </summary>
        None = 0,
        /// <summary>
        /// Equality comparer.
        /// </summary>
        Equals = 1,
        /// <summary>
        /// Inequality comparer.
        /// </summary>
        NotEquals = 2,
        /// <summary>
        /// Greater than comparer.
        /// </summary>
        GreaterThan = 3,
        /// <summary>
        /// Less than comparer.
        /// </summary>
        LessThan = 4,
        /// <summary>
        /// Greater than or equality comparer.
        /// </summary>
        GreaterThanOrEquals = 5,
        /// <summary>
        /// Less than or equality comparer.
        /// </summary>
        LessThanOrEquals = 6,
        /// <summary>
        /// Text pattern matching comparer.
        /// </summary>
        Like = 7,
        /// <summary>
        /// Negated text pattern matching comparer.
        /// </summary>
        NotLike = 8,
        /// <summary>
        /// Boolean AND operator.
        /// </summary>
        And = 9,
        /// <summary>
        /// Boolean OR operator.
        /// </summary>
        Or = 10,
        /// <summary>
        /// ANSI determination of NULL values.
        /// </summary>
        IsNull = 11,
        /// <summary>
        /// Negated ANSI determination of NULL values.
        /// </summary>
        IsNotNull = 12
    }
}
