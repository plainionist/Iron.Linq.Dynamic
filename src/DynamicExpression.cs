//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.Linq.Dynamic
{
    public static class DynamicExpression
    {
        public static Expression Parse( Type resultType, string expression, params object[] values )
        {
            var resolver = new MapIdentifierResolver();
            resolver.AddPositionalArguments( values );

            ExpressionParser parser = new ExpressionParser( expression, resolver );
            return parser.Parse( resultType );
        }

        public static LambdaExpression ParseLambda( Type itType, Type resultType, string expression, params object[] values )
        {
            var resolver = new MapIdentifierResolver();
            resolver.AddPositionalArguments( values );

            return ParseLambda( itType, resultType, expression, resolver );
        }
        
        public static Expression<Func<TResult>> ParseLambda<TResult>( string expression, params object[] values )
        {
            return (Expression<Func<TResult>>)ParseLambda( typeof( TResult ), expression, values );
        }

        public static LambdaExpression ParseLambda( Type resultType, string expression, params object[] values )
        {
            var resolver = new MapIdentifierResolver();
            resolver.AddPositionalArguments( values );

            var parser = new ExpressionParser( expression, resolver );
            return Expression.Lambda( parser.Parse( resultType ) );
        }

        public static Expression<Func<TResult>> ParseLambda<TResult>( string expression, IIdentifierResolver resolver )
        {
            return (Expression<Func<TResult>>)ParseLambda( typeof( TResult ), expression, resolver );
        }

        public static LambdaExpression ParseLambda( Type resultType, string expression, IIdentifierResolver resolver )
        {
            var parser = new ExpressionParser( expression, resolver );
            return Expression.Lambda( parser.Parse( resultType ) );
        }

        public static Expression<Func<TIterator, TResult>> ParseLambda<TIterator, TResult>( string expression, params object[] values )
        {
            return (Expression<Func<TIterator, TResult>>)ParseLambda( typeof( TIterator ), typeof( TResult ), expression, values );
        }

        public static Expression<Func<TIterator, TResult>> ParseLambda<TIterator, TResult>( string expression, IIdentifierResolver resolver )
        {
            return (Expression<Func<TIterator, TResult>>)ParseLambda( typeof( TIterator ), typeof( TResult ), expression, resolver );
        }


        public static LambdaExpression ParseLambda( Type itType, Type resultType, string expression, IIdentifierResolver resolver )
        {
            var parser = new ExpressionParser( expression, resolver, itType );
            return Expression.Lambda( parser.Parse( resultType ), parser.IteratorParameter );
        }

        public static Type CreateClass( params DynamicProperty[] properties )
        {
            return ClassFactory.Instance.GetDynamicClass( properties );
        }

        public static Type CreateClass( IEnumerable<DynamicProperty> properties )
        {
            return ClassFactory.Instance.GetDynamicClass( properties );
        }
    }
}
