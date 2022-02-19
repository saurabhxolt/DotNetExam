using ModuleExam.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuleExam.Controllers
{
    public class ProductsController : Controller
    {
        
        
        // GET: Products
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cdac;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn.Open();
            SqlCommand cmdShow = new SqlCommand();
            cmdShow.Connection = conn;
            cmdShow.CommandType = System.Data.CommandType.Text;
            cmdShow.CommandText = "Select * from Products";
            List<Products> prodList = new List<Products>();
            try
            {
                SqlDataReader dr = cmdShow.ExecuteReader();
                while (dr.Read())
                {
                    prodList.Add(new Products 
                    { 
                        ProductId = (int)dr["ProductId"], 
                        ProductName = (string)dr["ProductName"], 
                        Rate = (decimal)dr["Rate"], 
                        Description = (string)dr["Description"],
                        CategoryName = (string)dr["CategoryName"] 
                    }
                    );
                }
                dr.Close();
            }
            catch(Exception e)
            {
                ViewBag.ErrMsg = e.Message;
            }
            finally
            {
                conn.Close();
            }
            return View(prodList);
        }

      
      
        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cdac;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn.Open();
            SqlCommand cmdShow = new SqlCommand();
            cmdShow.Connection = conn;
            cmdShow.CommandType = System.Data.CommandType.Text;
            cmdShow.CommandText = "Select * from Products where ProductId=" + id;
            SqlDataReader dr = cmdShow.ExecuteReader();
            Products prod = null;
            if(dr.Read())
            {
                prod = new Products
                {
                    ProductId = (int)dr["ProductId"],
                    ProductName = (string)dr["ProductName"],
                    Rate = (decimal)dr["Rate"],
                    Description = (string)dr["Description"],
                    CategoryName = (string)dr["CategoryName"]
                };
            }
            dr.Close();
            conn.Close();
            return View(prod);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Products obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cdac;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn.Open();

            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = cn;
            cmdUpdate.CommandType = System.Data.CommandType.Text;
            cmdUpdate.CommandText = "update Products set  ProductName=@ProductName, Rate=@Rate, Description=@Description, CategoryName= @CategoryName where ProductId = @ProductId";
            cmdUpdate.Parameters.AddWithValue("ProductId", id);
            cmdUpdate.Parameters.AddWithValue("ProductName", obj.ProductName);
            cmdUpdate.Parameters.AddWithValue("Rate", obj.Rate);
            cmdUpdate.Parameters.AddWithValue("Description", obj.Description);
            cmdUpdate.Parameters.AddWithValue("CategoryName", obj.CategoryName);

            try
            {
                cmdUpdate.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return View();
        }

       
    }
}
