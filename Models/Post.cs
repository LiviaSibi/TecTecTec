using System;
using System.Collections.Generic;

namespace FeedbackMVC.Models
{
    public class Post
    {
        public ulong ID {get;set;}
        public uint Curtidas {get;set;}
        public string DonoDoPost {get;set;}
        public string DonoDoPostArroba {get;set;}
        public string MensagemDoPost {get;set;}

        //TODO NO COMENTARIOS REPOSITORY OS COMENTARIOS VAO TER UM ID ASSIM FICA MAIS FACIL DE PEGAR ELES
        public ulong IDDaListaDeComentarios {get;set;}
        public List<Comentario> ComentariosDoPost {get;set;}

        public DateTime DataDaPostagem {get;set;}
        public Post()
        {
            this.ID = 0;
            this.Curtidas = 0;
            this.DataDaPostagem = DateTime.Now;

            
        }



        
    }
}