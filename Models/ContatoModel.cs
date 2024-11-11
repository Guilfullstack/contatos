using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ControleContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome não informado")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Email não informado")]
        [EmailAddress(ErrorMessage ="Email inválido!")]
        public string Email { get; set; }
        // public DateTime? DataCadastro { get; set; }

        [Required(ErrorMessage ="Telefone não informado")]
        [Phone(ErrorMessage ="Numero de celular inválido")]
        public string Celular { get; set; }
        public int? UsuarioId { get; set; } 
        public UsuarioModel Usuario { get; set; }
        
    }
}

