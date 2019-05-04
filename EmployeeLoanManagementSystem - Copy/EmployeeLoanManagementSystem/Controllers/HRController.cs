﻿using EmployeeLoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class HRController : Controller
    {
        // public int ide;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-R6RA1PL\\TOOBAASIF;Initial Catalog=ProjectA;Persist Security Info=True;User ID=sa;Password=1212");

        DB7Entities4 db = new DB7Entities4();
        // GET: HR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }

        // GET: HR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        //employee details who applied for loan
        public static int empid;
        public static int loanid;
        [HttpGet]
        
        public ActionResult Edetails(int ide, int idee)
        {
            empid = ide;
            loanid = idee;
            List<string> l = new List<string>();
            string r = "Request Status";
            string q = "select Value from Lookup where Category = '"+r+"'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
          //  int i = 0;
            while (reader.Read())
            {
                value = (reader[0].ToString());
                //   l[i] = value;
                l.Add(value);
             
               // i++;
            }
            DB7Entities4 db = new DB7Entities4();
            Lookup db1 = new Lookup();
            ViewBag.choice = new SelectList(l);
          //s  choice.SelectedValue = l.Attributes("InitialValue");
            var getRequestStatus = db.LoanRequestStatus.ToList();
            SelectList list = new SelectList(getRequestStatus, "LoanId", "RequestStatus");
            ViewBag.requeststatus = list;

            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query1 = "select Employee.Id,LoanApply.LoanApplyId,FirstName +' '+ LastName as [Name], Email,Department.Name,Designation,Salary,HireDate,RequestDate,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId join Department on Department.Id = Employee.DepartmentId where RequestStatus = 'Pending' and Employee.Id = '" + ide + "' and  LoanApplyId = '" + idee + "'";
            SqlDataAdapter da = new SqlDataAdapter(query1, conn);
            da.Fill(dt);
            return View(dt);
        }
        private int instalno(int l)
        {
            string query;
            string r = "Pending";
            query = "select NoOfInstallments from LoanApply join LoanRequestStatus on LoanRequestStatus.LoanId = LoanApply.LoanApplyId where RequestStatus = '"+r+"' AND LoanApplyId = '" + l + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse((reader[0].ToString()));
            }
            return value;
        }


        [HttpPost, ActionName("Edetails")]
        [ValidateAntiForgeryToken]
        public ActionResult EdetailsConfirmed(FormCollection collection)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int ide = empid;
            int idee = loanid;
            int instalnum = instalno(idee);

            string choice = collection["choice"].ToString();
            string reason = collection["reason"].ToString();

            DateTime dt = DateTime.Now;
            DateTime startdate = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1);

            DateTime enddate = DateTime.Now.AddMonths(instalnum);
            DateTime sub = DateTime.Now;
            string qry = "update LoanRequestStatus set RespondDate='" + sub + "',RequestStatus='" +choice+ "',Reason='"+reason+"' where LoanId = '" + idee + "'";
            //  string qry = "insert into LoanDocumentVerify(PropertyDocument)values('"+image1+ "') select * from LoanDocumentVerify where LoanId = '" + ide+"'";
            SqlCommand sc = new SqlCommand(qry, conn);
            int i = sc.ExecuteNonQuery();
            if (choice == "Accepted")
            {
                string cid = "N";
                string qry1 = "update LoanApply set InstallmentStartDate='" + startdate + "',InstallmentEndDate='" + enddate + "' where LoanApplyId = '" + idee + "'";
                SqlCommand sc1 = new SqlCommand(qry1, conn);
                int j = sc1.ExecuteNonQuery();
                for(int p=1;p<=instalnum;p++)
                {
                    string qry2 = "insert into Installment(LoanId,InstallmentsNo,InstallmentDate,IsPaid) Values('"+idee+ "','" +p+ "','" + startdate + "','" +cid+ "')  ";
                    SqlCommand sc2 = new SqlCommand(qry2, conn);
                    int k = sc2.ExecuteNonQuery();
                    startdate = new DateTime(startdate.AddMonths(1).Year, startdate.AddMonths(1).Month, 1);
                }
            }

           
            return RedirectToAction("Welcome");
        }


        // GET: HR/Create
        public ActionResult Create()
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

            string a = "Pending";
            string q;
            q = "select Employee.Id,LoanApply.LoanApplyId, FirstName +' '+ LastName as [Name], Email,LoanMoney,LoanApply.[RequestDate],LoanCategory.Type from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanCategory on LoanApply.LoanCategory = LoanCategory.Id  join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where RequestStatus ='" + a + "'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);
            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        public ActionResult viewreq(int ide)
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

             string q;
            q = "select Employee.Id,LoanApply.LoanApplyId, FirstName +' '+ LastName as [Name], Email,Salary,Department.Name,Designation,HireDate,LoanCategory.Type,LoanMoney,RequestDate,NoOfInstallments,[Installment Money],LoanAgreement,RequestStatus,Reason,RespondDate from Employee Join Department on Department.Id= Employee.DepartmentId  Join LoanApply on Employee.Id = LoanApply.EmployeeId JOIN LoanCategory ON LoanCategory.Id = LoanApply.LoanCategory join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where LoanApplyId = '" + ide+"'";

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

            string a = "Accepted";
            string q;
            q = "select Employee.Id,LoanApply.LoanApplyId, FirstName +' '+ LastName as [Name], Email,LoanMoney,LoanApply.[RequestDate],LoanCategory.Type from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanCategory on LoanApply.LoanCategory = LoanCategory.Id join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where RequestStatus ='" + a + "'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);

        }
        public ActionResult prodocuments()
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string a = "Against Provident Fund";
            string st = "Accepted";
            string q;
            q = "select Employee.Id,LoanApply.LoanApplyId, FirstName +' '+ LastName as [Name], Email,LoanMoney,LoanApply.[RequestDate] from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanCategory on LoanApply.LoanCategory = LoanCategory.Id join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where LoanCategory.Type ='" + a + "' and RequestStatus='"+st+"'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);

        }
        public static int loanide;
        [HttpGet]
        public ActionResult prodocumentsverify(int ide,int idee)
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            loanide = idee;
            List<string> l = new List<string>();
            string r = "Request Status";
            string q1 = "select Value from Lookup where Category = '" + r + "'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(q1, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            //  int i = 0;
            while (reader.Read())
            {
                value = (reader[0].ToString());
                //   l[i] = value;
                l.Add(value);

                // i++;
            }
            DB7Entities4 db = new DB7Entities4();
            Lookup db1 = new Lookup();
            ViewBag.choice = new SelectList(l);
            string a = "Against Provident Fund";
            string st = "Accepted";
            string q;
            q = "select Employee.Id,LoanApply.LoanApplyId, FirstName +' '+ LastName as [Name], Email,LoanMoney,LoanApply.[RequestDate],NoOfInstallments,[Installment Money],InstallmentStartDate,InstallmentEndDate,PropertyDocument,AutomobileDocument,SubmissionDate,IsDefaulter from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanCategory on LoanApply.LoanCategory = LoanCategory.Id join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId join LoanDocumentVerify on LoanDocumentVerify.LoanId = LoanApply.LoanApplyId where LoanDocumentVerify.LoanId ='" + idee+"'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);

        }
        [HttpPost]
        public ActionResult prodocumentsverify(FormCollection collection)
        {

            string reason = collection["Reason"].ToString();
            string choice = collection["choice"].ToString();
            string q;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            q = "update LoanDocumentVerify set Reason='"+reason+ "',Status='" + choice + "',SubmissionDate='" + DateTime.Now + "'";
            SqlCommand sc2 = new SqlCommand(q, conn);
            int k = sc2.ExecuteNonQuery();
            return RedirectToAction("prodocuments");
        }
            public ActionResult RejectRequest()
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string a = "Rejected";
            string q;
            q = "select Employee.Id,LoanApply.LoanApplyId, FirstName +' '+ LastName as [Name], Email,LoanMoney,LoanApply.[RequestDate],LoanCategory.Type from Employee Join LoanApply on Employee.Id = LoanApply.EmployeeId join LoanCategory on LoanApply.LoanCategory = LoanCategory.Id join LoanRequestStatus on LoanApply.LoanApplyId =  LoanRequestStatus.LoanId where RequestStatus ='" + a + "'";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        public ActionResult instal(int ide,int idee)
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string a = "Rejected";
            string q;
            q = "select InstallmentsNo,[Installment Money],InstallmentDate,IsPaid from Installment join LoanApply on LoanApply.LoanApplyId = Installment.LoanId where Installment.LoanId ='"+idee+"' ";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
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
