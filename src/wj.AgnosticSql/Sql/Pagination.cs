namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines the pagination data required to returned results of data in a paged 
    /// (or batch) fashion.
    /// </summary>
    public class Pagination
    {
        #region Properties
        /// <summary>
        /// Gets or sets the required page number.
        /// </summary>
        public long? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of records that define a page.
        /// </summary>
        public long? PageSize { get; set; }
        #endregion
    }
}
