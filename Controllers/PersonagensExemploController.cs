using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExemploController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(personagens);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(personagens.FirstOrDefault(pe => pe.Id == id));
        }

        [HttpGet("GetOrdenado")]
        public IActionResult GetOrdem()
        {
            List<Personagem> listaFinal = personagens.OrderBy(p => p.Nome).ToList();
            return Ok(listaFinal);
        }

        [HttpGet("GetContagem")]
        public IActionResult GetQuantidade()
        {
            return Ok("Quantidade de personagens: " + personagens.Count);
        }

        [HttpGet("GetSomaForca")]
        public IActionResult GetSomaForca()
        {
            return Ok(personagens.Sum(p => p.Forca));
        }

        [HttpGet("GetSemCavaleiro")]
        public IActionResult GetSemCavaleiro()
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro);
            return Ok(listaBusca);
        }

        [HttpGet("GetByNomeAproximado/{nome}")]
        public IActionResult GetByNomeAproximado(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Nome.Contains(nome));
            return Ok(listaBusca);
        }

        [HttpGet("GetByForca/{forca}")]
        public IActionResult GetByForca(int forca)
        {
            return Ok(personagens.FirstOrDefault(x => x.Forca == forca));
        }

        [HttpGet("GetRemovendoMago")]
        public IActionResult GetRemovendoMagos()
        {
            Personagem? pRemove = personagens.Find(p => p.Classe == ClasseEnum.Mago);

            if (pRemove != null)
            {
                personagens.Remove(pRemove);
                return Ok("Personagem removido: " + pRemove.Nome);
            }
            else
                return BadRequest("Nenhum personagem encontrado para remoção");
        }

        [HttpGet("GetByInteligencia/{valor}")]
        public IActionResult GetByInteligencia(int valor)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Inteligencia == valor);

            if (listaBusca.Count == 0)
                return BadRequest("Nenhum personagem encontrado");
            else
                return Ok(listaBusca);
        }

        [HttpGet("BuscaPorId/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(personagens.FirstOrDefault(fulano => fulano.Id == id));
        }

        [HttpPost]
        public IActionResult AddPersonagem(Personagem novoPersonagem)
        {
            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        //Metodo put
        [HttpPut]
        public IActionResult UpdatePersonagem(Personagem p)
        {
            Personagem personagemAlterado = personagens.FirstOrDefault(pers => pers.Id == p.Id);

            if (personagemAlterado == null)
                return NotFound("Personagem não encontrado para alteração.");

            personagemAlterado.Nome = p.Nome;
            personagemAlterado.PontosVida = p.PontosVida;
            personagemAlterado.Forca = p.Forca;
            personagemAlterado.Defesa = p.Defesa;
            personagemAlterado.Inteligencia = p.Inteligencia;
            personagemAlterado.Classe = p.Classe;

            return Ok(personagens);
        }

        //Metodo Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personagens.RemoveAll(pers => pers.Id == id);
            return Ok(personagens);
        }

        [HttpGet("GetByEnum/{enumId}")]
        public IActionResult GetByEnum(int enumId)
        {
            ClasseEnum enumDigitado = (ClasseEnum)enumId;
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe == enumDigitado);
            return Ok(listaBusca);
        }

    }
}