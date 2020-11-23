using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models.ViewModel
{
    public class BillServices
    {
        [Key]
        [Display(Name = "Bill Number")]
        public int BillNo { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Bill Amount")]
        public double BillAmt { get; set; }
    }
}
