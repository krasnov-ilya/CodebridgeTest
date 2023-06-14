using System.Linq.Expressions;

namespace CodebridgeTest.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
        bool descending)
    {
        var command = descending ? "OrderByDescending" : "OrderBy";
        var type = typeof(TEntity);
        var property = type.GetProperty(orderByProperty);
        var parameter = Expression.Parameter(type, orderByProperty);
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command, new [] { type, property.PropertyType },
            source.Expression, Expression.Quote(orderByExpression));
        return source.Provider.CreateQuery<TEntity>(resultExpression);
    }
}