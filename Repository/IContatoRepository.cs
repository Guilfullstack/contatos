using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Models;

namespace ControleContatos.Repository
{
    public interface IContatoRepository
    {
        public ContatoModel AddContato(ContatoModel contato);
        public List<ContatoModel> BuscarTodos();
        public ContatoModel BuscarContatoId(int id);
        public ContatoModel Atualizar(ContatoModel contato);
        public Task<bool> ApagarContato(int id);
    }
}