using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class ProductsGenericController : BaseApiController
    {
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public ProductsGenericController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Category> CategoryRepository,
            IMapper mapper
        )
        {
            _productRepository = productRepository;
            _categoryRepository = CategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecificationParameters productParameters)
        {
            var specification = new ProductsWithCategorySpecification(productParameters);

            var product = await _productRepository.ListAsync(specification);

            var countSpec = new ProductsWithCategorySpecification(productParameters);
            var totalItems = await _productRepository.CountAsync(specification);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product);

            return Ok(new Pagination<ProductToReturnDto>(productParameters.PageIndex, productParameters.PageSize, totalItems, data));
        }

        [HttpGet("{i}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int i)
        {
            var specification = new ProductsWithCategorySpecification(i);
            var product = await _productRepository.GetEntityWithSpecification(specification);

            if (product == null)
                return NotFound();

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }


        [HttpGet("Category")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategory()
        {
            return Ok(await _categoryRepository.ListAllAsync());
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody]Product product)
        {
            if(product != null)
            {
                _productRepository.AddProduct(product);
                return Json(product);
            }
            return NotFound();
        }

        [HttpGet("Delete/{i}")]
        public ActionResult Delete(int deleteId)
        {
            if (_productRepository.DeleteProduct(deleteId))
                return Ok();
            return NotFound();
        }

        [HttpPost("Update")]
        public ActionResult Update([FromBody] Product product)
        {
            if (_productRepository.UpdateProduct(product))
                return Ok();
            return NotFound();
        }
    }
}
