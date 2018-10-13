using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTreeMapper
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var source = Expression.Parameter(typeof(TSource), "source");
            var destination = Expression.Parameter(typeof(TDestination), "destination");

            var mapExpressionBody = BuildMapExpressionBody<TSource, TDestination>(source, destination);
            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(mapExpressionBody, source);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        private Expression BuildMapExpressionBody<TSource, TDestination>(ParameterExpression source, ParameterExpression destination)
        {
            var statements = new List<Expression>();
            statements.Add(Expression.Assign(destination, Expression.New(typeof(TDestination))));
            statements.AddRange(GetPropertyMappingExpresions<TSource>(source, destination));
            statements.Add(destination);

            return Expression.Block(new[] { destination }, statements);
        }

        private IEnumerable<Expression> GetPropertyMappingExpresions<TSource>(ParameterExpression source, ParameterExpression destination)
        {
            var mapExpressions = new List<Expression>();

            var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
            foreach (var propertyInfo in propertyInfos)
            {
                var sourceProperty = Expression.Property(source, propertyInfo.Name);
                var targetProperty = Expression.Property(destination, propertyInfo.Name);

                Expression value = sourceProperty;
                if (value.Type != targetProperty.Type)
                {
                    value = Expression.Convert(value, targetProperty.Type);
                }

                Expression statement = Expression.Assign(targetProperty, value);

                mapExpressions.Add(statement);
            }

            return mapExpressions;
        }
    }
}
