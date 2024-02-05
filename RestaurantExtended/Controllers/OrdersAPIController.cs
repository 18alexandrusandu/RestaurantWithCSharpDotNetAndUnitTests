using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Controllers.dtos;
using RestaurantExtended.Data;
using RestaurantExtended.Models;
using RestaurantExtended.Services;
using ServiceStack;
using System.Diagnostics;

namespace RestaurantExtended.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class OrdersAPIController : ControllerBase
    {

        public IComenziService _service;

        public OrdersAPIController( IComenziService comenziService)
        {
           
            _service = comenziService;

        }
        [HttpGet("betweenDates")]
        public async Task<ActionResult<IEnumerable<Comanda>>> GetComandeziBetween2Date(DateTime start, DateTime end)
        {
          
            return _service.GetComenziByDate(start, end);
        }

        // POST: api/Produs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comanda>> PostComanda(Comanda comanda)
        {
          
             await _service.CreazaComanda(comanda);


            return CreatedAtAction("PostComanda", comanda);
        }


    



        [HttpGet("topComandate")]
        public async Task<ActionResult<List<RankedProduct>>> GetMostCommanded(DateTime start, DateTime end)
        {

            return await _service.GetProduseComandateByDate(start, end);


        }


       
    }

}


