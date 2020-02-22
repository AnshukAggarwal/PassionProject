using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoCareInc.Models;

namespace AutoCareInc.Data
{
    public class AutoCareIncContext : DbContext
    {
        public AutoCareIncContext() : base("name=AutoCareIncContext")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems{ get; set; }

    }
}