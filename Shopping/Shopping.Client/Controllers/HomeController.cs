﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shopping.Client.Data;
using Shopping.Client.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory httpClient, ILogger<HomeController> logger)
        {
            _httpClient = httpClient.CreateClient("ShoppingAPIClient");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/product");
            var content = await response.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);

            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
