using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDAPI.Models
{
    public class Pessoa
    {


        public Pessoa () => CriadoEm = DateTime.Now;

        [Key]
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public DateTime CriadoEm { get; set; }


    }
}