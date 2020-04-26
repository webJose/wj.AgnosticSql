namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Table SQL object that is obtained by joining two source tables.
    /// </summary>
    public class JoinedTable : AliasedElement, ITable
    {
        #region Properties
        /// <summary>
        /// Gets or sets the table to the left of the join.
        /// </summary>
        public ITable Left { get; set; }

        /// <summary>
        /// Gets or sets the table to the right of the join.
        /// </summary>
        public ITable Right { get; set; }

        /// <summary>
        /// Gets or sets the join type between the <see cref="Left"/> and <see cref="Right"/> 
        /// tables.
        /// </summary>
        public TableJoins JoinType { get; set; }

        /// <summary>
        /// Gets or sets the condition used to match records between the two table sources.
        /// </summary>
        public Condition JoinCondition { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="left">Table to the left of the join.</param>
        /// <param name="right">Table to the right of the join.</param>
        /// <param name="alias">Optional alias for the result of the join operation.  If this is 
        /// set, then SQL builders should group the result of the join under the given alias.</param>
        public JoinedTable(ITable left, ITable right, TableJoins joinType, Condition joinCondition, string alias = null)
            : base(alias)
        {
            Guard.IsNotNull(left, nameof(left));
            Guard.IsNotNull(right, nameof(right));
            Guard.AreNotEqual(joinType, TableJoins.Unknown, nameof(joinType));
            Left = left;
            Right = right;
            JoinType = joinType;
            JoinCondition = joinCondition;
        }
        #endregion
    }
}
