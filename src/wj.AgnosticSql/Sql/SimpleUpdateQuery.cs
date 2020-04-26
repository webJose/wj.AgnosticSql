using System.Collections;
using System.Collections.Generic;

namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Represents an UPDATE statement.
    /// </summary>
    public class SimpleUpdateQuery : IUpdate, IStatement
    {
        #region Properties
        /// <summary>
        /// Gets a collection of field-setting clauses that define how the record is updated.
        /// </summary>
        private SetFieldCollection Fields { get; } = new SetFieldCollection();
        #endregion

        #region IUpdate
        /// <inheritdoc />
        public ITable From { get; set; }

        /// <inheritdoc />
        public IExpression Where { get; set; }

        /// <inheritdoc />
        public void Add(SetField item)
            => Fields.Add(item);

        /// <inheritdoc />
        public void Clear()
            => Fields.Clear();

        /// <inheritdoc />
        public bool Contains(SetField item)
            => Fields.Contains(item);

        /// <inheritdoc />
        public void CopyTo(SetField[] array, int arrayIndex)
            => Fields.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(SetField item)
            => Fields.Remove(item);

        /// <inheritdoc />
        public int Count
            => Fields.Count;

        /// <inheritdoc />
        public bool IsReadOnly
            => false;

        /// <inheritdoc />
        public IEnumerator<SetField> GetEnumerator()
            => Fields.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => Fields.GetEnumerator();
        #endregion
    }
}
