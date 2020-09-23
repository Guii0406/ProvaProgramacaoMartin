using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaProgramação
{
    class Filmes
    {
        //nome
        private string nomeDoFilme;
        public string NomeDoFilme
        {
            get { return nomeDoFilme; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    nomeDoFilme = "undefined";
                }
                else
                {
                    nomeDoFilme = value;
                }
            }
        }
        //nota
        private float notaDoFilme;
        public float NotaDoFilme { get { return notaDoFilme; }
            set {
                if(value > 5)
                {
                    notaDoFilme = 5;
                }
                else
                {
                    notaDoFilme = value;
                }
            } }
        //data
        private DateTime dataDeLancamento;
        public DateTime DataDeLancamento
        {
            get { return dataDeLancamento; }
            set
            {
                if (value.Year > DateTime.Now.Year)
                {
                    dataDeLancamento = DateTime.Today;
                }
                else
                {
                    dataDeLancamento = value;
                }
            }
        }
        //constructor
        public Filmes(string nomeDoFilme, float notaDoFilme, DateTime dataDeLancamento)
        {
            this.NomeDoFilme = nomeDoFilme;
            this.NotaDoFilme = notaDoFilme;
            this.DataDeLancamento = dataDeLancamento;
        }
        //metodos
        public void AlterarFilme(string nomeDoFilme, float notaDoFilme, DateTime dataDeLancamento)
        {
            this.NomeDoFilme = nomeDoFilme;
            this.NotaDoFilme = notaDoFilme;
            this.DataDeLancamento = dataDeLancamento;
        }
    }
}
