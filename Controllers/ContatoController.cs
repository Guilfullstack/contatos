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
    //[Route("[controller]")]
    public class ContatoController : Controller
    {
        private readonly ILogger<ContatoController> _logger;
        //Inserir a dependencia 
        private readonly IContatoRepository _contatoRepository;
        public ContatoController(ILogger<ContatoController> logger, IContatoRepository contatoRepository)
        {
            _logger = logger;
            _contatoRepository = contatoRepository;
        }

        public IActionResult Index()
        {
            var listaContatos=_contatoRepository.BuscarTodos();
            return View(listaContatos);
        }
        public IActionResult Criar(){
            return View();
        }
        public IActionResult Editar(){
            return View();
        }
         public IActionResult ApagarConfirmacao(){
            return View();
        }
        //Metoto post para criar contato
        [HttpPost]
        public IActionResult Criar(ContatoModel contatoModel){
            _contatoRepository.AddContato(contatoModel);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}