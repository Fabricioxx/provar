using System;
using Microsoft.EntityFrameworkCore; // DbContext

namespace CRUDAPI.Models
{
    public class Contexto : DbContext
    {

        public DbSet<Pessoa> Pessoas{ get; set; } //propriedade do tipo DbSet do tipo Pessoa (tabela)

        public DbSet<Imc> Imcs{get; set; }




          //contrutor
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {
        }
       

        
    }
}