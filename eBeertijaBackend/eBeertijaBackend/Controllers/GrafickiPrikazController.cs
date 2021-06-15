using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class GrafickiPrikazController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BeertijaContext context;

        public GrafickiPrikazController(IMapper mapper, BeertijaContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetGrafickiPrikazStolova()
        {
            var stolovi = context.Stolovi.Where(s => s.IsActive)
                .OrderBy(s => s.Narudzbe.Where(n => n.IsActive && !n.IsOstvarenaNaRacunu && n.Datum.Date == DateTime.Now.Date).FirstOrDefault().Datum).ToList();
            var narudzbe = context.Narudzbe.Where(n => n.IsActive && !n.IsOstvarenaNaRacunu && n.Datum.Date == DateTime.Now.Date).ToList();

            var grafickiPrikaz = new GrafickiPrikazDto
            {
                AktivniStolovi = new List<StolDto>(),
                ObrisaniStolovi = new List<StolDto>()
            };
            var prioritet = 0;
            foreach (var stol in stolovi)
            {
                var narudzbaStola = narudzbe.Where(n => n.StolId == stol.Id).FirstOrDefault();
                if (narudzbaStola != null)
                {
                    prioritet++;
                    StolDto stolDto = new StolDto()
                    {
                        Id = stol.Id,
                        NarudzbaId = narudzbaStola.Id,
                        IsActive = true,
                        Orientation = stol.Orientation,
                        OznakaStola = stol.OznakaStola,
                        PrioritetNarudzbe = prioritet,
                        SerijskiBrojUredaja = stol.SerijskiBrojUredaja,
                        X = stol.X,
                        Y = stol.Y,
                        StolClass = "btn btn-warning"
                    };
                    grafickiPrikaz.AktivniStolovi.Add(stolDto);
                } else
                {
                    StolDto stolDto = new StolDto()
                    {
                        Id = stol.Id,
                        IsActive = true,
                        Orientation = stol.Orientation,
                        OznakaStola = stol.OznakaStola,
                        SerijskiBrojUredaja = stol.SerijskiBrojUredaja,
                        X = stol.X,
                        Y = stol.Y,
                        StolClass = "btn btn-success"
                    };
                    grafickiPrikaz.AktivniStolovi.Add(stolDto);
                }
                
            }

            return Ok(grafickiPrikaz);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult DodajStol(StolDto stol)
        {
            Stol noviStol = new Stol()
            {
                OznakaStola = stol.OznakaStola,
                IsActive = true,
                Orientation = stol.Orientation,
                X = stol.X,
                Y = stol.Y,
                SerijskiBrojUredaja = stol.SerijskiBrojUredaja
            };
            context.Add(noviStol);

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            var uneseniStol = context.Stolovi.Where(x => x.Id == noviStol.Id && x.IsActive).FirstOrDefault();

            var uneseniStolToReturn = mapper.Map<StolDto>(uneseniStol);
            uneseniStolToReturn.StolClass = "btn btn-success";
            return Ok(uneseniStolToReturn);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        public IActionResult SaveGrafickiPrikaz([FromBody]GrafickiPrikazDto grafickiPrikaz)
        {
            foreach (StolDto stol in grafickiPrikaz.AktivniStolovi)
            {
                if (stol.Id > 0)
                {
                    Stol stolDb = context.Stolovi.Where(s => s.Id == stol.Id).FirstOrDefault();

                    stolDb.X = stol.X;
                    stolDb.Y = stol.Y;
                    stolDb.Orientation = stol.Orientation;
                    stolDb.OznakaStola = stol.OznakaStola;
                    stolDb.SerijskiBrojUredaja = stol.SerijskiBrojUredaja;
                }
                else
                {
                    Stol stolDb = new Stol();
                    mapper.Map(stol, stolDb);
                    context.Stolovi.Add(stolDb);
                }
            }

            //pobrisani stolovi foreach da ide po bazi , sprema eventualne izmjene i stavlja na isactive false
            foreach (StolDto stol in grafickiPrikaz.ObrisaniStolovi)
            {
                if (stol.Id > 0)
                {
                    Stol stolDb = context.Stolovi.Where(s => s.Id == stol.Id).FirstOrDefault();

                    stolDb.X = stol.X;
                    stolDb.Y = stol.Y;
                    stolDb.Orientation = stol.Orientation;
                    stolDb.OznakaStola = stol.OznakaStola;
                    stolDb.SerijskiBrojUredaja = stol.SerijskiBrojUredaja;
                    stolDb.IsActive = false;
                }
            }

            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            return GetGrafickiPrikazStolova();
        }



    }
}
