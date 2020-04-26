namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Defines all possible table join types.
    /// </summary>
    public enum TableJoins
    {
        /// <summary>
        /// Unknown join type.  Only used for comparison and variable initialization purposes.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Inner join.
        /// </summary>
        Inner = 1,
        /// <summary>
        /// Left outer join.
        /// </summary>
        Left = 2,
        /// <summary>
        /// Right outer join.
        /// </summary>
        Right = 3,
        /// <summary>
        /// Full outer join.
        /// </summary>
        FullOuter = 4
    }
}
