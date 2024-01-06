using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternAccess : ControllerBase
    {

        [HttpGet("gerar")]
        public IEnumerable<GerarPessoaResponse> GerarPessoas([FromQuery] int quantidade)
        {
            List<GerarPessoaResponse> pessoas = new List<GerarPessoaResponse>();
            for (int i = 0; i < quantidade; i++)
            {
                pessoas.Add(GerarPessoa());
            }

            return pessoas;
        }

        private GerarPessoaResponse GerarPessoa()
        {
            Random random = new Random();

            int idadeAleatoria = random.Next(18, 60);
            string nomeGerado = GerarNomeAleatorio();

            return new GerarPessoaResponse
            {
                Nome = nomeGerado,
                Idade = idadeAleatoria
            };
        }

        private string GerarNomeAleatorio()
        {
            string[] vogais = { "a", "e", "i", "o", "u" };
            string[] consoantes = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "qu", "r", "s", "t", "v", "x", "z" };

            Random random = new Random();
            int tamanhoNome = random.Next(3, 8);

            string nomeGerado = "";

            for (int i = 0; i < tamanhoNome; i++)
            {
                if (i % 2 == 0)
                {
                    nomeGerado += vogais[random.Next(vogais.Length)];
                }
                else
                {
                    nomeGerado += consoantes[random.Next(consoantes.Length)];
                }
            }

            return nomeGerado;
        }

        public class GerarPessoaResponse
        {
            public string Nome { get; set; }
            public int Idade { get; set; }
        }
    }
}
