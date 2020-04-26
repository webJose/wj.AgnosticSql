namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the characteristics that SQL statements capable of filtering data must expose.
    /// </summary>
    public interface IWhere
    {
        /// <summary>
        /// Gets or sets an expression object representing the filtering criteria to be used 
        /// during a filtering operation.
        /// </summary>
        IExpression Where { get; set; }
    }
}
