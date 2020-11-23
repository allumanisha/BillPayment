using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            
            User Item = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:44361/api/Auth/User", content);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Item = JsonConvert.DeserializeObject<User>(apiResponse);
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44361/api/Auth/Login", content1))
                {
                    if (!response1.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }

                    string apiResponse1 = await response1.Content.ReadAsStringAsync();



                    string stringJWT = response1.Content.ReadAsStringAsync().Result;


                    JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                    HttpContext.Session.SetString("token", jwt.Token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(Item));
                    HttpContext.Session.SetInt32("Userid", Item.Userid);
                    HttpContext.Session.SetString("Username", Item.Username);
                    ViewBag.Message = "User logged in successfully!";

                    return RedirectToAction("Index", "Bill");


                }
            }
        }
        public ActionResult Logout()
        {
           
            HttpContext.Session.Remove("token");
            // HttpContext.Session.SetString("user", null);

            return View("Login");
        }
    }
}
