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
                usuario.SetSenhaHash();
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
                dbUsuario.Nome = usuario.Nome;
                dbUsuario.Email = usuario.Email;
                dbUsuario.Login = usuario.Login;
                dbUsuario.Perfil = usuario.Perfil;
                dbUsuario.DataAtualizacao = DateTime.Now;
                dbUsuario.Senha = usuario.Senha;
                _bancoContext.Usuarios.Update(dbUsuario);
                _bancoContext.SaveChanges();
                return dbUsuario;
            }
            catch (System.Exception erro)
            {
                throw new Exception($"Erro ao atualizar o contato {erro.Message}");
            }

        }

        public UsuarioModel AtualizarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioModel = BuscarPorId(alterarSenhaModel.Id);
            if (usuarioModel == null) throw new Exception("Houve um erro na atualização da senha, usuario não encontrado!");
            if (!usuarioModel.ValidarSenha(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");
            if (usuarioModel.ValidarSenha(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual");

            usuarioModel.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioModel.DataAtualizacao = DateTime.Now;
            _bancoContext.Usuarios.Update(usuarioModel);
            _bancoContext.SaveChanges();
            return usuarioModel;
        }

        public UsuarioModel? BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email == email.ToUpper());
        }

        public UsuarioModel? BuscarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());

        }

        public List<UsuarioModel> BuscarTodosUsuarios()
        {
            try
            {
                List<UsuarioModel> usuarioList = [];
                usuarioList = _bancoContext.Usuarios
                    .Include(x=>x.Contatos)
                    .ToList();
                return usuarioList;
            }
            catch (System.Exception erro)
            {

                throw new Exception($"Erro ao buscar lista de contatos: {erro.Message}");
            }
        }

        public async Task<bool> Deletar(int id)
        {
            UsuarioModel usuarioDb = BuscarPorId(id);
            if (usuarioDb == null) throw new Exception("Houve um erro na deleção do contato");
            _bancoContext.Usuarios.Remove(usuarioDb);
            _bancoContext.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}