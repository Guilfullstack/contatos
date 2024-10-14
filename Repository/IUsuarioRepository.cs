using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Models;

namespace ControleContatos.Repository
{
    public interface IUsuarioRepository
    {
        public UsuarioModel BuscarPorLogin(string login);
        public UsuarioModel AddUsuario(UsuarioModel usuario);
        public List<UsuarioModel> BuscarTodosUsuarios();
        public UsuarioModel BuscarPorId(int id);  
        public UsuarioModel Atualizar(UsuarioModel usuario);
        public  Task<bool> Deletar(int id);  
    }
}