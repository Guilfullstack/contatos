using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Data;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel AddUsuario(UsuarioModel usuario)
        {
            try
            {
                usuario.DataCadastro = DateTime.Now;
                _bancoContext.Usuarios.Add(usuario);
                _bancoContext.SaveChanges();
                return usuario;
            }
            catch (System.Exception erro)
            {
                throw new Exception($"Não foi possivel cadastrar o contato {erro.Message}");
            }
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            try
            {
                UsuarioModel dbUsuario = BuscarPorId(usuario.Id);
                if (dbUsuario == null) throw new Exception("Erro ao atualizar o contato");
                if (dbUsuario == usuario) return usuario;
                dbUsuario.Nome = usuario.Nome;
                dbUsuario.Email = usuario.Email;
                dbUsuario.Login = usuario.Login;
                dbUsuario.Perfil = usuario.Perfil;
                dbUsuario.DataAtualizacao = DateTime.Now;
                return dbUsuario;
            }
            catch (System.Exception erro)
            {
                throw new Exception($"Erro ao atualizar o contato {erro.Message}");
            }

        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModel> BuscarTodosUsuarios()
        {
            try
            {
                List<UsuarioModel> usuarioList = [];
                usuarioList = _bancoContext.Usuarios.ToList();
                return usuarioList;
            }
            catch (System.Exception erro)
            {
                
                throw new Exception($"Erro ao buscar lista de contatos: {erro.Message}");
            }
        }

        public UsuarioModel Deletar(int id)
        {
            try
            {
                UsuarioModel usuarioDb = BuscarPorId(id);
                if (usuarioDb == null) throw new Exception("Houve um erro na deleção do contato");
                _bancoContext.Usuarios.Remove(usuarioDb);
                return usuarioDb;
            }
            catch (System.Exception erro)
            {
                throw new Exception($"Erro ao deletar contato: {erro.Message}");
            }
        }
    }
}