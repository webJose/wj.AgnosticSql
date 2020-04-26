
namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Expression object that represents a SQL parameter.
    /// </summary>
    public class Parameter : Expression
    {
        #region Properties
        /// <summary>
        /// Gets or sets the parameter's name.
        /// </summary>
        public string Name { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        public Parameter(string name)
        {
            Guard.IsNotNull(name, nameof(name));
            Name = name;
        }
        #endregion
    }
}
