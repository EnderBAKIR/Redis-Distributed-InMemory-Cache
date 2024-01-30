using RedisExample.API.Models;

namespace RedisExample.API.Services
{
    public interface IProductService
    {
        Task<List<Products>> GetAsync();
        Task<Products> GetByIdAsync(int id);

        Task<Products> CreateAsync(Products products);
    }
}
