using AdoBLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBLL
{
    public class AccessData
    {
        private readonly string connectionString = "Data Source=ARAJOJO\\SQLEXPRESS;Initial Catalog=CrudDotNet;Integrated Security=True";


        public List<ProductBLL> GetAllProducts()
        {
            List<ProductBLL> listProducts = new List<ProductBLL>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    string query = "SELECT Id,Name,Value,Description FROM Produto WITH(NOLOCK)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var productBLL = new ProductBLL
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Value = reader.GetDecimal(2),
                            Description = reader.GetString(3)
                        };
                        listProducts.Add(productBLL);
                    }
                    con.Close();

                }
            }
            return listProducts;
        }

        public void CreateProduct(ProductBLL product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Produto(Name,Value,Description) VALUES(@Name, @Value,@Description)";
                    cmd.Parameters.Add(new SqlParameter("@Name", product.Name));
                    cmd.Parameters.Add(new SqlParameter("@Value", product.Value));
                    cmd.Parameters.Add(new SqlParameter("@Description", product.Description));
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void UpdateProduct(ProductBLL product)
        {
            if (product.Id > 0)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE Produto SET Name=@Name,Value=@Value,Description=@Description WHERE Id=@Id";
                        cmd.Parameters.Add(new SqlParameter("@Name", product.Name));
                        cmd.Parameters.Add(new SqlParameter("@Value", product.Value));
                        cmd.Parameters.Add(new SqlParameter("@Description", product.Description));
                        cmd.Parameters.Add(new SqlParameter("@Id", product.Id));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
                throw new Exception("Product not found!!!");
   
        }


        public void DeleteProduct(ProductBLL product)
        {
            if (product.Id > 0)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM Produto WHERE Id=@Id";              
                        cmd.Parameters.Add(new SqlParameter("@Id", product.Id));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
                throw new Exception("Product not found!!!");

        }

    }
}
