using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiM3.Context;
using WebApiM3.Entities;
using WebApiM3.Helpers;

namespace WebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autores.Include(X => X.Libros).ToList();
        }

        [HttpGet("primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores.FirstOrDefault();  
        }


        //End point con respuesta asíncrona
        //Get api/autores/5
        [HttpGet("{id}",Name ="ObtenerAutor")]
        public async Task < ActionResult<Autor>> Get(int id)
        {
            var autor =await context.Autores.Include(X=>X.Libros).SingleOrDefaultAsync(x => x.Id == id);

            if(autor==null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpPost]
        public ActionResult Post ([FromBody]Autor autor)
        {
            PrimeraLetraMayusculaAttribute obj = new PrimeraLetraMayusculaAttribute();

           
            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor",new {id=autor.Id },autor);
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Autor value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }


            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult <Autor> Delete ( int id)
        {

            var autor = context.Autores.FirstOrDefault(x => x.Id == id);


                if (autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }

    }
}
