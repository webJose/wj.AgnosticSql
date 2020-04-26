
namespace wj.AgnosticSql.Sql
{
    /// <summary>
    /// Function that eliminates repeated values.
    /// </summary>
    public class Distinct : Function
    {
        /// <summary>
        /// Creeates a new instance of this class.
        /// </summary>
        /// <param name="expr">Expression to be filtered out of repetitions.</param>
        public Distinct(IExpression expr)
            : base()
        {
            Guard.IsNotNull(expr, nameof(expr));
            Arguments.Add(expr);
        }
    }
}
