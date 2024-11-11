using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleContatos.Filters;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace ControleContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly ILogger<ContatoController> _logger;
        private readonly ISecao _secao;
        //Inserir a dependencia 
        private readonly IContatoRepository _contatoRepository;
        public ContatoController(ILogger<ContatoController> logger, IContatoRepository contatoRepository,ISecao secao)
        {
            _logger = logger;
            _contatoRepository = contatoRepository;
            _secao=secao;
        }

        public IActionResult Index()
        {
            var usuario=_secao.BuscarSecaoDoUsuario();
            List<ContatoModel> listaContatos = _contatoRepository.BuscarTodos(usuario.Id);
            return View(listaContatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            var contato = _contatoRepository.BuscarContatoId(id);
            return View(contato);
        }
        //Metoto post para criar contato
        [HttpPost]
        public IActionResult Criar(ContatoModel contatoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Menssagem-sucesso"] = $"Contato {contatoModel.Nome} salvo com sucesso:";
                     var usuario=_secao.BuscarSecaoDoUsuario();
                     contatoModel.UsuarioId=usuario.Id;
                    _contatoRepository.AddContato(contatoModel);
                    return RedirectToAction("Index");
                }
                return View(contatoModel);

            }
            catch (System.Exception erro)
            {
                TempData["Menssagem-erro"] = $"Não foi possivel salvar o contato: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Menssagem-sucesso"] = $"Contato alterado com sucesso:";
                     var usuario=_secao.BuscarSecaoDoUsuario();
                     contato.UsuarioId=usuario.Id;
                    _contatoRepository.Atualizar(contato);
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (System.Exception erro)
            {
                TempData["Menssagem-erro"] = $"Não foi possivel alterar o contato: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepository.BuscarContatoId(id);
            return View(contato);
        }
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _contatoRepository.ApagarContato(id);
                Console.WriteLine("Foi apagado:"+ apagado);
                if (apagado)
                {
                    TempData["Menssagem-sucesso"] = "Contato Apagado com sucesso!";
                    return RedirectToAction("Index");
                }else{
                     TempData["Menssagem-erro"]=$"Não foi possivel apagar o contato:";
                     return RedirectToAction("Index");
                }
                
            }
            catch (System.Exception erro)
            {
                TempData["Menssagem-erro"]=$"Não foi possivel apagar o contato, mais detalhes do erro: {erro.Message}";
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