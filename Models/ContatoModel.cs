using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        public ContatoModel(int id, string nome, string email,string celular)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Celular = celular;
        }
        public ContatoModel()
        {
            
        }
        
    }
}