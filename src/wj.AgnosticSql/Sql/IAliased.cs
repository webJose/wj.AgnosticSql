namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics SQL objects that can have an alias must expose.
    /// </summary>
    public interface IAliased
    {
        /// <summary>
        /// Gets or sets the alias for the SQL object.
        /// </summary>
        string Alias { get; set; }
    }
}
