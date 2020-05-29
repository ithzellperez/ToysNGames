using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToysNGames.Data.Entities;

namespace ToysNGames.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(DBContext dbContext, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Add(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save changes: {e}");
                return false;
            }
            finally
            {
                _dbContext.DetachAllEntities();
            }
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                _dbContext.Products.Update(product);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save changes: {e}");
                return false;
            }
            finally
            {
                _dbContext.DetachAllEntities();
            }
        }

        public async Task<bool> Delete(Product product)
        {
            try
            {
                _dbContext.Products.Remove(product);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save changes: {e}");
                return false;
            }
            finally
            {
                _dbContext.DetachAllEntities();
            }
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _dbContext.Products.AsNoTracking().Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Product[]> GetAllAsync()
        {
            return await _dbContext.Products.AsNoTracking().OrderBy(p => p.Id).ToArrayAsync();
        }
    }
}
