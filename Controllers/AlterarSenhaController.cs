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
    //[Route("[controller]")]
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISecao _secao;
        public AlterarSenhaController(IUsuarioRepository usuarioRepository, ISecao secao)
        {
            _usuarioRepository = usuarioRepository;
            _secao = secao;
        }

        public IActionResult Index()

        {

            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                UsuarioModel usuarioLogado = _secao.BuscarSecaoDoUsuario();
                alterarSenhaModel.Id = usuarioLogado.Id;
                if (ModelState.IsValid)
                {

                    _usuarioRepository.AtualizarSenha(alterarSenhaModel);
                    Console.WriteLine(alterarSenhaModel.ToString());
                    TempData["Menssagem-sucesso"] = "Senha Alterada com sucesso";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);
            }
            catch (System.Exception error)
            {
                TempData["Menssagem-erro"] = $"Ops! não conseguimos redefinir a sua senha, tente novamente. detalhe do erro: {error.Message}";
                return View("Index", alterarSenhaModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}