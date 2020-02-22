using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCareInc.Models.ViewModels
{
    public class AddInvoice
    {
        //this class will have 2 things.
        //1. the list of customers
        //2. the list of invoice items
        //the list of customers
        public List<Customer> customers { get; set; }
        //the list of invoice items
        public List<InvoiceItem> invoiceItems { get; set; }
    }
}