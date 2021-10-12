using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LTE_ASP_Base.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRedisStorage _redisStorage;

        public HomeController(ILogger<HomeController> logger, IDistributedCache distributedCache,
            IRedisStorage redisStorage)
        {
            _logger = logger;
            _redisStorage = redisStorage;
        }

        [HttpGet("redis")]
        public async Task<KeyValuePair<string, string>[]> Get()
        {
            _redisStorage.StringSet("TestLocal", "Tran Nguyen Khanh Chi");
            _redisStorage.HashSet("Hash Key", "A", "1");
            _redisStorage.HashSet("Hash Key", "B", "2");
            _redisStorage.HashSet("Hash Key", "C", "3");
            _redisStorage.HashSet("Hash Key", "D", "4");
            _redisStorage.HashSet("Hash Key", "D", "5");
            await _redisStorage.StringGet<string>("TestLocal");
            return await _redisStorage.HashGetAll<string>("Hash Key");
        }
        
        [HttpGet("test")]
        public string test()
        {
            return "this is home test!";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}