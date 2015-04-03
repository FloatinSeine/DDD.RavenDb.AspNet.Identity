using System;
using System.Linq.Expressions;

namespace Sample.Domain.CQRS.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(
        ISpecification<T> left,
        ISpecification<T> right)
            : base(left, right)
        {
        }

        public override Expression<Func<T, bool>> ToExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(Left.ToExpression, objParam),
                        Expression.Invoke(Right.ToExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
