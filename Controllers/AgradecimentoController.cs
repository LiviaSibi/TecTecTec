using FeedbackMVC.Controllers;
using FeedbackMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TecTecTec.Controllers
{
    public class AgradecimentoController : AbstractController
    {
        public IActionResult Index (){
            BaseViewModel baseviewmodel = new BaseViewModel();
            baseviewmodel.UsuarioLogado = ObterUsuarioSession();
            return View(baseviewmodel);
        }
    }
}