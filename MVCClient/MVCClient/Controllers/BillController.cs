using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetBill(int id)
        {
            
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                BillServices b = new BillServices();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44360/api/Bill/GetDetails"+id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        b = JsonConvert.DeserializeObject<BillServices>(apiResponse);
                    }
                  



                }
                return View(b);
            }
        }
    }
}
