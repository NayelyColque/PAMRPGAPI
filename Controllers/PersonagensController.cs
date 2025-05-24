using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using Microsoft.EntityFrameworkCore;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class PersonagensController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonagensController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id) 
        {
            try
            {
                Personagem? p = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                if (p == null)
                    return NotFound(new { message = "Personagem não encontrado." });

                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get() 
        {
            try
            {
                List<Personagem> lista = await _context.TB_PERSONAGENS.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Personagem novoPersonagem) 
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new Exception ("Pontos de vida não podem ser maiores que 100.");
                }

                await _context.TB_PERSONAGENS.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();  

                return Ok(novoPersonagem);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem) 
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new System.Exception("Pontos de vida não podem ser maiores que 100.");
                }

                _context.TB_PERSONAGENS.Update(novoPersonagem);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                Personagem pRemover = await _context.TB_PERSONAGENS.FirstOrDefaultAsync(p => p.Id == id);

                _context.TB_PERSONAGENS.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(new { message = "Personagem removido com sucesso.", linhasAfetadas });
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
