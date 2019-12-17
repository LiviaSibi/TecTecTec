using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FeedbackMVC.Controllers
{
    public class AbstractController : Controller
    {
        protected string ObterUsuarioSession(){
            var usuario = HttpContext.Session.GetString("Usuario");
            if(!string.IsNullOrEmpty(usuario)){
                return usuario;
            }
            else{
                return "";
            }
        }
    }
}