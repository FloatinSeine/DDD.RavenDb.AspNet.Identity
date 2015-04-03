
using System;
using System.Linq.Expressions;

namespace Sample.Domain.CQRS.Specifications
{
    public interface ISpecification<T>
    {
        //ISpecification<T> And(ISpecification<T> specification);
        bool IsSatisfiedBy(T item);
        Expression<Func<T, bool>> ToExpression { get; }
        //bool IsGeneralizationOf(ISpecification<T> specification);
        //bool IsSpecialCaseOf(ISpecification<T> specification);
        //ISpecification<T> Not(ISpecification<T> specification);
        //ISpecification<T> Or(ISpecification<T> specification);
        //ISpecification<T> RemainderUnsatisfiedBy(T item);
    }
}
