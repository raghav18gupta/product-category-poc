using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _storeContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }


        async Task<T> IGenericRepository<T>.GetEntityWithSpecification(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        async Task<IReadOnlyList<T>> IGenericRepository<T>.ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        async Task<int> IGenericRepository<T>.CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), specification);
        }

        public void AddProduct(Product product)
        {
            _storeContext.Products.Add(product);
            _storeContext.SaveChanges();
        }

        public bool DeleteProduct(int id)
        {
            if(_storeContext.Products.Any(p => p.Id == id))
            {
                Product product = _storeContext.Products.FirstOrDefault(p => p.Id == id);
                _storeContext.Products.Remove(product);
                _storeContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product product)
        {
            if (_storeContext.Products.Any(p => p.Id == product.Id))
            {
                _storeContext.Products.Update(product);
                _storeContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
