using IDistributedCacheRedisWeb.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistributedCacheRedisWeb.App.Controllers
{
    public class ProductsController : Controller
    {

        private IDistributedCache _distributedCache;


        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task< IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            //_distributedCache.SetString("name", "Ender" , cacheOptions);
            //await _distributedCache.SetStringAsync("surname", "Bakır" , cacheOptions);

            Products products = new Products {Id=1 , Name="Araba" , Price=1000 };

            string jsonproduct =JsonConvert.SerializeObject(products);
           await _distributedCache.SetStringAsync("products:1", jsonproduct , cacheOptions);

            return View();
        }
        public IActionResult show()
        {

            //string name = _distributedCache.GetString("name");
            //ViewBag.Name = name;
           
            
            return View();


        }
        public IActionResult Remove()
        {
            //_distributedCache.Remove("name");

            return View();
        }
    }
}
