using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmasController : ControllerBase
    {
        private readonly DataContext _context;

        public ArmasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Arma>> Get()
        {
            return _context.TB_ARMAS.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Arma> GetById(int id)
        {
            var arma = _context.TB_ARMAS.Find(id);
            if (arma == null) return NotFound();
            return arma;
        }

        [HttpPost]
        public ActionResult<Arma> Post(Arma novaArma)
        {
            _context.TB_ARMAS.Add(novaArma);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = novaArma.Id }, novaArma);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Arma armaAtualizada)
        {
            var arma = _context.TB_ARMAS.Find(id);
            if (arma == null) return NotFound();

            arma.Nome = armaAtualizada.Nome;
            arma.Dano = armaAtualizada.Dano;

            _context.TB_ARMAS.Update(arma);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var arma = _context.TB_ARMAS.Find(id);
            if (arma == null) return NotFound();

            _context.TB_ARMAS.Remove(arma);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
