using Giolo.Northwind.Data;
using Giolo.Northwind.Data.ObjectModel;
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

        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            DataAccess data = new DataAccess();
            var product = data.GetProducts();

            if (product == null)
                return NotFound();

            ProductModel p = new ProductModel();
            //p.id = 

            return Ok(p);
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]ProductModel value)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();

                product.Name = value.Name;
                product.Price = value.Price;

                DataAccess data = new DataAccess();
                data.InsertProduct(product);

                return Ok();
            }
            else
            {
                //bad request --> 400
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]ProductModel value)
        {
            Product product = new Product();

            product.Id = id;
            product.Name = value.Name;
            product.Price = value.Price;

            DataAccess data = new DataAccess();
            data.UpdateProduct(product);

            return Ok();
        }

        // DELETE: api/Products/5
        public IHttpActionResult Delete(int id)
        {
            DataAccess data = new DataAccess();
            data.DeleteProduct(id);
            return Ok();
        }
    }
}
