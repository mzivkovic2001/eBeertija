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

namespace ebeertijaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StavkaCjenikaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BeertijaContext context;

        public StavkaCjenikaController(IMapper mapper, BeertijaContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetStavkeCjenikaNaSnazi()
        {
            DateTime danasnjiDatum = DateTime.Now.Date;

            var cjenikNaSnazi = context.Cjenici.Where(c => c.IsActive == true && c.DatumPocetkaPrimjene.Date <= danasnjiDatum).OrderByDescending(c => c.DatumPocetkaPrimjene).FirstOrDefault();

            var stavke = context.StavkeCjenika.Where(s => s.IsActive && s.CjenikId == cjenikNaSnazi.Id).ToList();

            var stavkeToReturn = mapper.Map<IEnumerable<StavkaCjenikaDto>>(stavke);

            return Ok(stavkeToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult DodajStavkuCjenika(StavkaCjenikaDto stavka)
        {
            StavkaCjenika novaStavka = new StavkaCjenika()
            {
                CjenikId = stavka.CjenikId,
                Kategorija = stavka.Kategorija,
                CijenaBezPdva = stavka.CijenaBezPdva,
                CijenaSaPdvom = stavka.CijenaSaPdvom,
                PDV = stavka.PDV,
                Opis = stavka.Opis,
                IsActive = true,
                Naziv = stavka.Naziv
            };

            context.StavkeCjenika.Add(novaStavka);

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            var returnStavka = context.StavkeCjenika.Where(c => c.Id == novaStavka.Id).FirstOrDefault();
            var novaStavkaToReturn = mapper.Map<StavkaCjenikaDto>(returnStavka);
            return Ok(novaStavkaToReturn);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult UpdateStavke(StavkaCjenikaDto stavka)
        {
            var stavkaZaUpdate = context.StavkeCjenika.Where(s => s.Id == stavka.Id && s.IsActive).FirstOrDefault();

            stavkaZaUpdate.Kategorija = stavka.Kategorija;
            stavkaZaUpdate.CijenaBezPdva = stavka.CijenaBezPdva;
            stavkaZaUpdate.CijenaSaPdvom = stavka.CijenaSaPdvom;
            stavkaZaUpdate.PDV = stavka.PDV;
            stavkaZaUpdate.Opis = stavka.Opis;
            stavkaZaUpdate.Naziv = stavka.Naziv;

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            var novaStavka = context.StavkeCjenika.Where(c => c.Id == stavka.Id).FirstOrDefault();
            var novaStavkaToReturn = mapper.Map<StavkaCjenikaDto>(novaStavka);
            return Ok(novaStavkaToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult ObrisiStavku(int id)
        {
            var stavkaZaBrisati = context.StavkeCjenika.Where(s => s.Id == id).FirstOrDefault();
            stavkaZaBrisati.IsActive = false;

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            return Ok();
        }

    }
}
