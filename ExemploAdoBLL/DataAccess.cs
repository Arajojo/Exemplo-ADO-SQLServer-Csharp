using ExemploAdoBLL.ModelsBLL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExemploAdoBLL
{
    public class DataAccess
    {
        private readonly string connectionString = "Data Source=DESKTOP-Q222PPK\\SQLEXPRESS;Initial Catalog=CrudDotNet;Integrated Security=True";


        public List<ProductBLL> GetAllProducts()
        {
            List<ProductBLL> listProducts = new List<ProductBLL>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    string query = "SELECT Id,Name,Value,Description FROM Product WITH(NOLOCK)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
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


                }
            }
            return listProducts;
        }
    }
}
