using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCareInc.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate {get; set;}
        public string InvoiceNotes { get; set; }

        //Representing the Many in (One customer to Many invoices)        
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        //Representing the "Many" in (One Invoice to many InvoiceItems)
        public ICollection<InvoiceItem> InvoiceItems { get; set; }

    }
}