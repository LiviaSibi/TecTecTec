using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeedbackMVC.Models;
using FeedbackMVC.ViewModels;
using FeedbackMVC.Repositories;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using TecTecTec.Models;

namespace FeedbackMVC.Controllers
{
    public class HomeController : AbstractController
    {
        PostRepository postRepository = new PostRepository();
        ClienteRepository clienteRepository = new ClienteRepository();
        CurtidaRepository curtidaRepository = new CurtidaRepository();

        public IHostingEnvironment _env;
        private string _dir;
        public HomeController(IHostingEnvironment env)
        {
            _env = env;



        }
        public IActionResult Index()
        {

            ClienteViewModel clienteviewmodel = new ClienteViewModel();
            clienteviewmodel.PostsDeTodos = postRepository.ObterTodosOsPosts();
            clienteviewmodel.UsuarioLogado = ObterUsuarioSession();
            clienteviewmodel.PostsDeTodos = clienteviewmodel.PostsDeTodos.OrderByDescending(x => x.DataDaPostagem).ToList();
            clienteviewmodel.OQueEuCurti = curtidaRepository.ObterTudoQueCurti(ObterUsuarioSession());
            return View(clienteviewmodel);

        }
        public IActionResult Logar()
        {
            HttpContext.Session.SetString("Usuario","Satva");
            ClienteViewModel clienteviewmodel = new ClienteViewModel();
            clienteviewmodel.PostsDeTodos = postRepository.ObterTodosOsPosts();
            clienteviewmodel.UsuarioLogado = ObterUsuarioSession();
            
            return View("index",clienteviewmodel); //TODO MEXI AQ
        }


        [HttpPost]
        public IActionResult Postar(IFormCollection form)
        {
            if(form["MensagemDoPost"] != "")
            {
                Post post = new Post();
                Cliente cliente = clienteRepository.ObterPorArroba(ObterUsuarioSession());
                post.DonoDoPost = cliente.UsuarioNome;
                post.DonoDoPostArroba = cliente.UsuarioArroba;
                post.Curtidas = 0;
                post.MensagemDoPost = Regex.Replace(form["MensagemDoPost"], @"\t|\n|\r", "");
                postRepository.Inserir(post);

                ClienteViewModel clienteviewmodel = new ClienteViewModel();
                clienteviewmodel.PostsDeTodos = postRepository.ObterTodosOsPosts();
                clienteviewmodel.UsuarioLogado = ObterUsuarioSession();
                return RedirectToAction("Index","Home");
            }else{
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult CurtirPost(IFormCollection form)
        {
            Curtida curtida = new Curtida();
            curtida.Curtidor = ObterUsuarioSession();
            curtida.IDDoPost = Convert.ToUInt64(form["ID"]);

            curtidaRepository.Inserir(curtida);

            ClienteViewModel clienteviewmodel = new ClienteViewModel();
            clienteviewmodel.PostsDeTodos = postRepository.ObterTodosOsPosts();
            clienteviewmodel.UsuarioLogado = ObterUsuarioSession();
            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        public IActionResult DescurtirPost(IFormCollection form)
        {
            curtidaRepository.Remover(Convert.ToUInt64(form["ID"]));

            ClienteViewModel clienteviewmodel = new ClienteViewModel();
            clienteviewmodel.PostsDeTodos = postRepository.ObterTodosOsPosts();
            clienteviewmodel.UsuarioLogado = ObterUsuarioSession();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult ExcluirPost(IFormCollection form)
        {
            
                postRepository.Remover(Convert.ToUInt64(form["ID"]));

                ClienteViewModel clienteviewmodel = new ClienteViewModel();
                clienteviewmodel.PostsDeTodos = postRepository.ObterTodosOsPosts();
                clienteviewmodel.UsuarioLogado = ObterUsuarioSession();
                return RedirectToAction("Index","Home");

        }

        [HttpPost]
        public IActionResult UploadFotoDePerfil(IFormFile file)
        {

            _dir = _env.ContentRootPath+"/wwwroot/ArquivosDosClientes/"+ObterUsuarioSession();
            using (var fileStream = new FileStream(Path.Combine(_dir, "perfil.png"), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            return RedirectToAction("Index","Home");
        }




    }
}
