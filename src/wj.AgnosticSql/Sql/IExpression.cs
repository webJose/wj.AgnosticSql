namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics SQL objects that can return a scalar value must implement.
    /// </summary>
    public interface IExpression : IAliased
    {
        /// <summary>
        /// Gets or sets a Boolean value that hints if the expression is grouped to other expressions.
        /// </summary>
        /// <remarks>SQL builder implementations may opt to honor this value or not.</remarks>
        bool Grouped { get; set; }
    }
}
