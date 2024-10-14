using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControleContatos.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISecao _secao;
        public LoginController(
            ILogger<LoginController> logger, 
            IUsuarioRepository usuarioRepository,
            ISecao secao
        )
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _secao=secao;
        }

        public IActionResult Index()

        {
            if(_secao.BuscarSecaoDoUsuario()!=null)return RedirectToAction("Index","Home");
            return View();
        }
        public IActionResult Sair(){
            _secao.RemoverSecaoDoUsuario();
            return RedirectToAction("Index","Login");
        }
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel? usuarioModel = _usuarioRepository.BuscarPorLogin(loginModel.Login);
                    if (usuarioModel != null)
                    {
                        if (usuarioModel.ValidarSenha(loginModel.Senha!))
                        {
                            TempData["Menssagem-sucesso"] = $"Usuário logado com sucesso!";
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["Menssagem-erro"] = "Senha do usuário inválida!";
                    }
                    TempData["Menssagem-erro"] = "Usuario e/ou senha inválidos. Por favor tente novamente";
                }
                return View("Index");
            }
            catch (System.Exception erro)
            {

                TempData["Menssagem-erro"] = $"Ops, não conseguimos reslizar o seu login, tente novamente detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}