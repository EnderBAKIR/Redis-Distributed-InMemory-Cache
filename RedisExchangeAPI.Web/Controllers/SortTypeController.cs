using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortTypeController : Controller
    {
        private readonly RedisService _redisService;

        private readonly IDatabase db;

        private string ListKey = "sortedsetnames";

        public SortTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }


        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();

            if (db.KeyExists(ListKey))
            {
                db.SortedSetScan(ListKey).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }

            //db.SortedSetRangeByRank(ListKey, order: Order.Descending).ToList().ForEach(x =>
            //{
            //    list.Add(x.ToString());
            //});

            return View(list);
        }

        public IActionResult Add(string name , int score)
        {
           

            db.SortedSetAdd(ListKey , name, score);
             db.KeyExpire(ListKey, DateTime.Now.AddMinutes(5));

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(string name)
        {
           db.SortedSetRemove(ListKey , name);

            return RedirectToAction(nameof(Index));
        }
    }
}
