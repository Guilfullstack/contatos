using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Data;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace ControleContatos.Repository
{
    public class ContatoRepository :  IContatoRepository
    {   
        //A classe precisa do Banco context para operar no banco de dados
        private readonly BancoContext _bancoContext;
        public ContatoRepository(BancoContext bancoContext)
        {
            _bancoContext=bancoContext;
        }
        public ContatoModel AddContato(ContatoModel contato)
        {
           _bancoContext.Add(contato);
           _bancoContext.SaveChanges();
           return contato;
        }

        public List<ContatoModel> BuscarTodos()
        {
             List<ContatoModel>contatoModelsList=[];
            contatoModelsList=_bancoContext.Contatos.ToList();
            return contatoModelsList;     
        }
    }
}