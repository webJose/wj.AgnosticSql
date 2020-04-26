using System.Collections.Generic;
using System.Collections.Specialized;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Represents an UPDATE statement that returns information from the updated record.
    /// </summary>
    public class SimpleUpdateAndReturnQuery : SimpleUpdateQuery, ISelect
    {
        #region Properties
        /// <summary>
        /// Gets a collection of expressions that are to be returned after the UPDATE operation.
        /// </summary>
        public List<IExpression> ReturnedExpressions { get; } = new List<IExpression>();
        #endregion

        #region ISelect
        /// <inheritdoc />
        public Pagination Pagination { get; set; }

        /// <inheritdoc />
        public IList<string> FieldNames
                => Select.GetFieldNames(ReturnedExpressions);

        /// <inheritdoc />
        public bool Grouped { get; set; }

        /// <inheritdoc />
        public string Alias { get; set; }

        /// <inheritdoc />
        public void Add(IExpression item)
        {
            ReturnedExpressions.Add(item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <inheritdoc />
        public bool Contains(IExpression item)
            => ReturnedExpressions.Contains(item);

        /// <inheritdoc />
        public void CopyTo(IExpression[] array, int arrayIndex)
            => ReturnedExpressions.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(IExpression item)
        {
            bool result = ReturnedExpressions.Remove(item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return result;
        }

        /// <inheritdoc />
        IEnumerator<IExpression> IEnumerable<IExpression>.GetEnumerator()
            => ReturnedExpressions.GetEnumerator();


        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="e">The event's argument object.</param>
        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
            => CollectionChanged?.Invoke(this, e);
        #endregion

    }
}
