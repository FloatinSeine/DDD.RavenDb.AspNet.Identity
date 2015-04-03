using System;
using System.Linq.Expressions;


namespace Sample.Domain.CQRS.Specifications
{
    public class NegatedSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _inner;

        public NegatedSpecification(ISpecification<T> inner)
        {
            _inner = inner;
        }

        public ISpecification<T> Inner
        {
            get { return _inner; }
        }

        //public bool IsSatisfiedBy(T obj)
        //{
        //    return !_inner.IsSatisfiedBy(obj);
        //}

        public override Expression<Func<T, bool>> ToExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof (T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(_inner.ToExpression, objParam)
                        ),
                    objParam
                    );

                return newExpr;
            }
        }
    }
}
