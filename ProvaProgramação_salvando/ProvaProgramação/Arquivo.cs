using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ProvaProgramação
{
     static class Arquivo
    {
        public static void Salvar(List<Filmes> lista, string diretorio)
        {
            try
            {
            StreamWriter file = new StreamWriter(diretorio, false);
            string output = JsonConvert.SerializeObject(lista);

            file.Write(output);
            file.Close();
            }
            catch(Exception erro) { throw erro; }
        }

        public static void Ler(ref List<Filmes> lista, string diretorio)
        {
            try
            {
                string arquivo = File.ReadAllText(diretorio);
                lista = JsonConvert.DeserializeObject<List<Filmes>>(arquivo);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}
