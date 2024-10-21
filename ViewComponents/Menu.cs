using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ControleContatos.ViewComponents
{
    public class Menu:ViewComponent
    {
        public async Task<IViewComponentResult>InvokeAsync(){
            string? sessaoUsuario=HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return View();
            UsuarioModel? usuario=JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            return View(usuario);
        }    
    }
    
}