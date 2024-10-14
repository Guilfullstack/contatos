using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Models;

namespace ControleContatos.Helper
{
    public interface ISecao
    {
        public void CriarSecaoDoUsuario(UsuarioModel usuarioModel);
        public void RemoverSecaoDoUsuario();
        public UsuarioModel? BuscarSecaoDoUsuario();
    }
}