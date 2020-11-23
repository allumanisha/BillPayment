using BillPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillPayment.Repository
{
    public interface IPayementRepository
    {
        public Payment MakePayment(BillServices services);
    }
}
