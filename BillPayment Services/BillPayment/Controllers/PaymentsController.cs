using BillPayment.Models;
using BillPayment.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillPayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPayementRepository _repo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PaymentsController));
        public PaymentsController(IPayementRepository repo)
        {
            this._repo = repo;
        }
        [HttpPost]
        public IActionResult Post([FromBody] BillServices model)
        {
            try
            {
                _log4net.Info("Bill details");


                if (ModelState.IsValid)
                {

                    var payobj = _repo.MakePayment(model);
                
                    return Ok(payobj);

                }
                return BadRequest();


            }
            catch
            {
                _log4net.Error("Error");


                return new NoContentResult();
            }

        }
    }
}
