using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Data;
using RestaurantExtended.Models;
using RestaurantExtended.Services;

namespace RestaurantExtended.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "OneRolePolicy")]
    //[AllowAnonymous]
    public class ProdusController : ControllerBase
    {
     
        public IProductService _service;

        public ProdusController(IProductService productService)
        {
           
            _service = productService;
        }

        // GET: api/Produs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produs>>> GetProduct()
        {
            List<Produs> products = await _service.ListProducts();
            Debug.WriteLine("Nr produse:"+products.Count);
            foreach(var prod in products)
            {
                Debug.WriteLine(prod.name);
            }

            return products;
        }

        // GET: api/Produs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produs>> GetProdus(int id)
        {
            return await _service.getProduct(id);
        }

        // PUT: api/Produs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdus(int id, Produs produs)
        {
       
            produs.Id = id;

       

            await _service.editProduct(produs);


            return NoContent();
        }

        // POST: api/Produs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produs>> PostProdus(Produs produs)
        {
            /* if (_context.Product == null)
             {
                 return Problem("Entity set 'Context2.Product'  is null.");
             }
             */
            Debug.WriteLine("Intrat in produs");
         
            await _service.addProduct(produs);

            return CreatedAtAction("GetProdus", new { id = produs.Id }, produs);
        }

        // DELETE: api/Produs/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteProdus(int id)
        {
            await _service.deleteProduct(id);
            return NoContent();
        }
      


        [HttpPut("addImage/{id}")]
        public async Task<IActionResult> PutImage(int id, string image )
        {
           
          
            return NoContent();
        }
    }
}
