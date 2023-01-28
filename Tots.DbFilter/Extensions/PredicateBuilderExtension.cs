using System;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.Json;
using System.ComponentModel;

namespace Tots.DbFilter.Extensions
{
	public static class PredicateBuilderExtension
	{
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> Equal<T>(string key, dynamic value)
        {
            Expression<Func<T, bool>> predicate = PredicateBuilderExtension.True<T>();
            var predString = predicate.ToString();
            var prop = getProperty<T>(key);
            var parameter = Expression.Parameter(typeof(T));
            var propertyParameter = getParameterExperession(parameter, key);
            var property = Expression.Property(propertyParameter, prop);
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            var valueExp = Expression.Constant(value);
            var expr = Expression.Equal(property, valueExp);
            var resultexp = Expression.Lambda<Func<T, bool>>(expr, parameter);
            return resultexp;
            //return predicate.And(resultexp);
        }

        public static Expression<Func<T, bool>> Contains<T>(string key, dynamic value)
        {
            Expression<Func<T, bool>> predicate = PredicateBuilderExtension.True<T>();
            var predString = predicate.ToString();
            var prop = getProperty<T>(key);
            var parameter = Expression.Parameter(typeof(T));
            var propertyParameter = getParameterExperession(parameter, key);
            var property = Expression.Property(propertyParameter, prop);
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            Type t = typeof(List<>).MakeGenericType(type);
            var list = (IList)Activator.CreateInstance(t);
            var valueDeserialize = JsonSerializer.Deserialize<dynamic[]>(value);
            foreach (var item in valueDeserialize)
            {
                var converter = TypeDescriptor.GetConverter(type);
                list.Add(converter.ConvertFrom(item.ToString()));
            }

            var valueExp = Expression.Constant(list);
            var expr = Expression.Equal(Expression.Call(valueExp, valueExp.Type.GetMethod("Contains"), property), Expression.Constant(true));
            var resultexp = Expression.Lambda<Func<T, bool>>(expr, parameter);
            return resultexp;
        }

        private static PropertyInfo getProperty<T>(string name)
        {
            var split = name.Split(".");
            var type = typeof(T);

            PropertyInfo result = null;
            foreach (var item in split)
            {
                result = type.GetProperty(item);
                type = result.PropertyType;
            }
            return result;
        }

        private static Expression getParameterExperession(ParameterExpression parameter, string parameterName)
        {
            var split = parameterName.Split(".");
            split = split.Take(split.Count() - 1).ToArray();
            Expression result = parameter;
            foreach (var name in split)
            {
                result = Expression.Property(result, name);
            }
            return result;
        }
    }
}

