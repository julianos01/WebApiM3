﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiM3.Context;
using WebApiM3.Entities;

namespace WebApiM3.Controllers
{
    [Route ("api/[Controller]")]
    [ApiController]
    public class LibrosController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(X => X.Autor).ToList();
        }


        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Autor> Get(int id)
        {
            var libro = context.Autores.SingleOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id },libro);
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }


            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {

            var libro = context.Libros.FirstOrDefault(x => x.Id == id);


            if (libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}
