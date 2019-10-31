namespace RealEstate.Infrastructure
{
    using System;
    using System.Reflection;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtension
    {
        #region Helper
        public const string ASCENDING = "Ascending";

        public const string DESCENDING = "Descending";

        private static IOrderedQueryable<T> Ascending<T, U>(IQueryable<T> list, Expression<Func<T, U>> expression)
            => list.OrderBy(expression);

        private static IOrderedQueryable<T> Descending<T, U>(IQueryable<T> list, Expression<Func<T, U>> expression)
            => list.OrderByDescending(expression);

        private static PropertyInfo FindPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo property = null;
            if (propertyName.Contains("."))
            {
                string[] nameParts = propertyName.Split('.');
                property = type.GetProperty(nameParts[0]);

                if (property != null)
                {
                    propertyName = propertyName.Substring(propertyName.IndexOf('.') + 1);
                    property = FindPropertyInfo(property.PropertyType, propertyName);
                }
            }
            else
            {
                property = type.GetProperty(propertyName);
            }
            return property;
        }
        #endregion

        public static IOrderedQueryable<T> OrderByPropertyName<T>(this IQueryable<T> list,
            string propertyName, string orderBy)
        {
            PropertyInfo property = FindPropertyInfo(typeof(T), propertyName) ??
                                    typeof(T).GetProperties()[0];

            ParameterExpression args = Expression.Parameter(typeof(T), "x");
            Expression argsProperty = args;
            if (propertyName.Contains("."))
                propertyName.Split('.').ForEach(x
                    => argsProperty = Expression.PropertyOrField(argsProperty, x));
            else
                argsProperty = Expression.MakeMemberAccess(args, property);

            Type funcType = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
            LambdaExpression expression = Expression.Lambda(
                delegateType: funcType, body: argsProperty, parameters: args);

            MethodInfo method = typeof(QueryableExtension).GetMethod(
                orderBy == "asc" ? ASCENDING : DESCENDING,
                BindingFlags.Static | BindingFlags.NonPublic);

            return (IOrderedQueryable<T>)method.MakeGenericMethod(typeof(T), argsProperty.Type)
                    .Invoke(null, new object[] { list, expression });
        }
    }
}
