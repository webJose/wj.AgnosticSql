using System.Collections.ObjectModel;

namespace wj.AgnosticSql
{
    /// <summary>
    /// Collection of objects that can be referenced by name.  Objects collected under this 
    /// collection must have unique names.
    /// </summary>
    /// <typeparam name="T">The type of object in the collection.</typeparam>
    public class NamedObjectCollection<T> : KeyedCollection<string, T>
        where T : INamedObject
    {
        #region Methods
        /// <inheritdoc />
        protected override string GetKeyForItem(T item)
            => item.Name;
        #endregion
    }
}
