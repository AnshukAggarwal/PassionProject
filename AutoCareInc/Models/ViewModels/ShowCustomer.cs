using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCareInc.Models.ViewModels
{
    public class ShowCustomer
    {
        //this class shows the record of a customer and the list of invoices for that specific owner
        //individual customer
        public virtual Customer customer { get; set; }
        //list of all invoices they have
        public List<Invoice> invoices { get;  set; }
    }
}