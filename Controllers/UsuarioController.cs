using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControleContatos.Controllers
{
    // [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository=usuarioRepository;
        }

        public IActionResult Index()
        {
            List<UsuarioModel>? listUsuarios= _usuarioRepository.BuscarTodosUsuarios();

            return View(listUsuarios);
        }
        public IActionResult Criar(){
            return View();
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario){
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Menssagem-sucesso"]=$"Usuario {usuario.Nome} foi cadastrado com sucesso";
                    _usuarioRepository.AddUsuario(usuario);
                    return RedirectToAction("Index");
                }else{
                    return View(usuario);
                }
            }
            catch (System.Exception erro)
            {
                
                TempData["Menssagem-erro"] = $"Não foi possivel salvar o usuario: {erro.Message}";
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