using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;

        private readonly IDatabase db;

        private string ListKey = "names";

        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }

        public IActionResult Index()
        {
            List<string> namesList = new List<string>();
            if (db.KeyExists(ListKey)) 
            {
                db.ListRange(ListKey).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });
                
            
            }
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name) 
        {
            db.ListRightPush(ListKey, name);



        return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete(string name) 
        {
            db.ListRemoveAsync(ListKey, name).Wait();
            return RedirectToAction(nameof(Index));
            
        
        }

        public IActionResult DeleteFirstItem()
        {

            db.ListLeftPop(ListKey);
            return RedirectToAction(nameof(Index));
        }
    }
}
