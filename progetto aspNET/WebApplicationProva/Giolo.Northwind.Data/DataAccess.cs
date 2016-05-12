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
    }
}
