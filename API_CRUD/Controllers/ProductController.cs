using API_CRUD.Data;
using API_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private ProductContext _pContext;
        public ProductController(ProductContext context)
        {
            _pContext = context;
        }

        [HttpGet]
        public List<Product> GetProducts()
        {

            return _pContext.products.ToList();
        }

        [HttpGet("{id}")]
        public Product GetProducts(int id)
        {
            var product = _pContext.products.Where(x => x.ProductID == id).SingleOrDefault();

            return product;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not A Valid Product");

            }
            _pContext.products.Add(product);
            _pContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _pContext.products.FindAsync(id);
            if (product==null)
            {
                return NotFound();
            }
            _pContext.products.Remove(product);
            await _pContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Product product)
        {
            var prod = await _pContext.products.FindAsync(id);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Quantity = product.Quantity;
                prod.Price = product.Price;

                await _pContext.SaveChangesAsync();
            }
            return View();
        }









        
    }
}
