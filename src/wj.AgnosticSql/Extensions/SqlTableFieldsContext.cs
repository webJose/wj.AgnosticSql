using System;

namespace wj.AgnosticSql.Extensions
{
    /// <summary>
    /// Context class that assists in the creation of SQL field objects.
    /// </summary>
    public class SqlTableFieldsContext
    {
        #region Properties
        /// <summary>
        /// Gets the table object used in this context.
        /// </summary>
        internal Sql.ITable Table { get; }

        /// <summary>
        /// Gets the collection of fields where new fields are added.
        /// </summary>
        internal Sql.FieldCollection Fields { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="fields">Collection of fields where new fields are added.</param>
        /// <param name="table">Table object used associate newly created field objects.</param>
        internal SqlTableFieldsContext(Sql.FieldCollection fields, Sql.ITable table)
        {
            Table = table;
            Fields = fields;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a new field to the specified fields collection.
        /// </summary>
        /// <param name="name">The new field's name.</param>
        /// <param name="alias">The new field's alias.</param>
        /// <param name="fieldConfigFn">Configuration function that provides access to the newly created 
        /// field object.</param>
        /// <returns>This context object to allow for fluent syntax.</returns>
        public SqlTableFieldsContext Field(string name, string alias = null, Action<Sql.Field> fieldConfigFn = null)
        {
            Sql.Field field = Table.Field(name, alias);
            Fields.Add(field);
            fieldConfigFn?.Invoke(field);
            return this;
        }
        #endregion
    }
}
