using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; //ControllerBase e api
using Microsoft.EntityFrameworkCore; //Include e FirstOrDefault // para usar Task Async

namespace CRUDAPI.Models
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly Contexto _contexto;

        public PessoaController(Contexto contexto)
        {
            _contexto = contexto; //injeção de dependencia
        }

        [HttpGet] // metodo assincrono para retornar lista de pessoas
        [Route("Listar")]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoasAsync()
        {
            return await _contexto.Pessoas.ToListAsync();
        }

        [HttpGet] // metodo assincrono para retornar uma pessoa
        [Route("buscar/{pessoaid}")]
        public async Task<ActionResult<Pessoa>> GetPessoaByIdAsync(int pessoaid)
        {
            //return await _contexto.Pessoas.FindAsync(pessoaid); //retorna null se nao encontrar
            Pessoa pessoa =
                await _contexto
                    .Pessoas
                    .FirstOrDefaultAsync(p => p.Id == pessoaid);

            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada");
            }

            return pessoa; //retorna a pessoa encontrada no banco de dados
        }

        [HttpPost] // metodo assincrono para adicionar uma pessoa
        [Route("Adicionar")]
        public async Task<ActionResult<Pessoa>> PostPessoaAsync(Pessoa pessoa)
        {
            try
            {
                await _contexto.Pessoas.AddAsync(pessoa); //adiciona a pessoa no banco de dados
                await _contexto.SaveChangesAsync(); //salva as alterações no banco de dados
                return Ok(pessoa); //retorna a pessoa adicionada no banco de dados
            }
            catch (Exception)
            {
                return BadRequest("Erro ao adicionar pessoa"); //retorna erro se nao conseguir adicionar a pessoa
            }
        }

        [HttpPut] // metodo assincrono para atualizar uma pessoa
        [Route("Atualizar")]
        public async Task<ActionResult<Pessoa>> PutPessoaAsync(Pessoa pessoa)
        {
            try
            {
                _contexto.Pessoas.Update (pessoa); //atualiza a pessoa no banco de dados
                await _contexto.SaveChangesAsync(); //salva as alterações no banco de dados
                return Ok(pessoa); //retorna a pessoa atualizada no banco de dados
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar pessoa"); //retorna erro se nao conseguir atualizar a pessoa
            }
        }

        [HttpDelete] // metodo assincrono para deletar uma pessoa
        [Route("Deletar/{pessoaid}")]
        public async Task<ActionResult<Pessoa>> DeletePessoaAsync(int pessoaid)
        {
            try
            {
                Pessoa pessoa =
                    await _contexto
                        .Pessoas
                        .FirstOrDefaultAsync(p => p.Id == pessoaid); //procura a pessoa no banco de dados
                _contexto.Pessoas.Remove (pessoa); //remove a pessoa do banco de dados
                await _contexto.SaveChangesAsync(); //salva as alterações no banco de dados
                return Ok(pessoa); //retorna a pessoa removida do banco de dados
            }
            catch (Exception)
            {
                return BadRequest("Erro ao deletar pessoa"); //retorna erro se nao conseguir deletar a pessoa
            }
        }
    }
}
