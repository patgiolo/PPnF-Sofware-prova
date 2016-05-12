using Giolo.Northwind.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationProva.Models;

namespace WebApplicationProva.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        public IHttpActionResult Get()
        {
            DataAccess data = new DataAccess();
            var products = data.GetProducts();

            List<ProductModel> result = new List<ProductModel>();
            foreach (var item in products)
            {
                ProductModel p = new ProductModel();
                p.Id = item.Id;
                p.Name = item.Name;
                p.Price = item.Price;

                result.Add(p);
            }

            return Ok(result);
        }
    }
}
