using AdoBLL;
using AdoBLL.Models;
using System;

namespace ExemploADO
{
    internal class Program
    {
        public static void Main(string []args)
        {
            AccessData da = new AccessData();

            try
            {
                //Create 
                ProductBLL newProduct = new ProductBLL
                {
                    Name = "New Product",
                    Value = 50.00m,
                    Description = "This is a new product"
                };
                da.CreateProduct(newProduct);


                //Update
                ProductBLL updatedProduct = new ProductBLL
                {
                    Id = 5,
                    Name = "Updated Product",
                    Value = 100.00m,
                    Description = "This is the updated product."
                };
                da.UpdateProduct(updatedProduct);


                //Delete
                da.DeleteProduct(updatedProduct);


                //Read
                var products = da.GetAllProducts();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id} - {product.Name} - R$ {product.Value.ToString("F2")} - {product.Description}.");
                }
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error. {ex.Message}");
            }
            
        }
    }
}
