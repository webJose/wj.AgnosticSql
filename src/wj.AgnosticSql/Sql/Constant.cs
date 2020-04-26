using System;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Represents a constant value.
    /// </summary>
    /// <typeparam name="TConst">Type of value.</typeparam>
    public class Constant<TConst> : Expression
    {
        #region Static Section
        /// <summary>
        /// Constant value that represents no value.
        /// </summary>
        public static readonly Constant<string> NoValue = new Constant<string>(null);

        /// <summary>
        /// Constant value that represents a database NULL value.
        /// </summary>
        public static readonly Constant<DBNull> NullValue = new Constant<DBNull>(DBNull.Value);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the constant's value.
        /// </summary>
        public TConst Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="value">The new constant's value.</param>
        public Constant(TConst value)
        {
            Value = value;
        }
        #endregion
    }
}
