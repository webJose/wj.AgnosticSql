using System;
using System.Collections.Generic;
using wj.AgnosticSql.Extensions;

namespace wj.AgnosticSql
{
    /// <summary>
    /// Defines fluent syntax extensions to build SQL query objects.
    /// </summary>
    public static class SqlModelExtensions
    {
        #region SimpleInsertQuery Extensions
        /// <summary>
        /// Defines the fields for an INSERT statement.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="table">The destination table for the new record.</param>
        /// <param name="fieldNames">The list of field names to include in the INSERT operation.</param>
        /// <returns>The given insert query statement to enable fluent syntax.</returns>
        public static TInsertQuery Insert<TInsertQuery>(this TInsertQuery insertQuery, Sql.ITable table, params string[] fieldNames)
            where TInsertQuery : Sql.SimpleInsertQuery
        {
            List<Sql.Field> fields = new List<Sql.Field>();
            foreach (string fieldName in fieldNames)
            {
                fields.Add(new Sql.Field(fieldName, table));
            }
            return Insert(insertQuery, fields);
        }

        /// <summary>
        /// Defines the fields for an INSERT statement.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="fields">The list of fields to include in the INSERT operation.</param>
        /// <returns>The given insert statement object to enable fluent syntax.</returns>
        /// <remarks>The destination table is inferred from the provided field objects.</remarks>
        public static TInsertQuery Insert<TInsertQuery>(this TInsertQuery insertQuery, IEnumerable<Sql.Field> fields)
            where TInsertQuery : Sql.SimpleInsertQuery
        {
            //All fields must be for the same table.
            Sql.ITable table = null;
            foreach (Sql.Field field in fields)
            {
                if (table != null && field.Table != table)
                {
                    throw new ArgumentException("All provided fields must be for the same table.");
                }
                table = table ?? field.Table;
            }
            insertQuery.Table = table;
            foreach (Sql.Field field in fields)
            {
                insertQuery.Add(field);
            }
            return insertQuery;
        }

        /// <summary>
        /// Defines the fields for an INSERT statement.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="fields">The list of fields to include in the INSERT operation.</param>
        /// <returns>The given insert query statement object to enable fluent syntax.</returns>
        /// <remarks>The destination table is inferred from the provided field objects.</remarks>
        public static TInsertQuery Insert<TInsertQuery>(this TInsertQuery insertQuery, params Sql.Field[] fields)
            where TInsertQuery : Sql.SimpleInsertQuery
            => Insert(insertQuery, fields);

        /// <summary>
        /// Sets the destination table for the given INSERT query object.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="table">The destination table for the new record.</param>
        /// <returns>The given insert statement object to enable fluent syntax.</returns>
        public static TInsertQuery Into<TInsertQuery>(this TInsertQuery insertQuery, Sql.ITable table)
            where TInsertQuery : Sql.SimpleInsertQuery
        {
            insertQuery.Table = table;
            return insertQuery;
        }

        /// <summary>
        /// Defines the values that compose the new record.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="expressions">The expressions that represent the values for the enumerated 
        /// fields to be inserted.</param>
        /// <returns>The given insert statement object to enable fluent syntax.</returns>
        public static TInsertQuery Values<TInsertQuery>(this TInsertQuery insertQuery, params Sql.IExpression[] expressions)
            where TInsertQuery : Sql.SimpleInsertQuery
        {
            Guard.EnsureEqualCounts(expressions, insertQuery, nameof(expressions));
            List<Sql.IExpression> exprs = new List<Sql.IExpression>();
            exprs.AddRange(expressions);
            insertQuery.Values.Add(exprs);
            return insertQuery;
        }

        /// <summary>
        /// Defines the values that compose the new record.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="parameterNames">The parameter names used to create parameter expressions 
        /// that are used as values for the enumerated fields to be inserted.</param>
        /// <returns>The given insert statement object to enable fluent syntax.</returns>
        public static TInsertQuery Values<TInsertQuery>(this TInsertQuery insertQuery, params string[] parameterNames)
            where TInsertQuery : Sql.SimpleInsertQuery
        {
            Sql.Parameter[] parameters = new Sql.Parameter[parameterNames.Length];
            int i = 0;
            foreach (string parameterName in parameterNames)
            {
                parameters[i++] = new Sql.Parameter(parameterName);
            }
            return Values(insertQuery, parameters);
        }

        /// <summary>
        /// Defines the values that compose the new record.
        /// </summary>
        /// <typeparam name="TInsertQuery">Type of INSERT statement.</typeparam>
        /// <param name="insertQuery">The insert statement object to operate on.</param>
        /// <param name="valuesFn">Function that returns the list of values for the enumerated fields 
        /// to be inserted.</param>
        /// <returns>The given insert statement object to enable fluent syntax.</returns>
        public static TInsertQuery Values<TInsertQuery>(this TInsertQuery insertQuery, Action<ICollection<Sql.IExpression>> valuesFn)
            where TInsertQuery : Sql.SimpleInsertQuery
        {
            Guard.IsNotNull(valuesFn, nameof(valuesFn));
            List<Sql.IExpression> expressions = new List<Sql.IExpression>();
            valuesFn(expressions);
            return Values(insertQuery, expressions.ToArray());
        }
        #endregion

        #region IWhere Extensions
        /// <summary>
        /// Defines the condition used in the SQL statement object to filter records.
        /// </summary>
        /// <typeparam name="TStatement">Type of SQL statement object.</typeparam>
        /// <param name="whereStatement">The SQL statement object to operate on.</param>
        /// <param name="condition">The expression used as the condition to the SQL statement.</param>
        /// <returns>The given SQL statement object to enable fluent syntax.</returns>
        public static TStatement Where<TStatement>(this TStatement whereStatement, Sql.IExpression condition)
            where TStatement : Sql.IWhere
        {
            whereStatement.Where = condition;
            return whereStatement;
        }

        /// <summary>
        /// Defines the condition used in the SQL statement object to filter records.
        /// </summary>
        /// <typeparam name="TStatement">Type of SQL statement object.</typeparam>
        /// <param name="whereStatement">The SQL statement object to operate on.</param>
        /// <param name="conditionFn">A function delegate that produces an expression to be used 
        /// as the condition to the SQL statement.</param>
        /// <returns>The given SQL statement object to enable fluent syntax.</returns>
        /// <remarks>The condition delegate function receives an expression builder helper object 
        /// that can be used to simplify the creation of expressions and conditions.</remarks>
        public static TStatement Where<TStatement>(this TStatement whereStatement, Func<ExpressionBuilderHelper, Sql.IExpression> conditionFn)
            where TStatement : Sql.IWhere
        {
            whereStatement.Where = conditionFn(new ExpressionBuilderHelper());
            return whereStatement;
        }
        #endregion

        #region IExpression Collection Extensions
        /// <summary>
        /// Adds a new constant value object to the given collection of expressions.
        /// </summary>
        /// <typeparam name="TExpressionCollection">The destination expressions collection.</typeparam>
        /// <typeparam name="TConstant">Type of value for the constant object.</typeparam>
        /// <param name="values">Destination expressions collection.</param>
        /// <param name="value">Constant value.</param>
        /// <returns>The given destination expressions collection to enable fluent syntax.</returns>
        public static TExpressionCollection Constant<TExpressionCollection, TConstant>(this TExpressionCollection values, TConstant value)
            where TExpressionCollection : ICollection<Sql.IExpression>
        {
            Sql.Constant<TConstant> constant = new Sql.Constant<TConstant>(value);
            values.Add(constant);
            return values;
        }

        /// <summary>
        /// Adds a new parameter expression object to the given collection of expressions.
        /// </summary>
        /// <typeparam name="TExpressionCollection">The destination expressions collection.</typeparam>
        /// <param name="values">Destination expressions collection.</param>
        /// <param name="paramName">The new parameter's name.</param>
        /// <returns>The given destination expressions collection to enable fluent syntax.</returns>
        public static TExpressionCollection Parameter<TExpressionCollection>(this TExpressionCollection values, string paramName)
            where TExpressionCollection : ICollection<Sql.IExpression>
        {
            Sql.Parameter param = new Sql.Parameter(paramName);
            values.Add(param);
            return values;
        }
        #endregion

        #region ITable Extensions
        /// <summary>
        /// Creates a new field object that is associated to the given table object.
        /// </summary>
        /// <param name="table">The table used as owner table for the new field object.</param>
        /// <param name="name">The field's name.</param>
        /// <param name="alias">The field's alias.</param>
        /// <returns>The newly created field object.</returns>
        public static Sql.Field Field(this Sql.ITable table, string name, string alias = null, Action<Sql.Field> fieldCfgFn = null)
        {
            Sql.Field field = new Sql.Field(name, table, alias);
            fieldCfgFn?.Invoke(field);
            return field;
        }

        /// <summary>
        /// Creates a new field object that represents all fields of the given table object.
        /// </summary>
        /// <param name="table">The table used as owner table for the new all fields object.</param>
        /// <returns>The newly created all fields object.</returns>
        public static Sql.Field AllFields(this Sql.ITable table)
            => new Sql.Field(Sql.Field.AllFieldsName, table);

        /// <summary>
        /// Joins the given table object with another table object using the specified join type and 
        /// join condition generator function.
        /// </summary>
        /// <param name="left">Left table in the JOIN operation.</param>
        /// <param name="right">Left table in the JOIN operation.</param>
        /// <param name="joinType">Type of join to perform.</param>
        /// <param name="conditionFn">Condition generator function used to obtain the join condition.</param>
        /// <param name="alias">Optional alias for the result.</param>
        /// <returns>A new <see cref="Sql.JoinedTable"/> object.</returns>
        public static Sql.JoinedTable Join(this Sql.ITable left, Sql.ITable right, Sql.TableJoins joinType, Func<Sql.ITable, Sql.ITable, Sql.Condition> conditionFn, string alias = null)
            => new Sql.JoinedTable(left, right, joinType, conditionFn(left, right), alias);

        /// <summary>
        /// Joins the given table object with another table object using an inner join.
        /// </summary>
        /// <param name="left">Left table in the JOIN operation.</param>
        /// <param name="right">Right table in the JOIN operation.</param>
        /// <param name="conditionFn">Condition generator function used to obtain the join condition.</param>
        /// <param name="alias">Optional alias for the result.</param>
        /// <returns>A new <see cref="Sql.JoinedTable"/> object.</returns>
        public static Sql.JoinedTable InnerJoin(this Sql.ITable left, Sql.ITable right, Func<Sql.ITable, Sql.ITable, Sql.Condition> conditionFn, string alias = null)
            => Join(left, right, Sql.TableJoins.Inner, conditionFn, alias);

        /// <summary>
        /// Joins the given table object with another table object using a left outer join.
        /// </summary>
        /// <param name="left">Left table in the JOIN operation.</param>
        /// <param name="right">Right table in the JOIN operation.</param>
        /// <param name="conditionFn">Condition generator function used to obtain the join condition.</param>
        /// <param name="alias">Optional alias for the result.</param>
        /// <returns>A new <see cref="Sql.JoinedTable"/> object.</returns>
        public static Sql.JoinedTable LeftJoin(this Sql.ITable left, Sql.ITable right, Func<Sql.ITable, Sql.ITable, Sql.Condition> conditionFn, string alias = null)
            => Join(left, right, Sql.TableJoins.Left, conditionFn, alias);

        /// <summary>
        /// Joins the given table object with another table object using a right outer join.
        /// </summary>
        /// <param name="left">Left table in the JOIN operation.</param>
        /// <param name="right">Right table in the JOIN operation.</param>
        /// <param name="conditionFn">Condition generator function used to obtain the join condition.</param>
        /// <param name="alias">Optional alias for the result.</param>
        /// <returns>A new <see cref="Sql.JoinedTable"/> object.</returns>
        public static Sql.JoinedTable RightJoin(this Sql.ITable left, Sql.ITable right, Func<Sql.ITable, Sql.ITable, Sql.Condition> conditionFn, string alias = null)
            => Join(left, right, Sql.TableJoins.Right, conditionFn, alias);

        /// <summary>
        /// Joins the given table object with another table object using full outer join.
        /// </summary>
        /// <param name="left">Left table in the JOIN operation.</param>
        /// <param name="right">Right table in the JOIN operation.</param>
        /// <param name="conditionFn">Condition generator function used to obtain the join condition.</param>
        /// <param name="alias">Optional alias for the result.</param>
        /// <returns>A new <see cref="Sql.JoinedTable"/> object.</returns>
        public static Sql.JoinedTable FullOuterJoin(this Sql.ITable left, Sql.ITable right, Func<Sql.ITable, Sql.ITable, Sql.Condition> conditionFn, string alias = null)
            => Join(left, right, Sql.TableJoins.FullOuter, conditionFn, alias);
        #endregion

        #region ISelect Extensions
        /// <summary>
        /// Defines the list of expressions that compose a SELECT clause.
        /// </summary>
        /// <typeparam name="TSelect">Type of select statement.</typeparam>
        /// <param name="selectStatement">SELECT statment object to operate on.</param>
        /// <param name="selectList">List of expressions to be added to the SELECT list.</param>
        /// <returns>The given select statement to enable fluent syntax.</returns>
        public static TSelect Select<TSelect>(this TSelect selectStatement, params Sql.IExpression[] selectList)
            where TSelect : Sql.ISelect
            => Select(selectStatement, (IEnumerable<Sql.IExpression>)selectList);

        /// <summary>
        /// Defines the list of expressions that compose a SELECT clause.
        /// </summary>
        /// <typeparam name="TSelect">Type of select statement.</typeparam>
        /// <param name="selectStatement">SELECT statment object to operate on.</param>
        /// <param name="selectList">List of expressions to be added to the SELECT list.</param>
        /// <returns>The given select statement to enable fluent syntax.</returns>
        public static TSelect Select<TSelect>(this TSelect selectStatement, IEnumerable<Sql.IExpression> selectList)
            where TSelect : Sql.ISelect
        {
            foreach (Sql.IExpression expr in selectList)
            {
                selectStatement.Add(expr);
            }
            return selectStatement;
        }
        #endregion

        #region IFrom Extensions
        /// <summary>
        /// Sets the given table as the table source of data for SQL statements that support the FROM 
        /// clause.
        /// </summary>
        /// <typeparam name="TFrom">The type of query object.</typeparam>
        /// <param name="query">The query object to operate on.</param>
        /// <param name="table">The table source of data.</param>
        /// <returns>The given query to enable fluent syntax.</returns>
        public static TFrom From<TFrom>(this TFrom query, Sql.ITable table)
            where TFrom : Sql.IFrom
        {
            query.From = table;
            return query;
        }
        #endregion

        #region IUpdate Extensions
        /// <summary>
        /// Defines the list of field-value pairs that compose an UPDATE statement.
        /// </summary>
        /// <typeparam name="TUpdate">The type of update query.</typeparam>
        /// <param name="updateQuery">The UPDATE statement to operate on.</param>
        /// <param name="fields">List of field-value pairs to include in the UPDATE statement.</param>
        /// <returns>The given UPDATE statement to enable fluent syntax.</returns>
        public static TUpdate Update<TUpdate>(this TUpdate updateQuery, params Sql.SetField[] fields)
            where TUpdate : Sql.IUpdate
        {
            foreach (Sql.SetField sf in fields)
            {
                updateQuery.Add(sf);
            }
            return updateQuery;
        }

        /// <summary>
        /// Adds a single field-value pair to the given UPDATE statement.
        /// </summary>
        /// <typeparam name="TUpdate">The type of UPDATE statement.</typeparam>
        /// <param name="updateQuery">The UPDATE statement to operate on.</param>
        /// <param name="field">Field to update.</param>
        /// <param name="value">Expression that provides the field's new value.</param>
        /// <returns>The given UPDATE statement to enable fluent syntax.</returns>
        public static TUpdate Update<TUpdate>(this TUpdate updateQuery, Sql.Field field, Sql.IExpression value)
            where TUpdate : Sql.IUpdate
            => Update(updateQuery, new Sql.SetField(field, value));

        /// <summary>
        /// Adds a single field-value pair to the given UPDATE statement and sets the value with a 
        /// new <see cref="Sql.Parameter"/> expression.
        /// </summary>
        /// <typeparam name="TUpdate">The type of UPDATE statement.</typeparam>
        /// <param name="updateQuery">The UPDATE statement to operate on.</param>
        /// <param name="field">Field to update.</param>
        /// <param name="parameterName">The parameter named used to construct the 
        /// <see cref="Sql.Parameter"/> expression.</param>
        /// <returns>The given UPDATE statement to enable fluent syntax.</returns>
        public static TUpdate Update<TUpdate>(this TUpdate updateQuery, Sql.Field field, string parameterName)
            where TUpdate : Sql.IUpdate
            => Update(updateQuery, field, new Sql.Parameter(parameterName));
        #endregion

        #region IExpression Extensions
        /// <summary>
        /// Joins the given expression to another one using the Boolean AND operator, creating a 
        /// new <see cref="Sql.Condition"/> expression.
        /// </summary>
        /// <param name="expression">The expression to the left of the AND operator.</param>
        /// <param name="conditionFn">A fuction delegate that, when executed, returns the 
        /// expression to the right of the AND operator.</param>
        /// <returns>A new <see cref="Sql.Condition"/> object that has the original expression and 
        /// the expression returned by the <paramref name="conditionFn"/> delegate joined by the 
        /// AND operator.</returns>
        public static Sql.IExpression And(this Sql.IExpression expression, Func<ExpressionBuilderHelper, Sql.IExpression> conditionFn)
            => new Sql.Condition(expression, Sql.BooleanOperators.And, conditionFn(new ExpressionBuilderHelper()));

        /// <summary>
        /// Joins the given expression to another one using the Boolean OR operator, creating a 
        /// new <see cref="Sql.Condition"/> expression.
        /// </summary>
        /// <param name="expression">The expression to the left of the OR operator.</param>
        /// <param name="conditionFn">A fuction delegate that, when executed, returns the 
        /// expression to the right of the OR operator.</param>
        /// <returns>A new <see cref="Sql.Condition"/> object that has the original expression and 
        /// the expression returned by the <paramref name="conditionFn"/> delegate joined by the 
        /// OR operator.</returns>
        public static Sql.IExpression Or(this Sql.IExpression expression, Func<ExpressionBuilderHelper, Sql.IExpression> conditionFn)
            => new Sql.Condition(expression, Sql.BooleanOperators.Or, conditionFn(new ExpressionBuilderHelper()));

        /// <summary>
        /// Creates a new <see cref="Sql.SortExpression"/> object from the given expression 
        /// object with an <see cref="Sql.SortDirections.Ascending"/> sort direction.
        /// </summary>
        /// <param name="expression">Expression object used in the new sort expression object.</param>
        /// <returns>A new sort expression object that specifies the given expression object as 
        /// the expression to sort in <see cref="Sql.SortDirections.Ascending"/> fashion.</returns>
        public static Sql.SortExpression Ascending(this Sql.IExpression expression)
            => new Sql.SortExpression(expression, Sql.SortDirections.Ascending);

        /// <summary>
        /// Creates a new <see cref="Sql.SortExpression"/> object from the given expression 
        /// object with a <see cref="Sql.SortDirections.Descending"/> sort direction.
        /// </summary>
        /// <param name="expression">Expression object used in the new sort expression object.</param>
        /// <returns>A new sort expression object that specifies the given expression object as 
        /// the expression to sort in <see cref="Sql.SortDirections.Descending"/> fashion.</returns>
        public static Sql.SortExpression Descending(this Sql.IExpression expression)
            => new Sql.SortExpression(expression, Sql.SortDirections.Descending);
        #endregion

        #region SortExpression Extensions
        /// <summary>
        /// Creates a new list of expression objects containing the given expressions.
        /// </summary>
        /// <param name="expression">First expression in the collection.</param>
        /// <param name="secondExpression">Second expression in the collection.</param>
        /// <returns>The newly created list of expression objects.</returns>
        public static IList<Sql.SortExpression> ThenBy(this Sql.SortExpression expression, Sql.SortExpression secondExpression)
            => new List<Sql.SortExpression>() { expression, secondExpression };

        /// <summary>
        /// Adds the given expression object to the given list of expression objects.
        /// </summary>
        /// <param name="expressions">Destination expression list for the given expression object.</param>
        /// <param name="expression">Expression object to include in the list of expressions.</param>
        /// <returns>The given expression list object to enable fluent syntax.</returns>
        public static IList<Sql.SortExpression> ThenBy(this IList<Sql.SortExpression> expressions, Sql.SortExpression expression)
        {
            expressions.Add(expression);
            return expressions;
        }
        #endregion

        #region ISortable Extensions
        /// <summary>
        /// Adds the given expression objects to the ORDER BY list of expressions in the provided 
        /// SQL statement.  All expressions are set to sort in 
        /// <see cref="Sql.SortDirections.Ascending"/> order.
        /// </summary>
        /// <typeparam name="TStatement">Type of SQL statement.</typeparam>
        /// <param name="sortedStatement">SQL statement object whose ORDER BY list is being set.</param>
        /// <param name="expressions">List of expression objects that are used to create sort 
        /// expression objects that sort in <see cref="Sql.SortDirections.Ascending"/> order.</param>
        /// <returns>The given SQL statement to enable fluent syntax.</returns>
        public static TStatement OrderBy<TStatement>(this TStatement sortedStatement, params Sql.IExpression[] expressions)
            where TStatement : Sql.ISortable
        {
            foreach (Sql.IExpression expr in expressions)
            {
                sortedStatement.OrderBy.Add(new Sql.SortExpression(expr));
            }
            return sortedStatement;
        }

        /// <summary>
        /// Adds the given sort expression objects to the ORDER BY list of expressions in the 
        /// provided SQL statement.
        /// </summary>
        /// <typeparam name="TStatement">Type of SQL statement.</typeparam>
        /// <param name="sortedStatement">SQL statement object whose ORDER BY list is being set.</param>
        /// <param name="expressions">List of sort expression objects.</param>
        /// <returns>The given SQL statement to enable fluent syntax.</returns>
        public static TStatement OrderBy<TStatement>(this TStatement sortedStatement, params Sql.SortExpression[] expressions)
            where TStatement : Sql.ISortable
            => OrderBy(sortedStatement, expressions);

        /// <summary>
        /// Adds the given sort expression objects to the ORDER BY list of expressions in the 
        /// provided SQL statement.
        /// </summary>
        /// <typeparam name="TStatement">Type of SQL statement.</typeparam>
        /// <param name="sortedStatement">SQL statement object whose ORDER BY list is being set.</param>
        /// <param name="expressions">List of sort expression objects.</param>
        /// <returns>The given SQL statement to enable fluent syntax.</returns>
        public static TStatement OrderBy<TStatement>(this TStatement sortedStatement, IEnumerable<Sql.SortExpression> expressions)
            where TStatement : Sql.ISortable
        {
            sortedStatement.OrderBy.AddRange(expressions);
            return sortedStatement;
        }
        #endregion

        #region FieldCollection Extensions
        /// <summary>
        /// Creates a new table fields context object to assist populating a collection of 
        /// field objects.
        /// </summary>
        /// <param name="fields">The destination collection of field objects.</param>
        /// <param name="table">The table that is associated with the new field objects.</param>
        /// <returns>A newly created table fields context object.</returns>
        public static SqlTableFieldsContext Table(this Sql.FieldCollection fields, Sql.ITable table)
            => new SqlTableFieldsContext(fields, table);

        /// <summary>
        /// Creates a new table fields context object to assist populating a collection of 
        /// field objects from a previous context object.  Used to chain field creation for 
        /// multiple tables in a single fields collection.
        /// </summary>
        /// <param name="fieldsContext">Previous field context object.</param>
        /// <param name="table">The table that is associated with the new field objects.</param>
        /// <returns>A newly created table fields context object.</returns>
        public static SqlTableFieldsContext Table(this SqlTableFieldsContext fieldsContext, Sql.ITable table)
            => new SqlTableFieldsContext(fieldsContext.Fields, table);
        #endregion

        #region Batch Extensions
        /// <summary>
        /// Adds the given query object to the batch object.
        /// </summary>
        /// <typeparam name="TBatch">Type of batch object.</typeparam>
        /// <typeparam name="TQuery">Type of query object being added.</typeparam>
        /// <param name="batch">The destination batch object for the query.</param>
        /// <param name="query">Query object to be added to the batch object.</param>
        /// <param name="configFn">Query configuration delegate that can be used to modify the 
        /// <paramref name="query"/> object.</param>
        /// <returns>The given batch object to enable fluent syntax.</returns>
        private static TBatch Query<TBatch, TQuery>(this TBatch batch, TQuery query, Action<TQuery> configFn)
            where TBatch : Sql.Batch
            where TQuery : Sql.IStatement
        {
            batch.Add(query);
            configFn?.Invoke(query);
            return batch;
        }

        /// <summary>
        /// Adds a new SELECT query object to the given batch object.
        /// </summary>
        /// <typeparam name="TBatch">Type of batch object.</typeparam>
        /// <param name="batch">The destination batch object for the query.</param>
        /// <param name="configFn">Query configuration function that can be used to define or 
        /// modify the new query object.</param>
        /// <returns>The given batch object to enable fluent syntax.</returns>
        public static TBatch SelectQuery<TBatch>(this TBatch batch, Action<Sql.SelectQuery> configFn)
            where TBatch : Sql.Batch
            => batch.Query(new Sql.SelectQuery(), configFn);

        /// <summary>
        /// Adds a new simple INSERT query object to the given batch object.
        /// </summary>
        /// <typeparam name="TBatch">Type of batch object.</typeparam>
        /// <param name="batch">The destination batch object for the query.</param>
        /// <param name="configFn">Query configuration function that can be used to define or 
        /// modify the new query object.</param>
        /// <returns>The given batch object to enable fluent syntax.</returns>
        public static TBatch SimpleInsertQuery<TBatch>(this TBatch batch, Action<Sql.SimpleInsertQuery> configFn)
            where TBatch : Sql.Batch
            => batch.Query(new Sql.SimpleInsertQuery(), configFn);

        /// <summary>
        /// Adds a new simple INSERT and SELECT query object to the given batch object.
        /// </summary>
        /// <typeparam name="TBatch">Type of batch object.</typeparam>
        /// <param name="batch">The destination batch object for the query.</param>
        /// <param name="configFn">Query configuration function that can be used to define or 
        /// modify the new query object.</param>
        /// <returns>The given batch object to enable fluent syntax.</returns>
        public static TBatch SimpleInsertAndReturnQuery<TBatch>(this TBatch batch, Action<Sql.SimpleInsertAndReturnQuery> configFn)
            where TBatch : Sql.Batch
            => batch.Query(new Sql.SimpleInsertAndReturnQuery(), configFn);
        #endregion
    }
}
