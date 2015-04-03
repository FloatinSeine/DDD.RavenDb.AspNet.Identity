

namespace Sample.Domain.CQRS.Specifications
{
    public static class ISpecificationExtensions
    {
        public static ISpecification<T> And<T>(
        this ISpecification<T> left,
        ISpecification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        public static ISpecification<T> Or<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        public static ISpecification<T> Negate<T>(this ISpecification<T> inner)
        {
            return new NegatedSpecification<T>(inner);
        }
    }
}
