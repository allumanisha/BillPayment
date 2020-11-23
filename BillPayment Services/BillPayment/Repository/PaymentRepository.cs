
using BillPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BillPayment.Repository
{
    public class PaymentRepository : IPayementRepository
    {
        private readonly PaymentDbContext _payment;
       
        public PaymentRepository(PaymentDbContext payment)
        {
            this._payment = payment;
            
        }
        public  Payment MakePayment(BillServices services)
        {
            Payment pay = new Payment() {BillId=services.BillNo,Amount=services.BillAmt };
            _payment.Payments.Add(pay);
            _payment.SaveChanges();
            return pay;
            

            
        }

        
    }
}
