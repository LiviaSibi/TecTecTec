using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FeedbackMVC.Models;
using TecTecTec.Models;

namespace FeedbackMVC.Repositories
{
    public class CurtidaRepository
    {
        private const string PATH = "Database/Curtidas.csv";
        public CurtidaRepository(){
            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }
        }

        public bool Inserir (Curtida curtida){
            var linha = new string[] {PrepararRegistroCSV(curtida)};
            File.AppendAllLines(PATH, linha);
            
            return true;
        }

        public List<ulong> ObterTudoQueCurti(string Arroba)
        {
            List<ulong> TudoQueCurti = new List<ulong>();
            var linhas = File.ReadAllLines(PATH);

            foreach(var linha in linhas){

                if((ExtrairValorDoCampo("Curtidor", linha) == Arroba))
                {
                    
                    TudoQueCurti.Add(ulong.Parse(ExtrairValorDoCampo("IDDoPost", linha)));
                }
                
            }

            return TudoQueCurti;
        }


        public void Remover(ulong IDDoPost)
        {

            var linhas = File.ReadAllLines(PATH);
            
            for(int i = 0; i < linhas.Length; i++){
                if(Convert.ToUInt64(ExtrairValorDoCampo("IDDoPost", linhas[i])) == IDDoPost)
                {
                    List<string> ListaLinhas = linhas.ToList();
                    ListaLinhas.Remove(linhas[i]);

                    linhas = ListaLinhas.ToArray();
                    File.WriteAllLines(PATH, linhas);
                    break;
                }
                    
            }

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

        private string PrepararRegistroCSV (Curtida curtida){
            return $"IDDoPost={curtida.IDDoPost};Curtidor={curtida.Curtidor}";
        }
    }
}