using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contatos.Models
{
    public class HomeModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public HomeModel()
        {
            Nome = string.Empty;  // Inicialize com uma string vazia
            Email = string.Empty; // Inicialize com uma string vazia
        }
    }
}