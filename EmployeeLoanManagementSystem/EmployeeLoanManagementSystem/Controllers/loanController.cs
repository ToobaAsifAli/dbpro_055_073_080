using EmployeeLoanManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class loanController : Controller
    {
       // public int ide;
        SqlConnection conn = new SqlConnection("Data Source=HAIER-PC\\SQLEXPRESS;Initial Catalog=DB7;User ID=sa;Password=maham180598;MultipleActiveResultSets=True;Application Name=EntityFramework");
        private DB7Entities3 db = new DB7Entities3();
        // GET: loan
        public ActionResult Index()
        {
            return View();
        }
        private int emp_id(string email1)
        {
            string query;
            query = "SELECT Id FROM Employee WHERE  Email ='" + email1 + "' ";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse(reader[0].ToString());
            }
            return value;
        }
        private int RetrieveID()
        {
            int value = 0;
            try
            {
                string query = " Select LoanApplyId  from LoanApply where (LoanApplyId = SCOPE_IDENTITY());";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                var val = cmd.ExecuteScalar().ToString();
                value = int.Parse(val);

            }
            catch (Exception ex)
            {
                throw;
            }


            return value;
        }
        private int cat_id(string email1)
        {
            string query;

            query = "SELECT Id FROM LoanCategory WHERE  Type ='" + email1 + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse(reader[0].ToString());
            }
            return value;
        }
        // GET: loan/Details/5
        public ActionResult Details(int ide)
        {
            return View();
        }
        public ActionResult welcome()
        {
            return View();
        }
        public ActionResult loanpolicies()
        {
            return View();
        }
        [HttpGet]
        public ActionResult documentsverify()
        {

            return View();
        }
        public ActionResult loanstat()
        {
            return View();
        }
      
        public ActionResult instructions()
        {
            return View();
        }
        public ActionResult provident()
        {
            string s = "Against Provident Fund";
            int cat = cat_id(s);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT LoanApplyId,LoanMoney,RequestDate,LoanForProperty,LoanForAutomobile from LoanApply JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanApply.LoanApplyId JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanCategory='" + cat + "' AND Employee.Email ='" + User.Identity.GetUserName().ToString() + "'";
            //va
            string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,LoanForProperty,LoanForAutomobile from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory   where LoanCategory='" + cat + "' ";

            SqlDataAdapter da = new SqlDataAdapter(query1,conn);
            da.Fill(dt);

           // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        public ActionResult general()
        {
            string s = "General Loan";
            int cat = cat_id(s);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason from LoanApply JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanApply.LoanApplyId JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanCategory='" + cat + "' AND Employee.Email ='" + User.Identity.GetUserName().ToString() + "'";
            //va
            string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory   where LoanCategory='" + cat + "' ";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        // GET: loan/Create
        public ActionResult Create()
        {
            return View();
        }
        //file download
        public FileResult Download()
        {
            string path = Server.MapPath("~/assets/files");
            string filename = Path.GetFileName("agreement.pdf");
            string fullpath = Path.Combine(path, filename);
            return File(fullpath, "pdf", "employee_agreement.pdf");

            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        // POST: loan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
           // try
            //{
                // TODO: Add insert logic here
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                int catid = 0;
                string loancategory = collection["Category"].ToString();
                if (loancategory == "AgainstProvidentFund")
                {
                    string l = "Against Provident Fund";
                    catid = cat_id(l);
                }
                else if (loancategory == "General")
                {
                    string l = "General Loan";
                    catid = cat_id(l);
                }
                byte[] image1 = Encoding.ASCII.GetBytes(collection["instalacq"].ToString());
                int loanreq = int.Parse(collection["loanreq"]);
                int instalacq = int.Parse(collection["instalacq"]);

                DateTime enddate = DateTime.Parse(collection["enddate"]);
                DateTime startdate = DateTime.Parse(collection["startdate"]);
                int instal = (instalacq);
                decimal loan1 = loanreq;
                string email = collection["email"].ToString();
                decimal instalmoney = decimal.Parse(collection["instalmoney"]);

                // TODO: Add insert logic here
                string email1 = email;
                int emailid = emp_id(email1);
                //  string loan_mon = ;
                //  int  = emp_id(email1);
                string loanpur = null;
                if ((loancategory == "General"))
                { loanpur = null; }
                else
                {
                    if (collection["Purpose"].ToString() == null)
                    {
                        loanpur = null;
                    }
                    else
                    {
                        loanpur = collection["Purpose"].ToString();

                    }
                }
                DateTime req = DateTime.Now;
                string choose = "N";
                string choose1 = "Y";

                if (loanpur == "Automobile")
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose + "','" + choose1 + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    int j = RetrieveID();
                    string req1 = "Pending";
                    string reason1 = null;
                    // DateTime res = ;
                    string qry1 = "insert into LoanRequestStatus(LoanId,RequestStatus,Reason) values('" + j + "','" + req1 + "', '" + reason1 + "')    ";
                    SqlCommand sc1 = new SqlCommand(qry1, conn);
                    int q = sc1.ExecuteNonQuery();
                    string defaulter = "N";
                    string qry2 = "insert into LoanDocumentVerify(LoanId,IsDefaulter,Status)VALUES('" + j + "','" + defaulter + "','" + req1 + "')";
                    SqlCommand sc2 = new SqlCommand(qry2, conn);
                    int w = sc2.ExecuteNonQuery();
                }
                else if (loanpur == "Property")
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose1 + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    int j = RetrieveID();
                    string req1 = "Pending";
                    string reason1 = null;
                    // DateTime res = ;
                    string qry1 = "insert into LoanRequestStatus(LoanId,RequestStatus,Reason) values('" + j + "','" + req1 + "', '" + reason1 + "' )   ";
                    SqlCommand sc1 = new SqlCommand(qry1, conn);
                    int q = sc1.ExecuteNonQuery();
                    string defaulter = "N";
                    string qry2 = "insert into LoanDocumentVerify(LoanId,IsDefaulter,Status)VALUES('" + j + "','" + defaulter + "','" + req1 + "')";
                    SqlCommand sc2 = new SqlCommand(qry2, conn);
                    int w = sc2.ExecuteNonQuery();
                }
                else
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        int j = RetrieveID();
                        string req1 = "Pending";
                        string reason1 = null;
                        // DateTime res = ;
                        string qry1 = "insert into LoanRequestStatus(LoanId,RequestStatus,Reason) values('" + j + "','" + req1 + "', '" + reason1 + "')    ";
                        SqlCommand sc1 = new SqlCommand(qry1, conn);
                        int q = sc1.ExecuteNonQuery();
                        // ViewBag.Message = ("You have successfully applied for loan.");
                        TempData["Success"] = "Added Successfully!";
                        return RedirectToAction("welcome", "loan");
                    }
                    else
                    {
                        TempData["Success"] = "Not Added Successfully!";

                    }
                    }

                return View();


           // }

           /* catch
            {
                return View();
            }*/
        }

        // GET: loan/Edit/5
        public ActionResult Edit(int ide)
        {
           // int ide = int.Parse(collection["loanid"]);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT LoanCategory,LoanForProperty,LoanForAutomobile,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,LoanAgreement From LoanApply JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanApplyId='" + ide + "' ";
            //va
           // string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory   where LoanCategory='" + cat + "' ";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);

           // return View();
            
        }

        // POST: loan/Edit/5
        [HttpPost]
        public ActionResult Edit(int ide, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                byte[] image1 = Encoding.ASCII.GetBytes(collection["instalacq"].ToString());
                int loanreq = int.Parse(collection["loanreq"]);
                int instalacq = int.Parse(collection["instalacq"]);

                DateTime enddate = DateTime.Parse(collection["enddate"]);
                DateTime startdate = DateTime.Parse(collection["startdate"]);
                int instal = (instalacq);
                decimal loan1 = loanreq;
                string email = collection["email"].ToString();
                decimal instalmoney = decimal.Parse(collection["instalmoney"]);
                int catid = 0;
                // TODO: Add insert logic here
                string email1 = email;
                int emailid = emp_id(email1);
                //  string loan_mon = ;
                //  int  = emp_id(email1);
                string loancategory = collection["Category"].ToString();
                if (loancategory == "AgainstProvidentFund")
                {
                    string l = "Against Provident Fund";
                    catid = cat_id(l);
                }
                else if (loancategory == "General")
                {
                    string l = "General Loan";
                    catid = cat_id(l);
                }
                DateTime req = DateTime.Now;
                string choose = "N";
                string choose1 = "Y";
                string loanpur = collection["Purpose"].ToString();
                if (loancategory == "Automobile")
                {
                    String qry = "update LoanApply  set values(LoanCategory='" + catid + "',LoanMoney='" + loan1 + "',NoOfInstallments='" + instal + "',InstallmentStartDate='" + startdate + "',InstallmentEndDate=''" + enddate + "',RequestDate='" + req + "',LoanAgreement='" + image1 + "',LoanForProperty='" + choose + "',LoanForAutomobile='" + choose + "',InstallmentMoney='" + instalmoney + "'where LoanApplyId = '" + ide + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                }
                else if (loancategory == "Property")
                {
                    String qry = "update LoanApply  set values(LoanCategory='" + catid + "',LoanMoney='" + loan1 + "',NoOfInstallments='" + instal + "',InstallmentStartDate='" + startdate + "',InstallmentEndDate=''" + enddate + "',RequestDate='" + req + "',LoanAgreement='" + image1 + "',LoanForProperty='" + choose1 + "',LoanForAutomobile='" + choose + "',InstallmentMoney='" + instalmoney + "'where LoanApplyId = '" + ide + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                }
                else
                {
                    String qry = "update LoanApply  set values(LoanCategory='" + catid + "',LoanMoney='" + loan1 + "',NoOfInstallments='" + instal + "',InstallmentStartDate='" + startdate + "',InstallmentEndDate=''" + enddate + "',RequestDate='" + req + "',LoanAgreement='" + image1 + "',LoanForProperty='" + choose + "',LoanForAutomobile='" + choose + "',InstallmentMoney='" + instalmoney + "' where LoanApplyId = '"+ide+"') ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    if (i >= 1)
                    { ViewBag(); }
                    else
                    {
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult documentsverify(int? ide,FormCollection collection)
        {
            if (collection["image1"] != null)
            {
                byte[] image1 = Encoding.ASCII.GetBytes(collection["image1"].ToString());
            }

            return View();
        }
        // GET: loan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: loan/Delete/5
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
