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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using TecTecTec.Models;

namespace FeedbackMVC.Controllers
{
    public class ClienteController : AbstractController
    {
        ClienteRepository clienteRepository = new ClienteRepository();
        public IActionResult Login()
        {

            BaseViewModel baseviewmodel = new BaseViewModel();
            baseviewmodel.UsuarioLogado = ObterUsuarioSession();
            return View(baseviewmodel);

        }
        public IActionResult Cadastro()
        {

            BaseViewModel baseviewmodel = new BaseViewModel();
            baseviewmodel.UsuarioLogado = ObterUsuarioSession();
            return View(baseviewmodel);

        }
        public IActionResult index()
        {

            BaseViewModel baseviewmodel = new BaseViewModel();
            baseviewmodel.UsuarioLogado = ObterUsuarioSession();
            return View(baseviewmodel);

        }
        [HttpPost]
        public IActionResult Logar(IFormCollection form)
        {
            if(clienteRepository.Existe(form["UsuarioArroba"],"Usuario_Arroba") == true)
            {
                Cliente cliente = clienteRepository.ObterPorArroba(form["UsuarioArroba"]);
                if(cliente.Senha == form["Senha"])
                {
                    HttpContext.Session.SetString("Usuario",cliente.UsuarioArroba);
                    return RedirectToAction("Index","Home");
                }else{
                    BaseViewModel baseviewmodel = new BaseViewModel();
                    baseviewmodel.MensagemDeErro = "Senha não bate!";
                    return View("login",baseviewmodel);
                }


            }else
            {
                BaseViewModel baseviewmodel = new BaseViewModel();
                baseviewmodel.MensagemDeErro = "Usuario não existe!";
                return View("login",baseviewmodel);
            }


        }



        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form)
        {
            if(clienteRepository.Existe(form["UsuarioArroba"],"Usuario_Arroba") == false)
            {
                Cliente cliente = new Cliente();
                cliente.UsuarioArroba = "@"+form["UsuarioArroba"];
                cliente.UsuarioNome = form["UsuarioNome"];
                cliente.Senha = form["Senha"];
                clienteRepository.Inserir(cliente);
                HttpContext.Session.SetString("Usuario",cliente.UsuarioArroba);
                return RedirectToAction("Index","Home");



            }else
            {
                BaseViewModel baseviewmodel = new BaseViewModel();
                baseviewmodel.MensagemDeErro = "Usuario já existe!";
                return View("login",baseviewmodel);
            }


        }
        public IActionResult Logoff()
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToAction("index","Home");
        }
        
    }
}