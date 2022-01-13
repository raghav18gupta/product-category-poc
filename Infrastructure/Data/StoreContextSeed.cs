using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!_context.Category.Any())
                {
                    var brandTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var brandsTypes = JsonSerializer.Deserialize<List<Category>>(brandTypeData);

                    foreach (var item in brandsTypes)
                    {
                        _context.Category.Add(item);
                    }
                    await _context.SaveChangesAsync();

                }
                if (!_context.Products.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var item in products)
                    {
                        _context.Products.Add(item);
                    }
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
