using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using sample_api.Interface;
using sample_api.Models;
using sample_api.Services;

namespace sample_api.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        IProductsReference _repository;
        public ProductsController( IProductsReference repository)
        {
            _repository =repository;
        }

        [HttpGet("GetProduct")]
        public IActionResult Get(string id)
        {
            Product product = _repository.GetById(id);
            
            if (product == null)
            {
                return NotFound();
            }
            Console.WriteLine(Ok(product));
            return Ok(product);
        }

        [HttpGet("GetAllProducts")]
        
        public IActionResult GetProducts()
        {
            IEnumerable<Product> AllProds = _repository.GetAllItems();
            return Ok(AllProds);
        }
        [HttpPost("AddProduct")]
        public IActionResult Post(Product product)
        {
            _repository.Add(product);

            // var response = Request.CreateResponse(HttpStatusCode.Created, product);
            // string uri = Url.Link("DefaultApi", new { id = product.Id });
            // response.Headers.Location = new Uri(uri);

            return CreatedAtAction("Get", new { id = product.Id  }, product);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            _repository.Remove(id);
            Product product = _repository.GetById(id);
            return Ok();
        }

    }
}