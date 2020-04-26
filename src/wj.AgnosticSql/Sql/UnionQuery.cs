using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines a UNION SELECT statement.
    /// </summary>
    public class UnionQuery : Collection<ISelect>, ISelect, IStatement
    {
        #region Static Section
        /// <summary>
        /// Customized error message to prevent operations on SELECT expressions indirectly 
        /// from a <see cref="UnionQuery"/> object.
        /// </summary>
        private static readonly string ISelectInvalidOperationMessage = $"{nameof(UnionQuery)} statements do not control their field definitions.  Modify the underlying expressions instead.";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a Boolean value that indicates if the UNION operation to perform is 
        /// welcoming to record duplicates or not.
        /// </summary>
        public bool UnionAll { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Makes sure the given SELECT item's field count is the same as the other SELECT 
        /// items currently in the union object.  If not, an exception is thrown.
        /// </summary>
        /// <param name="item">The SELECT item being added to the Union object.</param>
        /// <exception cref="ArgumentException">Thrown if the field count does not match to the 
        /// field count of the other SELECT statements.</exception>
        protected void EnsureEqualExpressionCount(ISelect item)
        {
            if (Count > 0)
            {
                Guard.AreEqual
                (
                    item.Count,
                    this[0].Count,
                    "The SELECT statement being added does not contain the same number of expressions as other SELECT statements already in the collection."
                );
            }
        }

        /// <inheritdoc />
        protected override void InsertItem(int index, ISelect item)
        {
            EnsureEqualExpressionCount(item);
            base.InsertItem(index, item);
        }

        /// <inheritdoc />
        protected override void SetItem(int index, ISelect item)
        {
            // Counts are not checked if the UNION is empty or if there is one item and is being replaced.
            if (index != 0 || Count > 1)
            {
                EnsureEqualExpressionCount(item);
            }
            base.SetItem(index, item);
        }
        #endregion

        #region ISelect
        /// <inheritdoc />
        void ICollection<IExpression>.Add(IExpression item)
        {
            throw new InvalidOperationException(ISelectInvalidOperationMessage);
        }

        /// <inheritdoc />
        void ICollection<IExpression>.Clear()
        {
            throw new InvalidOperationException(ISelectInvalidOperationMessage);
        }

        /// <inheritdoc />
        bool ICollection<IExpression>.Contains(IExpression item)
        {
            if (Count == 0)
            {
                return false;
            }
            foreach (ISelect select in this)
            {
                if (select.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <inheritdoc />
        void ICollection<IExpression>.CopyTo(IExpression[] array, int arrayIndex)
        {
            if (Count == 0)
            {
                return;
            }
            this[0].CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        bool ICollection<IExpression>.Remove(IExpression item)
        {
            throw new InvalidOperationException(ISelectInvalidOperationMessage);
        }

        /// <inheritdoc />
        int ICollection<IExpression>.Count
            => Count == 0 ? 0 : this[0].Count;

        /// <inheritdoc />
        bool ICollection<IExpression>.IsReadOnly
            => true;

        /// <inheritdoc />
        IEnumerator<IExpression> IEnumerable<IExpression>.GetEnumerator()
            => Count == 0 ? null : this[0].GetEnumerator();

        /// <inheritdoc />
        string IAliased.Alias { get; set; }

        /// <inheritdoc />
        public List<Expression> OrderBy { get; } = new List<Expression>();

        /// <inheritdoc />
        public Pagination Pagination { get; set; }

        /// <inheritdoc />
        public IList<string> FieldNames
            => Count == 0 ? new List<string>() : this[0].FieldNames;

        /// <inheritdoc />
        public bool Grouped { get; set; }

        /// <inheritdoc />
        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
                foreach (ISelect sel in this)
                {
                    sel.CollectionChanged += value;
                }
            }
            remove
            {
                foreach (ISelect sel in this)
                {
                    sel.CollectionChanged -= value;
                }
            }
        }
        #endregion
    }
}
