using System.Collections.Generic;
using System.Collections.Specialized;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines an INSERT statement that returns data about the newly inserted record.
    /// </summary>
    public class SimpleInsertAndReturnQuery : SimpleInsertQuery, ISelect
    {
        #region Properties
        /// <summary>
        /// Gets a collection of expressions that define the data being returned.
        /// </summary>
        private List<IExpression> ReturnedExpressions { get; } = new List<IExpression>();
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
        public bool IsReadOnly
            => false;

        /// <inheritdoc />
        IEnumerator<IExpression> IEnumerable<IExpression>.GetEnumerator()
            => ReturnedExpressions.GetEnumerator();

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="e">Event argument object.</param>
        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
            => CollectionChanged?.Invoke(this, e);
        #endregion
    }
}
