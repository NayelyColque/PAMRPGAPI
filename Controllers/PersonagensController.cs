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
                // Validação de pontos de vida
                if (novoPersonagem.PontosVida > 100)
                {
                    return BadRequest(new { message = "Pontos de vida não podem ser maiores que 100." });
                }

                await _context.TB_PERSONAGENS.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();  

                return CreatedAtAction(nameof(GetSingle), new { id = novoPersonagem.Id }, novoPersonagem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem) 
        {
            try
            {
                // Validação de pontos de vida
                if (novoPersonagem.PontosVida > 100)
                {
                    return BadRequest(new { message = "Pontos de vida não podem ser maiores que 100." });
                }

                _context.TB_PERSONAGENS.Update(novoPersonagem);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas == 0)
                {
                    return NotFound(new { message = "Personagem não encontrado para atualização." });
                }

                return Ok(new { message = "Personagem atualizado com sucesso.", linhasAfetadas });
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
                // Usando o método FindAsync corretamente
                Personagem pRemover = await _context.TB_PERSONAGENS.FindAsync(id);

                if (pRemover == null)
                {
                    return NotFound(new { message = "Personagem não encontrado para remoção." });
                }

                _context.TB_PERSONAGENS.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(new { message = "Personagem removido com sucesso.", linhasAfetadas });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
