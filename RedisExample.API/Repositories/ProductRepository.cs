using Microsoft.EntityFrameworkCore;
using RedisExample.API.Models;

namespace RedisExample.API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Products> CreateAsync(Products products)
        {
          await  _context.Productss.AddAsync(products);
            await _context.SaveChangesAsync(); 
            return products;
        }

        public async Task<List<Products>> GetAsync()
        {
            return await _context.Productss.ToListAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _context.Productss.FindAsync(id);
        }
    }
}
