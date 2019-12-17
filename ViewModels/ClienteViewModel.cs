using System.Collections.Generic;
using FeedbackMVC.Models;


namespace FeedbackMVC.ViewModels
{
    public class ClienteViewModel : BaseViewModel
    {
        public List<Post> PostsDeTodos {get;set;}
        public List<ulong> OQueEuCurti {get;set;}
        
    }
}