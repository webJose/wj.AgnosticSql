using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Base class that implements the default behavior of SQL objects that represent data 
    /// selection.
    /// </summary>
    public class Select : Expression, ISelect
    {
        #region Static Section
        /// <summary>
        /// Gets a collection of field names by examining the individual expressions that 
        /// compose the SELECT list.
        /// </summary>
        /// <param name="expressions">Collection of expression objects defined for a SELECT 
        /// statement.</param>
        /// <returns>The derived field names based on the given collection of expressions.</returns>
        internal static List<string> GetFieldNames(ICollection<IExpression> expressions)
        {
            List<string> names = new List<string>();
            foreach (IAliased exp in expressions)
            {
                if (!String.IsNullOrWhiteSpace(exp.Alias))
                {
                    names.Add(exp.Alias);
                }
                //See if it is a field.  If it is, return its name.
                Field f = exp as Field;
                if (exp != null)
                {
                    names.Add(f.Name);
                }
                //Default.
                names.Add(null);
            }
            return names;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collection of expressions that compose the SELECT list.
        /// </summary>
        private List<IExpression> Expressions { get; } = new List<IExpression>();

        /// <summary>
        /// Gets or sets a Boolean value that identifies the SELECT query as one that discards 
        /// duplicate records.  Set it to true to discard duplicates; false to keep duplicates.
        /// </summary>
        public bool Distinct { get; set; }
        #endregion

        #region ISelect
        /// <inheritdoc />
        public Pagination Pagination { get; set; }

        /// <inheritdoc />
        public IList<string> FieldNames
            => GetFieldNames(this);

        /// <inheritdoc />
        public void Add(IExpression item)
        {
            Expressions.Add(item);
            NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item);
            RaiseCollectionChanged(e);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Expressions.Clear();
            NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            RaiseCollectionChanged(e);
        }

        /// <inheritdoc />
        public bool Contains(IExpression item)
             => Expressions.Contains(item);

        /// <inheritdoc />
        public void CopyTo(IExpression[] array, int arrayIndex)
            => Expressions.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(IExpression item)
        {
            bool result = Expressions.Remove(item);
            if (result)
            {
                NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item);
                RaiseCollectionChanged(e);
            }
            return result;
        }

        /// <inheritdoc />
        public int Count
            => Expressions.Count;

        /// <inheritdoc />
        public bool IsReadOnly
            => false;


        /// <inheritdoc />
        public IEnumerator<IExpression> GetEnumerator()
            => Expressions.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => Expressions.GetEnumerator();

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <inheritdoc />
        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
            => CollectionChanged?.Invoke(this, e);
        #endregion
    }
}
