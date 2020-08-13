using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CidadesCrud2.Models
{
    public class Cidades
    {

        public int Id { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public string Regiao { get; set; }
        public string CodigoIbge { get; set; }
    }
}