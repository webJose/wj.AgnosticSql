namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Field SQL object usually associated with tables or views.
    /// </summary>
    public class Field : Expression, INamedObject
    {
        #region Static Section
        /// <summary>
        /// Unique name that represents "All Fields" of a table or view.
        /// </summary>
        internal const string AllFieldsName = "*";

        /// <summary>
        /// Returns a field object that represents all fields.  The returned object is 
        /// not associated to any table.
        /// </summary>
        /// <returns></returns>
        public static Field AllFields() => new Field(AllFieldsName);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the field's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the field table owning the field.
        /// </summary>
        public ITable Table { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="name">The field's name.</param>
        /// <param name="table">The owning table.</param>
        /// <param name="alias">An optional alias name for the field.</param>
        public Field(string name, ITable table = null, string alias = null)
            : base(alias)
        {
            Name = name;
            Table = table;
        }
        #endregion
    }
}
