using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDAPI.Models
{

public class Imc{


    public Imc () => CriadoEm = DateTime.Now;

    [Key]
    public int? Id { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double Myimc { get; set; }
    public string Classificacao { get; set; }
    public DateTime CriadoEm { get; set; }


    public int PessoaId { get; set; }
    public Pessoa pessoa { get; set; }



}



}