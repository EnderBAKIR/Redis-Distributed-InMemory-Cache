using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisExample.API.Models;
using RedisExample.API.Repositories;
using RedisExample.API.Services;
using RedisExample.Cache;

namespace RedisExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        //private readonly RedisService _redisService;=>another using way for choose db.
                                                                                    
        public ProductsController(IProductService productService /*RedisService redisService*/)
        {
            _productService = productService;


            //_redisService = redisService;
            //var db = _redisService.GetDatabase(0);=>another using way
            //db.StringSet("isim", "ahmet"); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Add(Products products)
        {
            return Created(string.Empty ,await _productService.CreateAsync(products));
        }

    }
}
