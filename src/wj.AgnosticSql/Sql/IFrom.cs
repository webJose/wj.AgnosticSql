namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics SQL objects that have source tables must expose.
    /// </summary>
    public interface IFrom
    {
        /// <summary>
        /// Gets or sets the source table for the SQL statement.
        /// </summary>
        ITable From { get; set; }
    }
}
