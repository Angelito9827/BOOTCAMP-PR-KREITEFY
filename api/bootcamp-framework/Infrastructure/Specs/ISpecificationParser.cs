namespace bootcamp_framework.Infraestructure.Specs
{
    public interface ISpecificationParser<T>
    {
        Specification<T> ParseSpecification(string filter);
    }
}