using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BillPayment.Models
{
    public class Payment
    {
        
        [Key]
        public int PaymentId { get; set; }
        public int BillId { get; set; }
        public double Amount { get; set; }
        [ForeignKey("BillNo")]
       public virtual BillServices Bills { get; set; }

    }
}
