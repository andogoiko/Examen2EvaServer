using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cripto.Models;

namespace CriptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly CryptoContext db;

        public QueryController(CryptoContext context)
        {
            db = context;
        }

        [HttpGet("1")]
        public async Task<ActionResult> Query1(int ValorActual = 50)
        {
            // Ejemplo de método en controlador
            var list = await db.Moneda.Where(m => m.Actual > ValorActual).OrderBy(m => m.MonedaId).ToListAsync();

            return Ok(new
            {
                Id = 1,
                Descripcion = "Monedas con valor actual superior a 50€ ordenadas alfabéticamente",
                Data = list,
            });
        }

        [HttpGet("2")]
        public async Task<ActionResult> Query2()
        {
            // Ejemplo de método en controlador
            var list = await db.Contrato.GroupBy(c => c.CarteraId).Select(f => new
            {
                cartera = f.Key,
                total = f.Count()
            }).Where(f => f.total > 2).ToListAsync();

            return Ok(new
            {
                Id = 2,
                Descripcion = "Carteras con más de 2 monedas contratadas",
                Data = list,
            });
        }

        [HttpGet("3")]
        public async Task<ActionResult> Query3()
        {
            // Ejemplo de método en controlador
            var list = await db.Cartera.GroupBy(c => c.Exchange).Select(f => new
            {
                exchange = f.Key,
                carteras = f.Count()
            }).OrderByDescending(f => f.carteras).ToListAsync();

            return Ok(new
            {
                Id = 3,
                Descripcion = "Exchanges ordenados por números de carteras",
                Data = list,
            });
        }

        [HttpGet("4")]
        public async Task<ActionResult> Query4()
        {
            // Ejemplo de método en controlador
            var list = await db.Cartera.Where(c => true)
                            .SelectMany(c => c.Contratos, (c, con) => new
                            {
                                exchange = c.Exchange,
                                moneda = con.MonedaId

                            }).GroupBy(c => c.exchange).Select(f => new
                            {
                                exchange = f.Key,
                                TotalCarteras = f.Count()
                            }).OrderByDescending(f => f.TotalCarteras).ToListAsync();

            return Ok(new
            {
                Id = 4,
                Descripcion = "Exchanges ordenados por cantidad de monedas",
                Data = list,
            });
        }

        [HttpGet("5")]
        public async Task<ActionResult> Query5()
        {
            // Ejemplo de método en controlador
            var list = await db.Moneda.Where(m => true)
                .SelectMany(m => m.Contrataciones, (m, con) => new
                {
                    Moneda = m.MonedaId,
                    Contrato = con.ContratoId,
                    valorContrato = (m.Actual * con.Cantidad)
                }).OrderBy(m => m.Moneda).ToListAsync();

            return Ok(new
            {
                Id = 5,
                Descripcion = "Monedas en contratos ordenadas por valor total actual",
                Data = list,
            });
        }

        [HttpGet("6")]
        public async Task<ActionResult> Query6()
        {
            // Ejemplo de método en controlador
            var list = await db.Moneda.Where(m => true)
                .SelectMany(m => m.Contrataciones, (m, con) => new
                {
                    Moneda = m.MonedaId,
                    Contrato = con.ContratoId,
                    valorContrato = (m.Actual * con.Cantidad)
                }).OrderBy(m => m.Moneda).GroupBy(m => m.Moneda).Select(f => new
                {
                    Moneda = f.Key,
                    valorTotal = f.Sum(f => f.valorContrato)
                }).OrderByDescending(f => f.valorTotal).ToListAsync();

            return Ok(new
            {
                Id = 6,
                Descripcion = "Monedas en contratos ordenadas por valor actual total en todos los contratos",
                Data = list,
            });
        }

        [HttpGet("7")]
        public async Task<ActionResult> Query7()
        {
            // Ejemplo de método en controlador
            var list = await db.Moneda.Where(m => true)
                .SelectMany(m => m.Contrataciones, (m, con) => new
                {
                    Moneda = m.MonedaId,
                    Contrato = con.ContratoId,
                    valorContrato = (m.Actual * con.Cantidad)
                }).GroupBy(m => m.Moneda).Select(f => new
                {
                    Moneda = f.Key,
                    valorTotal = f.Sum(f => f.valorContrato),
                    Contratos = f.Count()
                }).OrderByDescending(f => f.Contratos).ToListAsync();

            return Ok(new
            {
                Id = 7,
                Descripcion = " Idem contando en cuantos contratos aparecen y ordenado por número de contratos",
                Data = list,
            });
        }
    }
}
