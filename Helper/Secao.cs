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
            string? sessaoUsuario=_HttpContextAccessor.HttpContext!.Session.GetString("sessaoUsuarioLogado");
            if(string.IsNullOrEmpty(sessaoUsuario))return null;
        
           return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSecaoDoUsuario(UsuarioModel usuarioModel)
        {
            string? sessaoUsuario= JsonConvert.SerializeObject(usuarioModel);
            _HttpContextAccessor.HttpContext!.Session.SetString("sessaoUsuarioLogado",sessaoUsuario);
        }

        public void RemoverSecaoDoUsuario()
        {
            _HttpContextAccessor.HttpContext!.Session.Remove("sessaoUsuarioLogado");
        }
        
    }
}