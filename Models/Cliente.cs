using System.Collections.Generic;

namespace FeedbackMVC.Models
{
    public class Cliente
    {
        public ulong ID {get;set;}
        public string UsuarioNome {get;set;}
        public string UsuarioArroba {get;set;}
        public string ImagemPerfilLink {get;set;}
        public string Bio {get;set;}
        public string Senha {get;set;}
        public List<Post> MeusPosts {get;set;}

    }
}