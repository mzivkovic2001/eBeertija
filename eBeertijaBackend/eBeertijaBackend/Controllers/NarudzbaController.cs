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
    public class NarudzbaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BeertijaContext context;

        public NarudzbaController(IMapper mapper, BeertijaContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetNarudzbaById(int id)
        {
            var narudzba = context.Narudzbe.Where(n => n.Id == id).Include(s => s.StavkeNarudzbe).ThenInclude(sc => sc.StavkaCjenika)
                .Include(st => st.Stol).FirstOrDefault();

            var mappedNarudzba = mapper.Map<NarudzbaDto>(narudzba);

            return Ok(mappedNarudzba);
        }

        [HttpPost]
        public IActionResult CreateNarudzba(NarudzbaDto narudzba)
        {
            var narudzbe = context.Narudzbe.Where(x => x.IsActive && x.Datum.Year == DateTime.Now.Year).ToList();

            var broj = 0;
            if(narudzbe.Count > 0)
            {
                broj = narudzbe.Max(n => n.Broj);
                broj++;
            } else
            {
                broj = 1;
            }

            var stol = context.Stolovi.Where(s => s.SerijskiBrojUredaja == narudzba.SerijskiBrojUredaja).FirstOrDefault();
            if(stol == null)
            {
                return BadRequest("Neispravan serijski broj.");
            }

            if(stol.Narudzbe.Where(n => n.IsActive && !n.IsOstvarenaNaRacunu && n.Datum.Date == DateTime.Now.Date).Any())
            {
                return BadRequest("Pogreška! Na stolu već postoje narudžbe koje nisu izdane na računu.");
            }

            List<StavkaNarudzbe> stavkeNarudzbe = new List<StavkaNarudzbe>();
            double sveukupnoSaPdvom = 0;
            double sveukupnoBezPdva = 0;
            double pdvUkupno = 0;

            foreach(var stavka in narudzba.StavkeNarudzbe)
            {
                var odabranaStavkaCjenika = context.StavkeCjenika.Where(sc => sc.Id == stavka.StavkaCjenikaId).FirstOrDefault();
                sveukupnoBezPdva += (stavka.Kolicina * odabranaStavkaCjenika.CijenaBezPdva);
                sveukupnoSaPdvom += (stavka.Kolicina * odabranaStavkaCjenika.CijenaSaPdvom);
                pdvUkupno += (stavka.Kolicina * odabranaStavkaCjenika.PDV);
            }

            Narudzba newNarudzba = new Narudzba()
            {
                Broj = broj,
                Datum = DateTime.Now,
                IsActive = true,
                IsOstvarenaNaRacunu = false,
                PdvIznos = pdvUkupno,
                UkupnoBezPdva = sveukupnoBezPdva,
                UkupnoSaPdvom = sveukupnoSaPdvom,
                StolId = stol.Id
            };

            foreach (var stavka in narudzba.StavkeNarudzbe)
            {
                var odabranaStavkaCjenika = context.StavkeCjenika.Where(sc => sc.Id == stavka.StavkaCjenikaId).FirstOrDefault();
                StavkaNarudzbe newStavka = new StavkaNarudzbe()
                {
                    IsActive = true,
                    JedinicnaCijenaStavke = odabranaStavkaCjenika.CijenaSaPdvom,
                    Kolicina = stavka.Kolicina,
                    StavkaCjenikaId = stavka.StavkaCjenikaId,
                    Narudzba = newNarudzba,
                    Ukupno = stavka.Kolicina * odabranaStavkaCjenika.CijenaSaPdvom
                };
                stavkeNarudzbe.Add(newStavka);
            }

            context.Narudzbe.Add(newNarudzba);
            context.SaveChanges("anonim");
            context.AddRange(stavkeNarudzbe);
            context.SaveChanges("anonim");

            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult OdbijanjeNarudzbe(int id)
        {
            var narudzba = context.Narudzbe.Where(n => n.Id == id).FirstOrDefault();

            narudzba.IsActive = false;

            context.Narudzbe.Update(narudzba);
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            return Ok();
        }
    }
}
