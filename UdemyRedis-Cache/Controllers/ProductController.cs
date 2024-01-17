using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using UdemyRedis_Cache.Models;

namespace UdemyRedis_Cache.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {






            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);//Memoryden silinme süresini ayarla//Set Delete Time From Memory
            options.Priority = CacheItemPriority.High;



            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"{key}->{value} => sebep:{reason}");
            });
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            Product product = new Product {Id = 1 , Name="Araba" , Price=1900 };
            _memoryCache.Set<Product>("product:1" , product );

            return View();
        }
        public IActionResult Show()
        {

            _memoryCache.TryGetValue("zaman", out string zamancache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = zamancache;
            ViewBag.callback = callback;
            ViewBag.product = _memoryCache.Get<Product>("product:1");
            return View();
        }
    }
}
