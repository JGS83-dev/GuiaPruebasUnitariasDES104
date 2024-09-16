using PersonasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PersonasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly PersonasDbContext _context;

        public PersonasController(PersonasDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            if (string.IsNullOrEmpty(persona.PrimerNombre) || string.IsNullOrEmpty(persona.PrimerApellido))
            {
                return BadRequest("El primer nombre y primer apellido no pueden estar vacios.");
            }

            if (persona.PrimerNombre.Length > 100 || persona.PrimerApellido.Length > 100)
            {
                return BadRequest("Los nombres y apellidos no deben exceder los 100 caracteres.");
            }

            if (!string.IsNullOrEmpty(persona.SegundoNombre))
            {
                if (persona.SegundoNombre.Length > 100)
                {
                    return BadRequest("Los nombres y apellidos no deben exceder los 100 caracteres.");
                }
            }

            if (!string.IsNullOrEmpty(persona.SegundoApellido))
            {
                if (persona.SegundoApellido.Length > 100)
                {
                    return BadRequest("Los nombres y apellidos no deben exceder los 100 caracteres.");
                }
            }

            if (persona.FechaNacimiento == null)
            {
                return BadRequest("La fecha de nacimiento es requerido.");
            }

            if (persona.FechaNacimiento == default(DateTime))
            {
                return BadRequest("La fecha de nacimiento es requerido.");
            }

            DateTime fechaMinima = new DateTime(1900, 1, 1);
            DateTime fechaActual = DateTime.Now;

            if (persona.FechaNacimiento > fechaActual)
            {
                return BadRequest("La fecha de nacimiento no puede ser mayor a la actul.");
            }

            if (persona.FechaNacimiento < fechaMinima)
            {
                return BadRequest("La fecha de nacimiento no puede ser anterior al 1 de enero de 1900.");
            }

            if (string.IsNullOrEmpty(persona.Dui))
            {
                return BadRequest("El DUI no puede estar vacio.");
            }

            if (!Regex.IsMatch(persona.Dui, @"^\d{8}-\d{1}$"))
            {
                return BadRequest("El formato debe ser XXXXXXXX-X, los cuales son digitos del 0 al 9");
            }

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        [HttpGet]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }
    }
}
