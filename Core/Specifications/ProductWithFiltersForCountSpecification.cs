using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParameters productParameters)
            : base(prop =>
                    (string.IsNullOrEmpty(productParameters.Search) || prop.Name.ToLower().Contains(productParameters.Search)) &&
                    (!productParameters.CategoryId.HasValue || prop.CategoryId == productParameters.CategoryId))
        {

        }
    }
}
