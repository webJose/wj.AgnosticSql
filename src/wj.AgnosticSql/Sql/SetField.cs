namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines a field assignation clause.  Used in UPDATE clauses.
    /// </summary>
    public class SetField : INamedObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the field to update.
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        /// Gets or sets the value to assign to the field.
        /// </summary>
        public IExpression Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="field">Field receiving the value.</param>
        /// <param name="value">Value being assigned to the field.</param>
        public SetField(Field field, IExpression value)
        {
            Field = field;
            Value = value;
        }
        #endregion

        #region INamedObject
        /// <inheritdoc />
        public string Name
            => Field?.Name;
        #endregion
    }
}
