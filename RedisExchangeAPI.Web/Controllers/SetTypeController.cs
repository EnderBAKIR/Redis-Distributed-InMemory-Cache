using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;

        private readonly IDatabase db;

        private string ListKey = "hashnames";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> namesList = new HashSet<string>();
            if (db.KeyExists(ListKey))
            {
                db.SetMembers(ListKey).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });
            }


            return View(namesList);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {

            /* if (!db.KeyExists(ListKey))*/ //if u want remove sliding expiration , you can use this way for absolute expiration

            db.KeyExpire(ListKey, DateTime.Now.AddMinutes(5));




            db.SetAdd(ListKey, name);

            return RedirectToAction(nameof(Index));
        }

        public async Task< IActionResult> Delete(string name) 
        {
            await db.SetRemoveAsync(ListKey, name);

            return RedirectToAction(nameof(Index));
        }
       
    }
}
