using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCareInc.Models
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemID { get; set; }
        public string InvoiceItemName { get; set; }
        public double InvoiceItemPrice { get; set; }

        //Representing the Many in (One invoice to Many InvoiceItems)

        public int InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]
        public virtual Invoice Invoice { get; set; }
    }
}