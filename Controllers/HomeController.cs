using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using token.Models;

namespace token.Controllers
{
    [ServiceFilter(typeof(LoginFilter))]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Gazete(string token)
        {
            if (token != null)
            {
                ViewBag.Token = token;
            }
            return View();
            /*using (var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            })
            using (var client = new HttpClient(handler))
            {
                HttpContext.Session.TryGetValue("token", out var tokenValue);
                string tokenSession = System.Text.Encoding.UTF8.GetString(tokenValue).Split('æ')[0];
                var response = await client.GetAsync($"https://localhost:5001/api/news/{tokenSession}");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var data=JsonConvert.DeserializeObject<List<Kategoris>>(content);
                return View(data);
            }*/
        }
        public IActionResult Index(string token)
        {
            if (token != null)
            {
                ViewBag.Token = token;
            }
            return View();
        }

        public IActionResult About(string token)
        {
            if (token != null)
            {
                ViewBag.Token = token;
            }
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact(string token)
        {
            if (token != null)
            {
                ViewBag.Token = token;
            }
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy(string token)
        {
            if (token != null)
            {
                ViewBag.Token = token;
            }
            return View();
        }
        [IgnoreAttribute]
        public IActionResult Login()
        {
            return View();
        }
        [IgnoreAttribute]
        [HttpPost]
        public ActionResult LogIn(string name, string password)
        {
            //Static UserName Password Check            
            if (name == "bora" && password == "1234")
            {
                string token = Guid.NewGuid().ToString()+"æ" + DateTime.Now;
                HttpContext.Session.Set("token", System.Text.Encoding.UTF8.GetBytes(token));
                ViewBag.Token = token;
                return Content(token);
            }
            else
            {
                return Content("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
