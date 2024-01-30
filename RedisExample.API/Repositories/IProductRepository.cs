using RedisExample.API.Models;

namespace RedisExample.API.Repositories
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAsync();
        Task<Products> GetByIdAsync(int id);

        Task<Products> CreateAsync(Products products);

    }
}
