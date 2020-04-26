namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Base class for all SQL objects that can be aliased.
    /// </summary>
    public class AliasedElement : IAliased
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="alias">If provided, the alias to use for the element.</param>
        public AliasedElement(string alias = null)
        {
            Alias = alias;
        }
        #endregion

        #region IAliased
        /// <inheritdoc />
        public virtual string Alias { get; set; }
        #endregion
    }
}
