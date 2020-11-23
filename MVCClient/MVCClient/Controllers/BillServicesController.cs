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
    public class BillServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         public async Task<IActionResult> MakePayment(int BillNo)
         {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                BillServices Item = new BillServices();
                BillServicesViewModel s = new BillServicesViewModel ();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44328/api/Payments/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<BillServices>(apiResponse);
                    }
                    s.BillId = Item.BillNo;
                    s.Amt = Item.BillAmt;
                    
                }
                return View(s);
            }
        }
    }
}
