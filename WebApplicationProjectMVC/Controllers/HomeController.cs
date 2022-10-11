using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationProjectMVC.Helper;
using WebApplicationProjectMVC.Models;


namespace WebApplicationProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        ProductAPI _prodapi = new ProductAPI();

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> lo gger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            List<ProductData> products = new List<ProductData>();
            HttpClient client = _prodapi.Initial();
            HttpResponseMessage resp = await client.GetAsync("api/product");
            if (resp.IsSuccessStatusCode)
            {
                var result = resp.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<ProductData>>(result);
                    
            }
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = new ProductData();
            HttpClient client = _prodapi.Initial();
            HttpResponseMessage resp = await client.GetAsync($"api/product/{id}");
            if (resp.IsSuccessStatusCode)
            {
                var result = resp.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(result);

            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductData product)
        {
            HttpClient client = _prodapi.Initial();
            var postTask = client.PostAsJsonAsync<ProductData>("api/product", product);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = new ProductData();
            HttpClient client = _prodapi.Initial();
            HttpResponseMessage resp = await client.DeleteAsync($"api/product/{id}");
           
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProductData product,int id)
        {
            //var products = new ProductData();
            HttpClient client = _prodapi.Initial();
            var resp = await client.PutAsJsonAsync($"api/product/{id}",product);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
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
