using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Demo3MVC.Models;

namespace MVC2Project.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        // display all
        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            string qry = "select * from Product";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToInt32(dr["price"]);
                    product.Company = dr["company"].ToString();

                    list.Add(product);
                }
            }
            con.Close();
            return list;
        }
        // display by id
        public Product GetProductById(int id)
        {
            Product product = new Product();
            string qry = "select * from Product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToInt32(dr["price"]);
                    product.Company = dr["company"].ToString();

                }
            }
            con.Close();
            return product;
        }
        // add record
        public int AddProduct(Product prod)
        {
            int result = 0;
            string qry = "insert into Product values(@name,@price,@company)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@company", prod.Company);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //update record
        public int UpdateProduct(Product prod)
        {
            int result = 0;
            string qry = "update Product set name=@name,price=@price,company=@company where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@company", prod.Company);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        // delete record
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from Product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


    }
}
