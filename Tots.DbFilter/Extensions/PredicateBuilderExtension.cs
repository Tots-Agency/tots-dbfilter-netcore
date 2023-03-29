﻿using System;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.Json;
using System.ComponentModel;
using System.Reflection.Metadata;

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

        public static Expression<Func<T, bool>> Equal<T>(string key, dynamic? value)
        {
            // Verificar si el primer key es un List y en un futuro que pueda ser recursivo
            if (IsRelationList<T>(key))
            {
                var split = key.Split(".");
                ParameterExpression paramC = Expression.Parameter(typeof(T), "c");
                MemberExpression memberOrders = Expression.Property(paramC, split[0]);
                Type typeOrder = GetTypeReal<T>(key);
                ParameterExpression paramO = Expression.Parameter(typeOrder, "o");
                MemberExpression memberOrderType = Expression.Property(paramO, split[1]);

                var propFinal = getProperty<T>(key);

                var converter = TypeDescriptor.GetConverter(propFinal.PropertyType);
                ConstantExpression constantOrderType = Expression.Constant(converter.ConvertFrom(value), propFinal.PropertyType);
                BinaryExpression equal = Expression.Equal(memberOrderType, constantOrderType);
                LambdaExpression lambdaO = Expression.Lambda(equal, paramO);
                MethodCallExpression any = Expression.Call(typeof(Enumerable), "Any", new Type[] { typeOrder }, memberOrders, lambdaO);
                return Expression.Lambda<Func<T, bool>>(any, paramC);
            }else if (IsRelation<T>(key))
            {
                var split = key.Split(".");
                var parameterI = Expression.Parameter(typeof(T));
                var parameterRel = Expression.Parameter(typeof(T), "p");
                MemberExpression memberOrders = Expression.Property(parameterRel, split[0]);
                
                //Type typeRel = GetType<T>(key);
                MemberExpression memberOrderType = Expression.Property(memberOrders, split[1]);

                var propertyRel = getProperty<T>(key);
                var typeRel = Nullable.GetUnderlyingType(propertyRel.PropertyType) ?? propertyRel.PropertyType;

                var convertedValue = Convert.ChangeType(value, typeRel);
                var valueExpRel = Expression.Constant(convertedValue, typeRel);
                var exprRel = Expression.Equal(memberOrderType, valueExpRel);
                LambdaExpression lambdaO = Expression.Lambda(exprRel, parameterRel);
                return (Expression<Func<T, bool>>)lambdaO;
            }


            var prop = getProperty<T>(key);
            var parameter = Expression.Parameter(typeof(T));
            var propertyParameter = getParameterExperession(parameter, key);
            var property = Expression.Property(propertyParameter, prop);
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            var valueExp = Expression.Constant(Convert.ChangeType(value, type));
            var expr = Expression.Equal(property, valueExp);
            var resultexp = Expression.Lambda<Func<T, bool>>(expr, parameter);
            return resultexp;
        }

        public static Expression<Func<T, bool>> Like<T>(string key, dynamic value)
        {
            var prop = getProperty<T>(key);
            var parameter = Expression.Parameter(typeof(T));
            var propertyParameter = getParameterExperession(parameter, key);
            var property = Expression.Property(propertyParameter, prop);
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            var valueExp = Expression.Constant(value);
            var expr = Expression.Equal(Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExp), Expression.Constant(true));
            var resultexp = Expression.Lambda<Func<T, bool>>(expr, parameter);
            return resultexp;
        }

        public static Expression<Func<T, bool>> Between<T>(string key, dynamic minValue, dynamic maxValue)
        {
            var prop = getProperty<T>(key);
            if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime))
            {
                return BetweenDates<T>(key, DateTime.Parse(minValue.ToString()), DateTime.Parse(maxValue.ToString()));
            }
            
            return BetweenNumbers<T>(key, minValue, maxValue);
        }

        public static Expression<Func<T, bool>> BetweenDates<T>(string key, DateTime? startDate, DateTime? endDate)
        {
            var prop = getProperty<T>(key);
            var parameter = Expression.Parameter(typeof(T));
            var propertyParameter = getParameterExperession(parameter, key);
            var property = Expression.Property(propertyParameter, prop);

            ConstantExpression startDateConstant;
            ConstantExpression endDateConstant;
            if (prop.PropertyType == typeof(DateTime?))
            {
                startDateConstant = Expression.Constant(startDate, typeof(DateTime?));
                endDateConstant = Expression.Constant(endDate, typeof(DateTime?));
            } else
            {
                startDateConstant = Expression.Constant(startDate);
                endDateConstant = Expression.Constant(endDate);
            }

            var greaterThanStartDate = Expression.GreaterThanOrEqual(property, startDateConstant);
            var lessThanEndDate = Expression.LessThanOrEqual(property, endDateConstant);

            var expr = Expression.AndAlso(greaterThanStartDate, lessThanEndDate);

            var resultexp = Expression.Lambda<Func<T, bool>>(expr, parameter);
            return resultexp;
        }

        public static Expression<Func<T, bool>> BetweenNumbers<T>(string propertyName, int minValue, int maxValue)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var minValueConstant = Expression.Constant(minValue);
            var maxValueConstant = Expression.Constant(maxValue);

            var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, minValueConstant);
            var lessThanOrEqual = Expression.LessThanOrEqual(property, maxValueConstant);

            var binaryExpression = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);

            var lambdaExpression = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
            return lambdaExpression;
        }


        public static Expression<Func<T, bool>> Likes<T>(List<string> keys, dynamic value)
        {
            Expression<Func<T, bool>> predicate = PredicateBuilderExtension.True<T>();
            var parameter = Expression.Parameter(typeof(T));

            bool isFirst = true;
            foreach (string key in keys)
            {
                var prop = getProperty<T>(key);
                var propertyParameter = getParameterExperession(parameter, key);
                var property = Expression.Property(propertyParameter, prop);
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var valueExp = Expression.Constant(value);

                BinaryExpression expr = Expression.Equal(Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExp), Expression.Constant(true));

                if (isFirst)
                {
                    predicate = Expression.Lambda<Func<T, bool>>(expr, parameter);
                    isFirst = false;
                }
                else
                {
                    predicate = Expression.Lambda<Func<T, bool>>(Expression.OrElse(predicate.Body, expr), parameter);
                }

            }

            return predicate;
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

                if (result.PropertyType.IsGenericType && result.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    type = result.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    type = result.PropertyType;
                }
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

        public static Expression<Func<T, object>> GetGroupByExpression<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<T, object>>(property, parameter);
            return lambda;
        }

        public static Expression<Func<T, object>> GroupByExpressionList<T>(string[] propertyNames)
        {
            var properties = propertyNames.Select(name => typeof(T).GetProperty(name.FirstCharToUpper())).ToArray();
            var propertyTypes = properties.Select(p => p.PropertyType).ToArray();
            var tupleTypeDefinition = typeof(Tuple).Assembly.GetType("System.Tuple`" + properties.Length);
            var tupleType = tupleTypeDefinition.MakeGenericType(propertyTypes);
            var constructor = tupleType.GetConstructor(propertyTypes);
            var param = Expression.Parameter(typeof(T), "item");
            var body = Expression.New(constructor, properties.Select(p => Expression.Property(param, p)));
            var expr = Expression.Lambda<Func<T, object>>(body, param);
            return expr;
        }

        public static IQueryable<T> AddOrderExpression<T>(IQueryable<T> query, String field, String typeOrd, bool isFirst)
        {
            String command = "OrderBy";
            if (isFirst && typeOrd == "asc")
            {
                command = "OrderBy";
            }else if(isFirst && typeOrd == "desc")
            {
                command = "OrderByDescending";
            }
            else if (typeOrd == "asc")
            {
                command = "ThenBy";
            }
            else if (typeOrd == "desc")
            {
                command = "ThenByDescending";
            }

            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var prop = getProperty<T>(field);
            var propertyAccess = Expression.MakeMemberAccess(parameter, prop);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, prop.PropertyType }, query.Expression, Expression.Quote(orderByExpression));
            return query.Provider.CreateQuery<T>(resultExpression);
        }

        public static Type GetType<T>(string name)
        {
            var split = name.Split(".");
            var type = typeof(T);

            PropertyInfo result = null;
            foreach (var item in split)
            {
                result = type.GetProperty(item);

                if (result.PropertyType.IsGenericType && result.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    type = result.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    type = result.PropertyType;
                }

            }
            return type;
        }

        public static Type GetTypeReal<T>(string name)
        {
            var split = name.Split(".");
            split = split.Take(split.Count() - 1).ToArray();
            var type = typeof(T);

            PropertyInfo result = null;
            foreach (var item in split)
            {
                result = type.GetProperty(item);

                if (result.PropertyType.IsGenericType && result.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    type = result.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    type = result.PropertyType;
                }

            }
            return type;
        }

        public static bool IsRelationList<T>(string name)
        {
            var split = name.Split(".");
            var type = typeof(T);

            PropertyInfo result = null;
            foreach (var item in split)
            {
                result = type.GetProperty(item);

                if (result.PropertyType.IsGenericType && result.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    return true;
                }
                else
                {
                    type = result.PropertyType;
                }

            }
            return false;
        }
        public static bool IsRelation<T>(string name)
        {
            var split = name.Split(".");
            if (split.Count() > 1)
            {
                return true;
            }

            return false;
        }
    }
}

