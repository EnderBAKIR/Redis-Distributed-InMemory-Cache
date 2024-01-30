using RedisExample.API.Models;
using RedisExample.API.Repositories;

namespace RedisExample.API.Services
{
    public class ProductService : IProductService
    {


        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Products> CreateAsync(Products products)
        {
         return  await _productRepository.CreateAsync(products);
        }

        public async Task<List<Products>> GetAsync()
        {
            return await _productRepository.GetAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}
