namespace wj.AgnosticSql
{
    /// <summary>
    /// Defines the capabilities required from SQL builder objects.
    /// </summary>
    public interface ISqlBuilder
    {
        /// <summary>
        /// Produces a string containing the SQL that accurately represents the given expression.
        /// </summary>
        /// <param name="expression">Input expression used to generate its SQL.</param>
        /// <returns>A string containing the given expression's SQL.</returns>
        string BuildSqlExpression(Sql.IExpression expression);

        /// <summary>
        /// Produces a string containing the SQL that accurately represents the given statement.
        /// </summary>
        /// <remarks>A statement differs from an expression in that a statement is a complete, 
        /// valid SQL command, while an expression may not be.</remarks>
        /// <param name="statement">Input statement used to generate its SQL.</param>
        /// <returns>A string containing the given statement's SQL.</returns>
        string BuildSqlStatement(Sql.IStatement statement);
    }
}
