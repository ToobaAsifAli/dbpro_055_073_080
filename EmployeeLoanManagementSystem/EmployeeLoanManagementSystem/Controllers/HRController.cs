using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using EmployeeLoanManagementSystem.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Text;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class HRController : Controller
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-OFNFN9P\\SKI2017;initial catalog=DB7;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot");
        
        DB7Entities3 db = new DB7Entities3();

        // GET: HR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        //Pending Requests
        public ActionResult PendingRequest()
        {

                DataTable dt = new DataTable();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string q;
            string pen = "Pending";
                q = "select FirstName + LastName as [Name], Email,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where RequestStatus = '"+pen+"'";

                SqlDataAdapter da = new SqlDataAdapter(q, conn);
                da.Fill(dt);
                // r data = db.LoanApplies.SqlQuery(query).ToList();
                return View(dt);
        }
        public ActionResult AcceptRequest()
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string q;
                q = "select FirstName + LastName as [Name], Email,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where RequestStatus = 'Accepted'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
            
        }
        public ActionResult RejectRequest()
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string q;
                q = "select FirstName + LastName as [Name], Email,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where RequestStatus = 'Rejected'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        // GET: HR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HR/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HR/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HR/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
