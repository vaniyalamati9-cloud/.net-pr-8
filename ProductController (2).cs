using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["InventoryDB"]
            .ConnectionString;

        // READ
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Product";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId =
                            Convert.ToInt32(reader["ProductId"]),

                        ProductName =
                            reader["ProductName"].ToString(),

                        Category =
                            reader["Category"].ToString(),

                        Price =
                            Convert.ToDecimal(reader["Price"]),

                        Quantity =
                            Convert.ToInt32(reader["Quantity"])
                    });
                }
            }

            return View(products);
        }

        // CREATE - GET
        public ActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con =
                       new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Product
                        (ProductName, Category, Price, Quantity)
                        VALUES
                        (@ProductName, @Category, @Price, @Quantity)";

                    SqlCommand cmd =
                        new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(
                        "@ProductName", product.ProductName);

                    cmd.Parameters.AddWithValue(
                        "@Category", product.Category);

                    cmd.Parameters.AddWithValue(
                        "@Price", product.Price);

                    cmd.Parameters.AddWithValue(
                        "@Quantity", product.Quantity);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // EDIT - GET
        public ActionResult Edit(int id)
        {
            Product product = null;

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                string query =
                    "SELECT * FROM Product WHERE ProductId=@ProductId";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId =
                            Convert.ToInt32(reader["ProductId"]),

                        ProductName =
                            reader["ProductName"].ToString(),

                        Category =
                            reader["Category"].ToString(),

                        Price =
                            Convert.ToDecimal(reader["Price"]),

                        Quantity =
                            Convert.ToInt32(reader["Quantity"])
                    };
                }
            }

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con =
                       new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Product
                        SET ProductName=@ProductName,
                            Category=@Category,
                            Price=@Price,
                            Quantity=@Quantity
                        WHERE ProductId=@ProductId";

                    SqlCommand cmd =
                        new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(
                        "@ProductId", product.ProductId);

                    cmd.Parameters.AddWithValue(
                        "@ProductName", product.ProductName);

                    cmd.Parameters.AddWithValue(
                        "@Category", product.Category);

                    cmd.Parameters.AddWithValue(
                        "@Price", product.Price);

                    cmd.Parameters.AddWithValue(
                        "@Quantity", product.Quantity);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // DELETE
        public ActionResult Delete(int id)
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                string query =
                    "DELETE FROM Product WHERE ProductId=@ProductId";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}