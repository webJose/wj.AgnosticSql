# wj.AgnosticSql

Use this library, designed specifically to cover Dapper, if you would like to write your queries in C# in an independent manner from your RDBMS of choice.

## Motivation

This project was born from three seeds:

1. My love for Dapper!
2. My architect's indecision to select an RDBMS while the project is ongoing.
3. Dapper does not support the `IQueryable` interface, and therefore the cool LINQ cannot be easily used with Dapper (see [https://dapper-tutorial.net/knowledge-base/38826292/how-to-use-dapper-with-linq]).

## Project Objectives

**Main Objective:** To be able to write a Data Access layer for an RDBMS without actually binding the layer to any specific RDBMS.

Among the specific objectives, we have:

1. To be able to write SQL that can work on any RDBMS of choice.  Sure, there is ANSI SQL, but we all know the cool stuff is not ANSI'ed.
2. To be able to write a repository and then inject to it an object that can translate my generic statements into fully compliant SQL statements for the RDBMS of choice.
3. To be compliant with IoC.

## Quickstart

This project defines a set of classes that represent general SQL parts, like tables, views, functions, fields, etc.  Those individual parts are then used to create statements, like SELECT, INSERT and UPDATE.  The project also provides a rich set of extension methods for said parts that allow the developer to build the puzzle of objects using fluent syntax that resembles actual SQL.

Once a query has been defined using the above classes and extensions, you may select an RDBMS and a corresponding **SQL Builder**.  It is this builder object that takes your puzzle of queries and output actual SQL in the form of a string.

### Step 1:  Define Queries

Import the `wj.AgnosticSql` namespace for the extension methods, and the `wj.AgnosticSql.Sql` namespace for the SQL objects.  Yes, most likely I will remove the latter namespace to simplify usage.

Now write a class that defines the SQL objects you will need like tables, views and fields, and then write the queries.

The following code shows a simple yet complete(ish) way to create a repository for a **User** class.

```c#
//Base class for repositories.  Helps us simplify the boilerplate.
public class RepositoryBase<TEntity, TQueries>
    where TQueries : new()
{
    protected ISqlBuilder SqlBuilder { get; }
    protected TQueries Queries { get; }
    public RepositoryBase(ISqlBuilder sqlBuilder)
    {
        SqlBuilder = sqlBuilder;
        Queries = new TQueries();
    }
}

//A repository for the User entity.
public class UserRepository : RepositoryBase<User, UserRepository.Queries>, IUserRepository
{
    public UserRepository(ISqlBuilder sqlBuilder)
        : base(sqlBuilder)
    { }

    #region Queries
    internal class Queries
    {
        //Tables.
        private Table UserTable = new Table("tblUser", alias: "u");
        private Table UserHistoryTable = new Table("tblUserHistory", alias: "uh");

        //You may declare variables of type Field to have key fields handy.
        private Field UserHistoryUserId;
        //You can also (and should) specify collections of fields.
        private FieldCollection UserTableFields = new FieldCollection()
        {
            UserTable.Field("Id"),
            UserTable.Field("FirstName"),
            UserTable.Field("LastName"),
            UserTable.Field("Username"),
            UserTable.Field("Email"),
            UserTable.Field("PasswordHash")
        };
        private FieldCollection UserHistoryTableFields = new FieldCollection()
        {
            UserHistoryTable.Field("Id"),
            UserHistoryTable.Field("UserId", fieldCfgFn: f => UserHistoryUserId = f),
            UserHistoryTable.Field("LoggedInAt"),
            UserHistoryTable.Field("LoggedOutAt"),
            UserHistoryTable.Field("RemoteIp")
        };

        //Now let's go for some actual queries.
        public SimpleInsertAndReturnQuery InsertUser = new SimpleInsertAndReturnQuery()
            .Insert(UserTableFields)
            .Into(UserTable)
            .Values("FirstName", "LastName", "Username", "Email", "PasswordHash")
            .Select(UserTableFields)
            ;

        public SelectQuery GetLoggedInUsers = new SelectQuery()
            .Select(UserTableFields)
            .From
            (
                UserHistoryTable
                .InnerJoin(UserTable, (l, r) => UserHistoryUserId == r.Field("Id"))
            )
            .Where
            (h =>
            {
                (UserHistoryTable.Field("LoggedInAt") >= h.Constant(DateTime.UtcNow.AddMonths(-6))
                .And
                (
                    h.Condition(h => UserHistoryTableFields["LoggedOutAt"], BooleanOperators.IsNull)
                )
            })
            .OrderBy
            (
                UserHistoryTableFields["LoggedInAt"].Descending()
                .ThenBy(UserHistoryTableFields["RemoteIp"].Ascending())
            )
            ;
    }
    #endregion
}
```

The code sample above shows key features:

1. Shows how to inject a SQL builder object to the repository.  Eventually, this is the object that receives the different queries and generates actual SQL for the RDBMS of choice.
2. Shows how writing queries resemble actual SQL to a certain degree.
3. Shows various ways to reference fields in tables, namely:

    - You can have a `Field` variable and use it in SELECT or building a WHERE condition.
    - You can reference an existing `Field` object that is contained in a `FieldCollection` object by using the field's name.
    - You can simply create a new `Field` object from a `Table` object.

4. Shows how C# Boolean operators can be used to create conditions for WHERE and JOIN clauses.
5. Some extension methods provide an expression builder helper object that can be used to create conditions, constants and parameters.

**NOTE:**  There are more features not shown here.  For example, you can set aliases for fields.  This is important for Dapper so Dapper can bind models properly to values coming from the RDBMS.  You can also select expressions like counts or anything else that is an expression (an expression is something that produces a scalar value).

Field and expression aliases become handy for RDBMS's like PostgreSQL since this guy lets you use aliases in ORDER BY and WHERE clauses.  Just saying, although with this construction method that kind of thing is not very important to the consumer developer because that is the SQL builder's business.

Anyway, back to topic.  Most likely you don't need to access individual tables or fields from the repository, so ideally those should be privately declared in the `Queries` class.

If there won't be runtime modifications of the query objects in the `Queries` class, the entire thing can be `static`, which should save you some RAM.

### Step 2:  Use the Queries

This part is super simple.  In your repository you will most likely have a 1:1 relationship between methods and queries, so the **User** repository in the example most likely would have two methods:

```c#
public async Task<User> CreateUserAsync(User newUser)
{
    string sql = SqlBuilder.BuildSqlStatement(Queries.InsertUser);
    using (IDbConnection conn = GetConnectionSomehow())
    {
        //Oh Dapper, how much I love you!
        User result = await conn.QuerySingleAsync<User>(sql, newUser);
        return result;
    }
}

public async Task<IEnumerable<User>> GetLoggedInUsersAsync()
{
    string sql = SqlBuilder.BuildSqlStatement(Queries.GetLoggedInUsers);
    using (IDbConnection conn = GetConnectionSomehow())
    {
        //Dapper, have I told you lately how much I love you?
        var result = await conn.QueryAsync<User>(sql, newUser);
        return result;
    }
}
```

Hopefully this is simple enough.

## If you can, Contribute

I am doing my best to provide a complete project as well as at least two other projects for SQL builders:  Microsoft SQL Server and PostgreSQL.  If you can and are willing to help, email me ([webJose@gmail.com]).  Ideally we should have SQL builders for SQL Server, MySQL, PostgreSQL, Oracle and one more.

**THANK YOU!!!**
