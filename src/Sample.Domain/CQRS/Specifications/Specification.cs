using System;
using System.Linq.Expressions;

namespace Sample.Domain.CQRS.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {

        private Func<T, bool> _compiledExpression;

        private Func<T, bool> CompiledExpression
        {
            get { return _compiledExpression ?? (_compiledExpression = ToExpression.Compile()); }
        }

        public abstract Expression<Func<T, bool>> ToExpression { get; }

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}
