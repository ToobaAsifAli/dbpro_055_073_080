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
        SqlConnection conn = new SqlConnection("Data Source=HAIER-PC\\SQLEXPRESS;Initial Catalog=DB7;User ID=sa;Password=maham180598;MultipleActiveResultSets=True;Application Name=EntityFramework");

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
        public ActionResult instructions()
        {
            return View();
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
            try
            {
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
                
                DateTime req = DateTime.Now;
                string choose = "N";
                string choose1 = "Y";
                string loanpur = collection["Purpose"].ToString();
                if (loancategory == "Automobile")
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose + "','" + choose1 + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                }
                else if (loancategory == "Property")
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose1 + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                }
                else
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    if (i >= 1)
                    { ViewBag(); }
                    else
                    {
                    }
                }
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: loan/Edit/5
        public ActionResult Edit(int id)
        {
            
                return View();
            
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
