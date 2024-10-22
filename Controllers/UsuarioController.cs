using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControleContatos.Controllers
{
    [PaginaRestritaSomenteAdm]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            List<UsuarioModel>? listUsuarios = _usuarioRepository.BuscarTodosUsuarios();

            return View(listUsuarios);
        }
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Menssagem-sucesso"] = $"Usuario {usuario.Nome} foi cadastrado com sucesso";
                    _usuarioRepository.AddUsuario(usuario);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(usuario);
                }
            }
            catch (System.Exception erro)
            {

                TempData["Menssagem-erro"] = $"Não foi possivel salvar o usuario: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
       public IActionResult Editar(int id){
            var usuarioModel=_usuarioRepository.BuscarPorId(id);
            UsuarioSemSenhaModel usuarioSemSenhaModel=new UsuarioSemSenhaModel(){
                        Id=usuarioModel.Id,
                        Nome=usuarioModel.Nome,
                        Login=usuarioModel.Login,
                        Email=usuarioModel.Email,
                        Perfil=usuarioModel.Perfil,
                    };
            return View(usuarioSemSenhaModel);
        }
        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioModel=new UsuarioModel(){
                        Id=usuarioSemSenhaModel.Id,
                        Nome=usuarioSemSenhaModel.Nome,
                        Login=usuarioSemSenhaModel.Login,
                        Email=usuarioSemSenhaModel.Email,
                        Perfil=usuarioSemSenhaModel.Perfil,

                    };
                    _usuarioRepository.Atualizar(usuarioModel);
                    TempData["Menssagem-sucesso"] = $"Usuário alterado com sucesso:";
                    return RedirectToAction("Index");
                }
                return View("Editar", usuarioSemSenhaModel);
            }
            catch (System.Exception erro)
            {
                TempData["Menssagem-erro"] = $"Não foi possivel alterar o contato: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _usuarioRepository.BuscarPorId(id);
            return View(usuario);
        }
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _usuarioRepository.Deletar(id);
                Console.WriteLine("Foi apagado:" + apagado);
                if (apagado)
                {
                    TempData["Menssagem-sucesso"] = "Usuario removido com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Menssagem-erro"] = $"Não foi possivel remover o usuario:";
                    return RedirectToAction("Index");
                }

            }
            catch (System.Exception erro)
            {
                TempData["Menssagem-erro"] = $"Não foi possivel apagar o usuário, mais detalhes do erro: {erro.Message}";
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