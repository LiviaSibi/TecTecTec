using System.IO;
using FeedbackMVC.Models;

namespace FeedbackMVC.Repositories
{
    public class ClienteRepository
    {
        private const string PATH = "Database/Clientes.csv";
        private const string ARQUIVOSDOSCLIENTES = "wwwroot/ArquivosDosClientes";
        public ClienteRepository(){
            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }
            if(!File.Exists(ARQUIVOSDOSCLIENTES)){
                Directory.CreateDirectory(ARQUIVOSDOSCLIENTES);
            }
        }
        public bool Inserir (Cliente cliente){

            var quantidadeLinhas = File.ReadAllLines(PATH).Length;
            cliente.ID = (ulong) ++quantidadeLinhas;
            string nomedocliente = cliente.UsuarioArroba;

            
            string LocalDasImagens = "wwwroot/ArquivosDosClientes/"+nomedocliente;
            Directory.CreateDirectory(LocalDasImagens);

            // System.IO.File.Copy("wwwroot/img/perfil.png",LocalDasImagens);

            var linha = new string[] {PrepararRegistroCSV(cliente)};
            File.AppendAllLines(PATH, linha);
            
            return true;
        }

        public Cliente ObterPorArroba(string arroba)
        {
            //TODO CRIA NOVA LISTA DE POSTS VAZIA PARA RECEBER OS POSTS
            var linhas = File.ReadAllLines(PATH);

            foreach(var linha in linhas){

                if(ExtrairValorDoCampo("Usuario_Arroba", linha) == arroba)
                {
                    Cliente c = new Cliente();
                    c.ID = ulong.Parse(ExtrairValorDoCampo("ID", linha));
                    c.UsuarioNome = ExtrairValorDoCampo("Usuario_Nome", linha);
                    c.UsuarioArroba = ExtrairValorDoCampo("Usuario_Arroba", linha);
                    c.Bio = ExtrairValorDoCampo("Bio", linha);
                    c.Senha = ExtrairValorDoCampo("Senha", linha);
                    return c;
                }

            }
            return null;


        }
        public Cliente ObterUsuarioPorArroba(string arroba)
        {
            //TODO CRIA NOVA LISTA DE POSTS VAZIA PARA RECEBER OS POSTS
            var linhas = File.ReadAllLines(PATH);

            foreach(var linha in linhas){

                if(ExtrairValorDoCampo("Usuario_Arroba", linha) == arroba)
                {
                    Cliente c = new Cliente();
                    c.ID = ulong.Parse(ExtrairValorDoCampo("ID", linha));
                    c.UsuarioNome = ExtrairValorDoCampo("Usuario_Nome", linha);
                    c.UsuarioArroba = ExtrairValorDoCampo("Usuario_Arroba", linha);
                    c.Bio = ExtrairValorDoCampo("Bio", linha);
                    c.Senha = ExtrairValorDoCampo("Senha", linha);
                    return c;
                }

            }
            return null;


        }

        public bool Existe(string oque, string na_onde)
        {
            //TODO CRIA NOVA LISTA DE POSTS VAZIA PARA RECEBER OS POSTS
            var linhas = File.ReadAllLines(PATH);
            
            foreach(var linha in linhas){

                if(ExtrairValorDoCampo(na_onde, linha) == oque)
                {
                    return true;

                }

            }
            return false;


        }

        private string PrepararRegistroCSV (Cliente cliente){
            return $"ID={cliente.ID};Usuario_Nome={cliente.UsuarioNome};Senha={cliente.Senha};Usuario_Arroba={cliente.UsuarioArroba};Bio={cliente.Bio}";
        }
        

        public string ExtrairValorDoCampo(string nomeCampo, string linha){
            var chave = nomeCampo;
            var indiceChave = linha.IndexOf(chave);

            var indiceTerminal = linha.IndexOf(";", indiceChave);
            var valor = "";

            if(indiceTerminal != -1){
                valor = linha.Substring(indiceChave, indiceTerminal - indiceChave);
            }
            else{
                valor = linha.Substring(indiceChave);
            }

            System.Console.WriteLine($"Campo {nomeCampo} tem valor {valor}");
            return valor.Replace(nomeCampo + "=", "");
        }

        
    }
}