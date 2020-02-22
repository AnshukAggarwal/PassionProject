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

namespace AutoCareInc.Controllers
{
    public class InvoiceItemController : Controller
    {
        private AutoCareIncContext db = new AutoCareIncContext();
        // GET: InvoiceItem
        public ActionResult List()
        {
            return View(db.InvoiceItems.ToList());
        }
        //show invoice item
        public ActionResult Show(int? id)
        {   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceitem = db.InvoiceItems.SqlQuery("Select * from invoiceitems where invoiceitemid=@InvoiceItemID", new SqlParameter("@InvoiceItemID",id)).FirstOrDefault();
            if (invoiceitem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceitem);
        }
        //method for adding a record. We need two method. One to show the base page and one to write to the db once the user submits the form.
        public ActionResult Add()
        {
            return View();
        }
        //now the Httppost add method
        [HttpPost]
        public ActionResult Add(string InvoiceItemName, double InvoiceItemPrice)
        {
            //pull the data from the form fields and write them to the DB
            string query = "Insert into InvoiceItems (InvoiceItemName,InvoiceItemPrice) values (@InvoiceItemName, @InvoiceItemPrice)";
            SqlParameter[] parameters= new SqlParameter[2];
            parameters[0] = new SqlParameter("@InvoiceItemName", InvoiceItemName);
            parameters[1] = new SqlParameter("@InvoiceItemPrice", InvoiceItemPrice);
            //execute the query
            db.Database.ExecuteSqlCommand(query, parameters);
            return RedirectToAction("List");
        }
        //methods for editing a record. Again we need two methods. One which show the base info for the record and one which writes the updated
        //values to the DB
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem selecteditem = db.InvoiceItems.SqlQuery("Select * from InvoiceItems where invoiceitemid=@InvoiceItemID", new SqlParameter("@InvoiceItemID", id)).FirstOrDefault();
            return View(selecteditem);
        }
        //now httppost method
        [HttpPost]
        public ActionResult Edit(int id, string InvoiceItemName, double InvoiceItemPrice)
        {   //now pull the data from the form fields.
            string query = "Update invoiceitems set InvoiceItemName=@InvoiceItemName, InvoiceItemPrice=@InvoiceItemPrice where invoiceitemid=@invoiceitemid ";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@InvoiceItemName", InvoiceItemName);
            parameters[1] = new SqlParameter("@InvoiceItemPrice", InvoiceItemPrice);
            parameters[2] = new SqlParameter("@invoiceitemid",id);
            //execute this query now
            db.Database.ExecuteSqlCommand(query, parameters);
            return RedirectToAction("List");
        }
        //methods to delete a record from DB. Again the same, two methods, one to show base info and one to delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem selecteditem = db.InvoiceItems.SqlQuery("Select * from invoiceitems where invoiceitemid=@InvoiceItemID", new SqlParameter("@InvoiceItemID", id)).FirstOrDefault();

            return View(selecteditem);
        }
        [HttpPost]
        public ActionResult Delete (int id)
        {
            string query = "Delete from invoiceitems where invoiceitemid=@InvoiceItemID";
            SqlParameter parameter = new SqlParameter("@InvoiceItemID", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}