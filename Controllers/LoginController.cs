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
        private readonly IEmail _email;
        public LoginController(
            ILogger<LoginController> logger,
            IUsuarioRepository usuarioRepository,
            ISecao secao,
            IEmail email
        )
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _secao = secao;
            _email = email;
        }

        public IActionResult Index()

        {
            if (_secao.BuscarSecaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }
        public IActionResult Sair()
        {
            _secao.RemoverSecaoDoUsuario();
            return RedirectToAction("Index", "Login");
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
                        Console.WriteLine("lOGIN: "+loginModel.Login);
                        if (usuarioModel.ValidarSenha(loginModel.Senha!))
                        {
                            _secao.CriarSecaoDoUsuario(usuarioModel);
                            var usuario = _secao.BuscarSecaoDoUsuario();
                            TempData["Menssagem-sucesso"] = $"Usuário logado com sucesso! " + usuario.Nome;
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
        public ActionResult RedefinirSenha()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> EnviarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel? usuarioModel = _usuarioRepository.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);
                    if (usuarioModel != null)
                    {
                        string novaSenha = usuarioModel.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuarioModel.Email, "Sistema de contatos - Nova senha", mensagem);
                        if (emailEnviado)
                        {

                             try
                             {
                                _usuarioRepository.Atualizar(usuarioModel);
                             }
                             catch (System.Exception)
                             {
                                
                                throw;
                             }

                             
                              
                            TempData["Menssagem-sucesso"] = "Enviamos para o email cadastrado uma nova senha!";
                        }
                        else
                        {
                            TempData["Menssagem-erro"] = "Não conseguimos enviar seu email. Por favor tente novamente";
                        }

                        return RedirectToAction("Index", "Login");
                    }
                    TempData["Menssagem-erro"] = "Não conseguimos redefinir sua senha. Por favor verifique os dados informados";
                }
                return RedirectToAction("RedefinirSenha", "Login");
            }
            catch (System.Exception erro)
            {

                TempData["Menssagem-erro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente detalhe do erro: {erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}