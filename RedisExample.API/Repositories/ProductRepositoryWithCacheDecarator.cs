
using RedisExample.API.Models;
using RedisExample.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisExample.API.Repositories
{
    public class ProductRepositoryWithCacheDecarator : IProductRepository
    {
        private const string productKey = "productCaches";//redis server table name
        private readonly IProductRepository _ProductRepository;//our repo
        private readonly RedisService _redisService;//Redis repo
        private readonly IDatabase _cacheRepository;//for choose database in redis server.

        public ProductRepositoryWithCacheDecarator(IProductRepository productRepository, RedisService redisService)
        {
            _ProductRepository = productRepository;
            _redisService = redisService;
            _cacheRepository = _redisService.GetDatabase(2);
        }

        public async Task<Products> CreateAsync(Products products)
        {
            var newProduct = await _ProductRepository.CreateAsync(products);

            if (await _cacheRepository.KeyExistsAsync(productKey))
            {
                await _cacheRepository.HashSetAsync(productKey, products.Id, JsonSerializer.Serialize(newProduct));
            }
            return newProduct;
        }

        public async Task<List<Products>> GetAsync()
        {
            if (!await _cacheRepository.KeyExistsAsync(productKey))//check the redis if data not found in cache load with "LoadCacheFromDbAsync"
            {
                return await LoadCacheFromDbAsync();
            }

            var products = new List<Products>();

            var cacheProducts = await _cacheRepository.HashGetAllAsync(productKey);

            foreach (var item in cacheProducts.ToList()) //if item already in cache , deserialize it and add list.
            {

                var product = JsonSerializer.Deserialize<Products>(item.Value);

                products.Add(product);
            }

            return products;
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            if (_cacheRepository.KeyExists(productKey))
            {
                var product = await _cacheRepository.HashGetAsync(productKey, id);
                return product.HasValue ? JsonSerializer.Deserialize<Products>(product) : null;
            }

            var products = await LoadCacheFromDbAsync();
            return products.FirstOrDefault(x => x.Id == id);
        }

        private async Task<List<Products>> LoadCacheFromDbAsync()
        {
            var products = await _ProductRepository.GetAsync();//this method implement products model.

            products.ForEach(p =>//this method add list our data and serialize it. 
            {
                _cacheRepository.HashSetAsync(productKey, p.Id, JsonSerializer.Serialize(p));
            });

            return products;

        }
    }
}
