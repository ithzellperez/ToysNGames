using System.Threading.Tasks;
using ToysNGames.Data.Entities;

namespace ToysNGames.Data
{
    public interface IProductRepository
    {
        Task<bool> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Product product);
        Task<Product[]> GetAllAsync();
        Task<Product> GetAsync(int id);
    }
}