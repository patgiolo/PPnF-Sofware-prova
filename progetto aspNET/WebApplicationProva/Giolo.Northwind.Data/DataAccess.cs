using Giolo.Northwind.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giolo.Northwind.Data
{
    public class DataAccess
    {
        string connectionString;

        public DataAccess()
        {
            //string x = ConfigurationManager.AppSettings["timeout"];

            this.connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public DataAccess(string ConnectionStringName)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }

        // Get all products
        public IEnumerable<Product> GetProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT [ProductID]
      ,[ProductName]
      ,[SupplierID]
      ,[CategoryID]
      ,[QuantityPerUnit]
      ,[UnitPrice]
      ,[UnitsInStock]
      ,[UnitsOnOrder]
      ,[ReorderLevel]
      ,[Discontinued]
  FROM .[dbo].[Products]";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();

                    List<Product> products = new List<Product>();

                    while (reader.Read())
                    {
                        Product p = new Product();

                        p.Id = (int)reader["ProductID"];
                        p.Name = reader["ProductName"] as string;
                        p.Price = reader["UnitPrice"] == DBNull.Value ? null : (decimal?)reader["UnitPrice"];

                        Console.WriteLine(p.Name);
                        products.Add(p);
                    }

                    return products;
                }
            }
        }

        // Get product
        public Product GetProduct(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT [ProductID]
      ,[ProductName]
      ,[SupplierID]
      ,[CategoryID]
      ,[QuantityPerUnit]
      ,[UnitPrice]
      ,[UnitsInStock]
      ,[UnitsOnOrder]
      ,[ReorderLevel]
      ,[Discontinued]
  FROM .[dbo].[Products]
  WHERE ProductID = @id";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    SqlParameter paramId = new SqlParameter("@id", id);
                    command.Parameters.Add(paramId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Product p = new Product();

                        p.Id = (int)reader["ProductID"];
                        p.Name = reader["ProductName"] as string;
                        p.Price = reader["UnitPrice"] == DBNull.Value ? null : (decimal?)reader["UnitPrice"];

                        return p;
                    }
                    return null;
                }
            }
        }

        // Insert product
        public void InsertProduct(Product product)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [dbo].[Products]
           ([ProductName]
           ,[SupplierID]
           ,[CategoryID]
           ,[QuantityPerUnit]
           ,[UnitPrice]
           ,[UnitsInStock]
           ,[UnitsOnOrder]
           ,[ReorderLevel]
           ,[Discontinued])
     VALUES
           (@ProductName
           ,@SupplierID
           ,@CategoryID
           ,@QuantityPerUnit
           ,@UnitPrice
           ,@UnitsInStock
           ,@UnitsOnOrder
           ,@ReorderLevel
           ,@Discontinued)";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@ProductName", product.Name));
                    command.Parameters.Add(new SqlParameter("@SupplierID", product.SupplierID == null ? DBNull.Value : (object)product.SupplierID));
                    command.Parameters.Add(new SqlParameter("@CategoryID", (object)product.CategoryID ?? DBNull.Value));        //stessa cosa di quello sopra ma + veloce
                    command.Parameters.Add(new SqlParameter("@QuantityPerUnit", (object)product.QuantityPerUnit ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", (object)product.Price ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UnitsInStock", (object)product.UnitsInStock ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UnitsOnOrder", (object)product.UnitsOnOrder ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@ReorderLevel", (object)product.ReorderLevel ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Discontinued", product.Discontinued));

                    command.ExecuteNonQuery();
                }
            }
        }

        // Update product
        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [dbo].[Products]
   SET [ProductName] = @ProductName
      ,[SupplierID] = @SupplierID 
      ,[CategoryID] = @CategoryID
      ,[QuantityPerUnit] = @QuantityPerUnit
      ,[UnitPrice] = @UnitPrice
      ,[UnitsInStock] = @UnitsInStock
      ,[UnitsOnOrder] = @UnitsOnOrder
      ,[ReorderLevel] = @ReorderLevel
      ,[Discontinued] = @Discontinued
 WHERE ProductID = @id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", product.Id));
                    command.Parameters.Add(new SqlParameter("@ProductName", product.Name));
                    command.Parameters.Add(new SqlParameter("@SupplierID", product.SupplierID == null ? DBNull.Value : (object)product.SupplierID));
                    command.Parameters.Add(new SqlParameter("@CategoryID", (object)product.CategoryID ?? DBNull.Value));        //stessa cosa di quello sopra ma + veloce
                    command.Parameters.Add(new SqlParameter("@QuantityPerUnit", (object)product.QuantityPerUnit ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", (object)product.Price ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UnitsInStock", (object)product.UnitsInStock ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UnitsOnOrder", (object)product.UnitsOnOrder ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@ReorderLevel", (object)product.ReorderLevel ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Discontinued", product.Discontinued));

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete product
        public void DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                string query = @"
DELETE FROM [dbo].[Products]
WHERE ProductID = @id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", productId));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
