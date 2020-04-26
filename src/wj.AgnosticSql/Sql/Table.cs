namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// SQL table object.
    /// </summary>
    public class Table : AliasedElement, ITable
    {
        #region Properties
        /// <summary>
        /// Gets or sets the table's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the table schema's name.
        /// </summary>
        public string Schema { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="name">The table's name.</param>
        /// <param name="schema">The table schema's name.</param>
        /// <param name="alias">An optional alias for the table.</param>
        public Table(string name = null, string schema = null, string alias = null)
            : base(alias)
        {
            Name = name;
            Schema = schema;
        }
        #endregion
    }
}
