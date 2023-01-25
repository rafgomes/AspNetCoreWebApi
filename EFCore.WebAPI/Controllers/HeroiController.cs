using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Dominio;
using EFCore.Dominio.Interfaces;
using EFCore.Repo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly IHeroiServices heroiService;

        public HeroiController(IHeroiServices heroiService)
        {
            
            this.heroiService = heroiService;
        }
        // GET: api/Heroi
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await heroiService.GetAllHerois(true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        // GET: api/Heroi/5
        [HttpGet("{id}", Name = "GetHeroi")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Heroi heroi = await heroiService.GetHeroiById(id, true);

                return Ok(heroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST: api/Heroi
        [HttpPost]
        public async Task<IActionResult> Post(Heroi model)
        {
            try
            {
                string result = await heroiService.AddHeroi(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }            
        }

        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi model)
        {
            try
            {

                string result = await heroiService.UpdateHeroi(id, model);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await heroiService.DeleteHeroi(id);

                return Ok(result);
               
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }          
        }
    }
}
