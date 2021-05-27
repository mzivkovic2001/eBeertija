using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ebeertijaBackend.DatabaseContext;
using ebeertijaBackend.DTOs;
using ebeertijaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebeertijaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CjenikController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BeertijaContext context;

        public CjenikController(IMapper mapper, BeertijaContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult GetCjenici()
        {
            var cjeniciList = context.Cjenici.Where(c => c.IsActive).Include(s => s.StavkeCjenika).ToList();

            var cjeniciListReturn = mapper.Map<IEnumerable<CjenikDto>>(cjeniciList);
            return Ok(cjeniciListReturn);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult CreateCjenik([FromBody] CjenikDto cjenik)
        {
            DateTime danasnjiDatum = DateTime.Now.Date;

            var cjenikPostojiPodUnesenimDatumom = context.Cjenici.Where(c => c.IsActive && c.DatumPocetkaPrimjene.Date == cjenik.DatumPocetkaPrimjene.Date).FirstOrDefault();

            if (cjenik.DatumPocetkaPrimjene.Date <= danasnjiDatum || cjenikPostojiPodUnesenimDatumom != null)
            {
                return BadRequest("Cjenik već postoji sa istim datumom primjene ili " +
                    "cjenik koji unosite ima datum primjene koji je manji ili jednak današnjem datumu.");
            }
            else
            {
                var noviCjenik = new Cjenik()
                {
                    DatumPocetkaPrimjene = cjenik.DatumPocetkaPrimjene.ToLocalTime().Date
                };
                context.Cjenici.Add(noviCjenik);
                context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
                var noviCjenikToReturn = mapper.Map<CjenikDto>(noviCjenik);
                return Ok(noviCjenikToReturn);
            }
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult UpdateCjenik(CjenikDto cjenikForUpdateDto)
        {
            DateTime danasnjiDatum = DateTime.Now.Date;
            var cjenik = context.Cjenici.Where(c => c.Id == cjenikForUpdateDto.Id && c.IsActive == true).FirstOrDefault();

            if (cjenik.DatumPocetkaPrimjene <= danasnjiDatum)
            {
                return BadRequest("Ne mozete uređivati cjenik koji je na snazi.");
            }

            cjenik.DatumPocetkaPrimjene = cjenikForUpdateDto.DatumPocetkaPrimjene.ToLocalTime().Date;

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            var azuriraniCjenikToReturn = mapper.Map<CjenikDto>(cjenik);
            return Ok(azuriraniCjenikToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult ObrisiCjenik(int id)
        {
            DateTime danasnjiDatum = DateTime.Now.Date;
            var cjenik = context.Cjenici.Where(c => c.Id == id && c.IsActive == true).FirstOrDefault();

            if (cjenik.DatumPocetkaPrimjene <= danasnjiDatum)
            {
                return BadRequest("Ne mozete obrisati cjenik koji je na snazi.");
            }

            cjenik.IsActive = false;

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            return Ok();
        }

    }
}
