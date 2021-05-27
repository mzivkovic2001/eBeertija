using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ebeertijaBackend.DatabaseContext;
using ebeertijaBackend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebeertijaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzvjestajController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BeertijaContext context;

        public IzvjestajController(IMapper mapper, BeertijaContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("{datum}")]
        [Authorize]
        public IActionResult GetRacuniIzvjestajNaOdabraniDatum(DateTime datum)
        {
            var zahtjevaniRacuni = context.Racuni.Where(r => r.Datum.Date == datum.Date).Include(s => s.StavkeRacuna).ThenInclude(scr => scr.StavkaCjenika)
                                                                                        .Include(n => n.Narudzba).ThenInclude(sn => sn.StavkeNarudzbe).ThenInclude(sc => sc.StavkaCjenika)
                                                                                        .Include(nr => nr.Narudzba).ThenInclude(s => s.Stol)
                                                                                        .Include(stol => stol.Stol).ToList();
            var mappiraniZahtjevi = mapper.Map<IEnumerable<RacunIzvjestajDto>>(zahtjevaniRacuni);

            foreach(var racun in mappiraniZahtjevi)
            {
                racun.BrojOpis = racun.Broj.ToString() + '/' + racun.Datum.Year.ToString();
                if(racun.NarudzbaId.HasValue)
                {
                    var oznakaStola = context.Stolovi.Where(s => s.SerijskiBrojUredaja == racun.Narudzba.SerijskiBrojUredaja).FirstOrDefault().OznakaStola;
                    racun.Narudzba.OznakaStola = oznakaStola;
                }

                var storno = context.Racuni.Where(r => r.StorniraniRacunId == racun.Id).FirstOrDefault();
                if(storno != null)
                {
                    racun.BrojStorniranogRacuna = storno.Broj.ToString() + '/' + storno.Datum.Year.ToString();
                }
            }

            return Ok(mappiraniZahtjevi.OrderByDescending(d => d.Datum));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRacuniIzvjestajNaDanasnjiDatum()
        {
            return GetRacuniIzvjestajNaOdabraniDatum(DateTime.Now);
        }
    }
}
