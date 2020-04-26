namespace wj.AgnosticSql
{
    /// <summary>
    /// Identifies a named object.  When a named object is grouped with others, it is expected for 
    /// all named objects in the group to have unique names.
    /// </summary>
    public interface INamedObject
    {
        /// <summary>
        /// Gets the object's name.
        /// </summary>
        string Name { get; }
    }
}
