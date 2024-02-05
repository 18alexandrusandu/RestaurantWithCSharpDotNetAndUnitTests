using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Implementation;
using NuGet.Protocol;
using RestaurantExtended.Controllers.dtos;
using RestaurantExtended.Data;
using RestaurantExtended.Models;
using RestaurantExtended.Services;
using RestaurantExtended.Services.implementations;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RestaurantExtended.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "OneRolePolicy")]
    //[AllowAnonymous]
    public class ComenziController : ControllerBase
    {
        
        public IComenziService _service;

        public ComenziController(IComenziService comenziService)
        {
           
            _service = comenziService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comanda>>> GetComanda()
        {
          
            return await _service.ListAll();
        }







        // GET: api/Comenzi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comanda>> GetComanda(int id)
        {
           
            var comanda = await _service.GetComanda(id);

            if (comanda == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Comanda" + comanda.ToJson());
            return comanda;
        }

        // PUT: api/Produs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComanda(int id, Comanda dto)
        {

            dto.Id = id;
            await _service.UpdateComanda(dto);

            return NoContent();
        }

        // POST: api/Produs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteComanda(int id)
        { 

            await _service.DeleteComanda(id);

            return NoContent();
        }

   




        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("export/{type}")]
        public async Task<ActionResult<ExportResultDto>> Export(int type,ExportDto exp)
        {

            List<Object> objs = new List<Object>();

             List<Comanda>  comenzi=_service.GetComenziByDate(exp.start, exp.end);
              List<RankedProduct> produse=   await _service.GetProduseComandateByDate(exp.start, exp.end);
           foreach(Comanda item in comenzi)
            {
                objs.Add(item.ToJson());
            }
            foreach (RankedProduct item in produse)
            {
                objs.Add(item.ToJson());
            }
            Debug.WriteLine(objs.Count(), objs.ToArray().ToString());
            return _service.export(type,exp.path, objs);
         
            
        }




    }

     





}

