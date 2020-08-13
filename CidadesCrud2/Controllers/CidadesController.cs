using CidadesCrud2.Models;
using CidadesCrud2.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CidadesCrud2.Controllers
{
    public class CidadesController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public List<Cidades> ObterCidades()
        {
            var cidades = new CidadesRepository().ObterTodasCidades();

            return cidades;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<Cidades> ObterCidadePeloNome(string nome)
        {
            var cidades = new CidadesRepository().ObterCidadePeloNome(nome);

            return cidades;
        }

        [HttpPost]
        [ActionName("Cadastrar")]
        public Cidades Cadastrar(string nome, string uf, string regiao, string lat, string lng, string codigoIbge)
        {
            var novaCidade = new Cidades()
            {
                Lat = lat,
                Lng = lng,
                Nome = nome,
                Regiao = regiao,
                UF = uf,
                CodigoIbge = codigoIbge
            };

            var cidades = new CidadesRepository().CadastrarCidade(novaCidade);
            return cidades;
        }

        [HttpPost]
        [AllowAnonymous]
        public void ExcluirCadastro(string id)
        {

            new CidadesRepository().ExcluirCidade(id);
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Alterar")]
        public void Alterar(string nome, string uf, string regiao, string lat, string lng, string codigoIbge, int id)
        {
            var cidade = new Cidades()
            {
                Lat = lat,
                Lng = lng,
                Nome = nome,
                Regiao = regiao,
                UF = uf,
                CodigoIbge = codigoIbge,
                Id = id
            };

            new CidadesRepository().AlterarCidades(cidade);
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("ValidarCadastroIgual")]
        public bool ValidarCadastroIgual(string nome, string uf, int id) {
            return new CidadesRepository().ValidarCadastroIgual(nome, uf, id);
        }


        




    }
}