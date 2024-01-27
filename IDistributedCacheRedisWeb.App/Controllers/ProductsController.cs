using IDistributedCacheRedisWeb.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

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
            Byte[] byteproduct = Encoding.UTF8.GetBytes(jsonproduct);

            _distributedCache.Set("product:1", byteproduct);

           //await _distributedCache.SetStringAsync("products:1", jsonproduct , cacheOptions);

            return View();
        }

        public IActionResult show()
        {

            //string name = _distributedCache.GetString("name");
            //ViewBag.Name = name;

            Byte[] byteProduct = _distributedCache.Get("product:1");
            string jsonproduct = Encoding.UTF8.GetString(byteProduct);
            
            Products p = JsonConvert.DeserializeObject<Products>(jsonproduct);
            ViewBag.product = p;
            return View();


        }

        public IActionResult Remove()
        {
            //_distributedCache.Remove("name");

            return View();
        }

        public IActionResult ImageCache()
        {

            String path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/download.jpg");

            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("resim", imageByte);

            return View();
        }

        public IActionResult ImageUrl()
        {
            byte[] resimbyte = _distributedCache.Get("resim");
            return File(resimbyte, "image/jpg");
            
            
        }
    }
}
