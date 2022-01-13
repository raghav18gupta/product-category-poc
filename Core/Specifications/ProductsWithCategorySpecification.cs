using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithCategorySpecification : BaseSpecification<Product>
    {
        public ProductsWithCategorySpecification(ProductSpecificationParameters productParameters)
            : base(prop => 
                    (string.IsNullOrEmpty(productParameters.Search) || prop.Name.ToLower().Contains(productParameters.Search)) &&
                    (!productParameters.CategoryId.HasValue || prop.CategoryId == productParameters.CategoryId))
        {
            AddInclude(prop => prop.Category);
            AddOrderBy(prop => prop.Name);
            ApplyPaging(productParameters.PageSize * (productParameters.PageIndex - 1),
                        productParameters.PageSize);

            if (!string.IsNullOrEmpty(productParameters.Sort))
            {
                switch (productParameters.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(prop => prop.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(prop => prop.Price);
                        break;
                    default:
                        AddOrderBy(prop => prop.Name);
                        break;
                }
            }
        }
        public ProductsWithCategorySpecification(int id) : base(prop => prop.Id == id)
        {
            AddInclude(prop => prop.Category);
        }
    }
}
