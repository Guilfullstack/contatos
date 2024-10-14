using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ControleContatos.Helper;
using ControleContatos.Models;
using Newtonsoft.Json;

namespace ControleContatos.Helper
{
    public class Secao : ISecao
    {
        //Atraves do httpcontext consigo criar seção
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public Secao(IHttpContextAccessor contextAccessor)
        {
            _HttpContextAccessor = contextAccessor;
        }
        public UsuarioModel? BuscarSecaoDoUsuario()
        {
            string? secaoUsuario=_HttpContextAccessor.HttpContext!.Session.GetString("SecaoUsuarioLogado");
            if (secaoUsuario == null)return null;
        
           return JsonConvert.DeserializeObject<UsuarioModel>(secaoUsuario);
        }

        public void CriarSecaoDoUsuario(UsuarioModel usuarioModel)
        {
            string? secaoUsuario= JsonConvert.SerializeObject(usuarioModel);
            _HttpContextAccessor.HttpContext!.Session.SetString("SecaoUsuarioLogado",secaoUsuario);
        }

        public void RemoverSecaoDoUsuario()
        {
            _HttpContextAccessor.HttpContext!.Session.Remove("SecaoUsuarioLogado");
        }
    }
}