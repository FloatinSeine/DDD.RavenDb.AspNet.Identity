
namespace Sample.Domain.CQRS.Specifications
{
    public abstract class CompositeSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _leftExpr;
        private readonly ISpecification<T> _rightExpr;

        protected CompositeSpecification(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            _leftExpr = left;
            _rightExpr = right;
        }

        public ISpecification<T> Left
        {
            get { return _leftExpr; }
        }

        public ISpecification<T> Right
        {
            get { return _rightExpr; }
        }

    }
}
