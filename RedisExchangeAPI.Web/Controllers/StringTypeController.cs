using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0); //=>=> //another usage of Redis db = In Controller you must use this method. var db = _redisService.GetDb(which you want db.number);
        }

        public IActionResult Index()
        {
           

            db.StringSet("name", "Ender Bakır");
            db.StringSet("Ziyaretci", 100);

            return View();
        }
        public IActionResult Show() 
        {
            var value = db.StringGet("name");
            db.StringIncrement("Ziyaretci", 1);
            if(value.HasValue)
            {
                ViewBag.value = value.ToString();
            }

            return View();


        }
    }
}
