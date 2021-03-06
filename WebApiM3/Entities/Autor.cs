﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiM3.Helpers;

namespace WebApiM3.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayusculaAttribute]
        public String Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
