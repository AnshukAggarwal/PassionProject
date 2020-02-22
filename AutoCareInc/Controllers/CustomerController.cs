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
using PagedList;
using PagedList.Mvc;
using AutoCareInc.Models.ViewModels;

namespace AutoCareInc.Controllers
{
    public class CustomerController : Controller
    {
        //create an instance of the context DB

        private AutoCareIncContext db = new AutoCareIncContext(); 
        // GET: Customer
        public ActionResult List(string searchBy, string search, int? page)
        {   //write a sql query to fetch the list of customers. This will be a list of type customer
            if (searchBy == "First Name")
            {
                return View(db.Customers.Where(x =>x.CustomerFname.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(db.Customers.Where(x => x.CustomerLname.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 3));
            }
            //List<Customer> customers = db.Customers.SqlQuery("Select * from Customers").ToList();
            //return View(customers);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Add(string CustomerFname, string CustomerLname, string CustomerAddress, string CustomerEmail, string CustomerPhone)
        {
            
            string query = "insert into customers (CustomerFname, CustomerLname, CustomerAddress, CustomerEmail, CustomerPhone) values (@CustomerFname,@CustomerLname,@CustomerAddress,@CustomerEmail,@CustomerPhone)";
            SqlParameter[] sqlparams = new SqlParameter[5]; 
            
            sqlparams[0] = new SqlParameter("@CustomerFname", CustomerFname);
            sqlparams[1] = new SqlParameter("@CustomerLname", CustomerLname);
            sqlparams[2] = new SqlParameter("@CustomerAddress", CustomerAddress);
            sqlparams[3] = new SqlParameter("@CustomerEmail", CustomerEmail);
            sqlparams[4] = new SqlParameter("@CustomerPhone", CustomerPhone);

            //execute the query
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of pets so we can see our new one!
            return RedirectToAction("List");
        }

        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Customer customer= db.Customers.SqlQuery("select * from customers where customerid=@CustomerID", new SqlParameter("@CustomerID", id)).FirstOrDefault();
            string second_query = "Select * from invoices join customers on customers.customerid=invoices.customerid where customers.customerid=@CustomerID";
            SqlParameter sqlparam = new SqlParameter("@CustomerID", id);
            List<Invoice> CustomerInvoices = db.Invoices.SqlQuery(second_query, sqlparam).ToList();
            ShowCustomer viewmodel = new ShowCustomer();
            viewmodel.customer = customer;
            viewmodel.invoices = CustomerInvoices;
            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {
            //need information about a particular pet
            Customer selectedcustomer = db.Customers.SqlQuery("select * from customers where customerid =@CustomerID", new SqlParameter("@CustomerID", id)).FirstOrDefault();

            return View(selectedcustomer);
        }
        [HttpPost]
        public ActionResult Edit(int id, string CustomerFname, string CustomerLname, string CustomerAddress, string CustomerEmail, string CustomerPhone)
        {
            string query = "Update customers set CustomerFname= @CustomerFname, CustomerLname= @CustomerLname, CustomerAddress= @CustomerAddress,CustomerEmail=@CustomerEmail, CustomerPhone= @CustomerPhone where customerid=@CustomerID ";
            SqlParameter[] sqlparams = new SqlParameter[6];//array of parameters
            sqlparams[0] = new SqlParameter("@CustomerFname", CustomerFname);
            sqlparams[1] = new SqlParameter("@CustomerLname", CustomerLname);
            sqlparams[2] = new SqlParameter("@CustomerAddress", CustomerAddress);
            sqlparams[3] = new SqlParameter("@CustomerEmail", CustomerEmail);
            sqlparams[4] = new SqlParameter("@CustomerPhone", CustomerPhone);
            sqlparams[5] = new SqlParameter("@CustomerID", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");

            
        }

        public ActionResult Delete(int? id)
        {
            //this method shows the details of the selected pet
            Debug.WriteLine("I am trying to pull the record for");
            Customer selectedcustomer = db.Customers.SqlQuery("select * from customers where customerid =@CustomerID", new SqlParameter("@CustomerID", id)).FirstOrDefault();

            return View(selectedcustomer);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {   //this methods deletes the pet from the DB.
            string query = "Delete from customers where customerid= @CustomerID";
            SqlParameter sqlparam = new SqlParameter("@CustomerID", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);
            return RedirectToAction("List");
        }
    }
}