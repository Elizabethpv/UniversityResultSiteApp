using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityResultSiteApp.Models;

namespace UniversityResultSiteApp.Controllers
{
    public class HomeController : Controller
    {
       
        // GET: Home
        public ActionResult StudentResult()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentResultView(string testChar)
        {
            Student student = new Student();

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);

            con.Open();
            SqlCommand cmd = new SqlCommand("PersonTopView", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("testChar", testChar);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                student.ID = Convert.ToInt32(reader["ID"]);
                student.Name = reader["Name"].ToString();
                student.Malayalam =Convert.ToInt32( reader["Malayalam"]);
                student.English = Convert.ToInt32(reader["English"]);
                student.Hindi = Convert.ToInt32(reader["Hindi "]);
                int total = student.Malayalam + student.Hindi + student.English;
                if(total>200)
                {
                    student.Status = "Pass";

                }
                else
                {
                    student.Status = "Fail";
                }
            }
            con.Close();

            return View("StudentView",student);
          
        }

       
    }
}