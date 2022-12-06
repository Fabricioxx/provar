using System;
using System.Collections.Generic;
using System.Linq;
using CRUDAPI.Models;
using CRUDAPI.Utils;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;


namespace CRUDAPI.Controllers
{

    [ApiController]
    [Route("api/imc")]
    public class ImcController : ControllerBase
    {

        private readonly Contexto _context;
        public ImcController(Contexto context) 
        {
            _context = context; 
        }




        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Imc imc)
        {            


          double B = 0.0;
          string A = "";

            B = CalculoImc.CalcularImc(imc.Altura,imc.Peso);

            imc.Myimc = B;

            A = CalculoImc.classificar(imc.Myimc);

            imc.Classificacao = A;
            
            // folha.Funcionario = 
                // _context.Funcionarios.Find(folha.FuncionarioId);


            _context.Imcs.Add(imc);
            _context.SaveChanges();
            return Created("", imc);
        }






        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() 
        {
            
            List<Imc> Imcs = _context.Imcs.Include(p => p.pessoa).ToList();

            if(Imcs.Count == 0) //Cont - conta a quantidade de elementos na lista
            {
                return NotFound("Nenhuma folha de pagamento encontrada");
            }

            return Ok(Imcs); 
        }



        [HttpPatch]
        [Route("alterar")]
        public IActionResult Alterar([FromBody] Imc imc)
        {

            try
            {
                _context.Imcs.Update(imc); // Atualiza a folha de pagamento no banco de dados
                _context.SaveChanges();// Salva as alterações no banco de dados

                return Ok(imc); // Ok é um método que retorna um status 200
            }
            catch (Exception )
            {
                return BadRequest("Erro ao alterar a folha de pagamento"); // BadRequest é um método que retorna um status 400
            }
        }

       





        
    }
}