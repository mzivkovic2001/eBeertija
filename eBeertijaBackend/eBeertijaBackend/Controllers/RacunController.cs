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
    public class RacunController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BeertijaContext context;

        public RacunController(IMapper mapper, BeertijaContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("{stolId}")]
        [Authorize]
        public IActionResult GetRacunWindow(int stolId)
        {
            NewRacunDto noviRacun = new NewRacunDto()
            {
                StavkeCjenikaNaSnazi = new List<StavkaCjenikaDto>(),
                StavkeRacuna = new List<StavkaRacunaDto>()
            };

            var odabraniStol = context.Stolovi.Where(i => i.Id == stolId).FirstOrDefault();
            noviRacun.StolId = odabraniStol.Id;
            noviRacun.OznakaStola = odabraniStol.OznakaStola;

            var maxBroj = 0;
            var racuniUGodini = context.Racuni.Where(r => r.IsActive && r.Datum.Year == DateTime.Now.Year).ToList();
            if(racuniUGodini.Count > 0)
            {
                maxBroj = racuniUGodini.Max(b => b.Broj) + 1;
            } else
            {
                maxBroj = 1;
            }
                noviRacun.Broj = maxBroj;

            var cjenik = context.Cjenici.Where(c => c.IsActive && c.DatumPocetkaPrimjene.Date <= DateTime.Now.Date).Include(s => s.StavkeCjenika)
                .OrderByDescending(d => d.DatumPocetkaPrimjene).FirstOrDefault();

            noviRacun.StavkeCjenikaNaSnazi = mapper.Map<List<StavkaCjenikaDto>>(cjenik.StavkeCjenika);
            noviRacun.BrojOpis = maxBroj.ToString() + '/' + DateTime.Now.Year.ToString();

            return Ok(noviRacun);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateRacun([FromBody]RacunDto racun)
        {
            var racunToCreate = mapper.Map<Racun>(racun);

            if(racun.NarudzbaId.HasValue)
            {
                var narudzba = context.Narudzbe.Where(n => n.Id == racun.NarudzbaId).FirstOrDefault();
                narudzba.IsOstvarenaNaRacunu = true;
                context.Narudzbe.Update(narudzba);
            }

            racunToCreate.StavkeRacuna = mapper.Map<ICollection<StavkaRacuna>>(racun.StavkeRacuna);

            // za atribute racuna
            double sveukupnoBezPdva = 0;
            double sveukupnoSaPdvom = 0;
            double ukupnoPdv = 0;

            foreach(var stavka in racunToCreate.StavkeRacuna)
            {
                var stavkaCjenika = context.StavkeCjenika.Where(s => s.Id == stavka.StavkaCjenikaId).FirstOrDefault();
                stavka.JedinicnaCijenaStavke = stavkaCjenika.CijenaSaPdvom;
                stavka.UkupnoSaPdvom = stavkaCjenika.CijenaSaPdvom * stavka.Kolicina;
                stavka.UkupnoBezPdva = stavkaCjenika.CijenaBezPdva * stavka.Kolicina;
                stavka.IznosPdv = stavkaCjenika.PDV * stavka.Kolicina;

                sveukupnoBezPdva += stavka.UkupnoBezPdva;
                sveukupnoSaPdvom += stavka.UkupnoSaPdvom;
                ukupnoPdv += stavka.IznosPdv;
            }
            var dateTime = DateTime.Now;
            racunToCreate.Datum = dateTime;
            racunToCreate.IsStorniran = false;
            racunToCreate.UserId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            racunToCreate.NazivKorisnika = context.Users.Where(u => u.Id == racunToCreate.UserId).FirstOrDefault().FullName;
            racunToCreate.UkupnoBezPdva = sveukupnoBezPdva;
            racunToCreate.UkupnoSaPdvom = sveukupnoSaPdvom;
            racunToCreate.IznosPdv = ukupnoPdv;

            context.Racuni.Add(racunToCreate);
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult StornoRacuna(int id)
        {
            var racunZaStornirati = context.Racuni.Where(r => r.Id == id && r.IsStorniran == false && r.StorniraniRacunId == null && r.Datum.Date == DateTime.Now.Date).Include(s => s.StavkeRacuna)
                .FirstOrDefault();

            if(racunZaStornirati ==  null)
            {
                return BadRequest("Ne možete stornirati račun ili je prošao rok za storniranje računa");
            }

            racunZaStornirati.IsStorniran = true;
            context.Racuni.Update(racunZaStornirati);
            var broj = context.Racuni.Where(r => r.IsActive && r.Datum.Year == DateTime.Now.Year).Max(b => b.Broj);
             var userId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
             var nazivKorisnika = context.Users.Where(u => u.Id == userId).FirstOrDefault().FullName;

            Racun stornoRacun = new Racun()
            {
                Broj = broj + 1,
                Datum = DateTime.Now,
                IsStorniran = false,
                IsActive = true,
                IznosPdv = racunZaStornirati.IznosPdv * (-1),
                NarudzbaId = racunZaStornirati.NarudzbaId,
                StolId = racunZaStornirati.StolId,
                StorniraniRacunId = racunZaStornirati.Id,
                UkupnoBezPdva = racunZaStornirati.UkupnoBezPdva * (-1),
                UkupnoSaPdvom = racunZaStornirati.UkupnoSaPdvom * (-1),
                UserId = userId,
                NazivKorisnika = nazivKorisnika
            };

            context.Racuni.Add(stornoRacun);
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            List<StavkaRacuna> stavke = new List<StavkaRacuna>();

            foreach(StavkaRacuna stavka in racunZaStornirati.StavkeRacuna)
            {
                StavkaRacuna stornoStavka = new StavkaRacuna()
                {
                    IsActive = true,
                    IznosPdv = stavka.IznosPdv * (-1),
                    JedinicnaCijenaStavke = stavka.JedinicnaCijenaStavke * (-1),
                    Kolicina = stavka.Kolicina,
                    Racun = stornoRacun,
                    StavkaCjenikaId = stavka.StavkaCjenikaId,
                    UkupnoBezPdva = stavka.UkupnoBezPdva * (-1),
                    UkupnoSaPdvom = stavka.UkupnoSaPdvom * (-1)
                };
                stavke.Add(stornoStavka);
            }

            context.StavkeRacuna.AddRange(stavke);
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            return Ok();
        }

        [HttpPost("narudzba/{id}")]
        public IActionResult KreirajRacunNarudzbe(int id, [FromBody]NarudzbaDto narudzbaDto)
        {
            var narudzba = context.Narudzbe.Where(n => n.Id == id && n.IsActive && !n.IsOstvarenaNaRacunu)
                .Include(s => s.StavkeNarudzbe).ThenInclude(sc => sc.StavkaCjenika).FirstOrDefault();
            var broj = context.Racuni.Where(r => r.IsActive && r.Datum.Year == DateTime.Now.Year).Max(b => b.Broj);
            var userId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var nazivKorisnika = context.Users.Where(u => u.Id == userId).FirstOrDefault().FullName;

            List<StavkaRacuna> stavkeRacuna = new List<StavkaRacuna>();

            Racun newRacun = new Racun()
            {
                Broj = broj + 1,
                Datum = DateTime.Now,
                IsActive = true,
                IsStorniran = false,
                NarudzbaId = narudzba.Id,
                StolId = narudzba.StolId,
                NazivKorisnika = nazivKorisnika,
                UserId = userId,
                UkupnoSaPdvom = narudzba.UkupnoSaPdvom,
                UkupnoBezPdva = narudzba.UkupnoBezPdva,
                IznosPdv = narudzba.PdvIznos
            };

            foreach(var stavka in narudzba.StavkeNarudzbe)
            {
                StavkaRacuna newStavka = new StavkaRacuna()
                {
                    IsActive = true,
                    JedinicnaCijenaStavke = stavka.JedinicnaCijenaStavke,
                    IznosPdv = stavka.Kolicina * stavka.StavkaCjenika.PDV,
                    StavkaCjenikaId = stavka.StavkaCjenikaId,
                    Racun = newRacun,
                    Kolicina = stavka.Kolicina,
                    UkupnoBezPdva = stavka.StavkaCjenika.CijenaBezPdva * stavka.Kolicina,
                    UkupnoSaPdvom = stavka.StavkaCjenika.CijenaSaPdvom * stavka.Kolicina
                };

                stavkeRacuna.Add(newStavka);
            }

            narudzba.IsOstvarenaNaRacunu = true;
            context.Narudzbe.Update(narudzba);
            context.Racuni.Add(newRacun);
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            context.StavkeRacuna.AddRange(stavkeRacuna);
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            return Ok();
        }

    }
}
