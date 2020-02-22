using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoCareInc.Data;
using AutoCareInc.Models;
using System.Diagnostics;
using AutoCareInc.Models.ViewModels;

namespace AutoCareInc.Controllers
{//create instance of DB
    public class InvoiceController : Controller
    {
        private AutoCareIncContext db = new AutoCareIncContext();
        // GET: Invoice
        //list method
        public ActionResult List()
        {
            return View(db.Invoices.ToList());
        }
        //add method. First one will be to show the add page only. This page requires the list of customers.
        public ActionResult Add()
        {
            List<Customer> customers = db.Customers.SqlQuery("select * from customers").ToList();
            List<InvoiceItem> invoiceItems = db.InvoiceItems.SqlQuery("select * from invoiceitems").ToList();
            AddInvoice viewmodel = new AddInvoice();
            viewmodel.customers = customers;
            viewmodel.invoiceItems = invoiceItems;
            return View(viewmodel);
        }
        //second will be to grab the info from the user form and write it to the DB
        [HttpPost]
        public ActionResult Add(int CustomerID, DateTime InvoiceDate, string InvoiceNotes)
        {
            Debug.WriteLine("I am adding a record to DB");
            //write query
            string query = "insert into invoices (InvoiceDate, InvoiceNotes, CustomerID) values(@InvoiceDate, @InvoiceNotes, @CustomerID)";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@InvoiceDate", InvoiceDate);
            parameters[1] = new SqlParameter("@InvoiceNotes", InvoiceNotes);
            parameters[2] = new SqlParameter("@CustomerID", CustomerID);
            //execute the query
            db.Database.ExecuteSqlCommand(query, parameters);
            return RedirectToAction("List");
        }//method to show a particular invoice
        public ActionResult Show(int? id)
        {
            Invoice invoice = db.Invoices.SqlQuery("Select * from invoices where invoiceid=@invoiceid", new SqlParameter("@invoiceid",id)).FirstOrDefault();
            return View (invoice);
        }
        //methods to update the invoice. 
        //First method is to show the base information
        public ActionResult Update(int id)
        {
            Invoice seletedinvoice= db.Invoices.SqlQuery("Select * from invoices where invoiceid=@invoiceid", new SqlParameter("@invoiceid", id)).FirstOrDefault();
            return View(seletedinvoice);
        }
        //second is to take the information from the user and update the DB
        [HttpPost]
        public ActionResult Update(int id, string InvoiceNotes, DateTime InvoiceDate)
        {
            string query = "Update invoices set InvoiceDate=@InvoiceDate, InvoiceNotes=@InvoiceNotes where InvoiceID=@InvoiceID";
            SqlParameter[] parameters= new SqlParameter[3];
            parameters[0] = new SqlParameter("@InvoiceNotes", InvoiceNotes);
            parameters[1] = new SqlParameter("@InvoiceID", id);
            parameters[2] = new SqlParameter("@InvoiceDate", InvoiceDate);

            //execute the query
            db.Database.ExecuteSqlCommand(query, parameters);
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            //this method will display the base information of the record
            
            Invoice seletedinvoice = db.Invoices.SqlQuery("Select * from invoices where invoiceid=@invoiceid", new SqlParameter("@invoiceid", id)).FirstOrDefault();
            return View(seletedinvoice);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "Delete from invoices where invoiceid=@invoiceid";
            SqlParameter parameter = new SqlParameter("@invoiceid", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}