using CidadesCrud2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CidadesCrud2.Repository
{
    public class CidadesRepository
    {
        public List<Cidades> ObterTodasCidades()
        {
            var listaCidades = new List<Cidades>();

            string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;

            var sql = "SELECT * FROM mysqlcidades.cidades;";

            var conexao = new MySqlConnection(stringConexao);

            var command = new MySqlCommand(sql, conexao);

            conexao.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cidade = new Cidades()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Regiao = reader["reg"].ToString(),
                        Lat = reader["lat"].ToString(),
                        Lng = reader["lng"].ToString(),
                        Nome = reader["nome"].ToString(),
                        UF = reader["uf"].ToString(),
                        CodigoIbge = reader["codigo_ibge"].ToString()
                    };
                    listaCidades.Add(cidade);
                }
            }

            conexao.Close();

            return listaCidades;
        }


        public Cidades CadastrarCidade(Cidades cidade)
        {
            string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;

            var sql = "insert into cidades(codigo_ibge,uf,nome,lng,lat,reg)values(@codigo_ibge,@uf,@nome,@lng,@lat,@regiao);";

            MySqlConnection conn = new MySqlConnection(stringConexao);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = sql;
            comm.Parameters.AddWithValue("@uf", cidade.UF);
            comm.Parameters.AddWithValue("@nome", cidade.Nome);
            comm.Parameters.AddWithValue("@lng", cidade.Lng);
            comm.Parameters.AddWithValue("@lat", cidade.Lat);
            comm.Parameters.AddWithValue("@regiao", cidade.Regiao);
            comm.Parameters.AddWithValue("@codigo_ibge", cidade.CodigoIbge);
            comm.ExecuteNonQuery();
            conn.Close();

            return ObterCidade(cidade.Nome, cidade.UF).FirstOrDefault();
        }

        public void ExcluirCidade(string id)
        {
            string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;


            var sql = "delete from cidades where id=@id";

            MySqlConnection conn = new MySqlConnection(stringConexao);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = sql;
            comm.Parameters.AddWithValue("@id", id);
            comm.ExecuteNonQuery();
            conn.Close();

        }

        public void AlterarCidades(Cidades cidade)
        {
            var stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(stringConexao);
            
            try
            {
                string sql = "update cidades set codigo_ibge = @codigo_ibge, uf = @uf, nome = @nome, lng = @lng, lat = @lat, reg = @reg where id = @id";
                var comm = new MySqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@id", cidade.Id);
                comm.Parameters.AddWithValue("@reg", cidade.Regiao);
                comm.Parameters.AddWithValue("@nome", cidade.Nome);
                comm.Parameters.AddWithValue("@uf", cidade.UF);
                comm.Parameters.AddWithValue("@codigo_ibge", cidade.CodigoIbge);
                comm.Parameters.AddWithValue("@lat", cidade.Lat);
                comm.Parameters.AddWithValue("@lng", cidade.Lng);
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }


        public List<Cidades> ObterCidade(string nome, string uf)
        {
            var listaCidades = new List<Cidades>();

            string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;

            var sql = "SELECT * FROM mysqlcidades.cidades where nome = @nome and uf = @uf;";

            var conexao = new MySqlConnection(stringConexao);

            var command = new MySqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@uf", uf);
            command.Parameters.AddWithValue("@nome", nome);
            conexao.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cidade = new Cidades()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Regiao = reader["reg"].ToString(),
                        Lat = reader["lat"].ToString(),
                        Lng = reader["lng"].ToString(),
                        Nome = reader["nome"].ToString(),
                        UF = reader["uf"].ToString(),
                        CodigoIbge = reader["codigo_ibge"].ToString()
                    };
                    listaCidades.Add(cidade);
                }
            }

            conexao.Close();

            return listaCidades;
        }

        public List<Cidades> ObterCidadePeloNome(string nome)
        {
            var listaCidades = new List<Cidades>();

            string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;

            var sql = "SELECT * FROM mysqlcidades.cidades where nome like '%" + nome + "%'";

            var conexao = new MySqlConnection(stringConexao);

            var command = new MySqlCommand(sql, conexao);
            conexao.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cidade = new Cidades()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Regiao = reader["reg"].ToString(),
                        Lat = reader["lat"].ToString(),
                        Lng = reader["lng"].ToString(),
                        Nome = reader["nome"].ToString(),
                        UF = reader["uf"].ToString(),
                        CodigoIbge = reader["codigo_ibge"].ToString()
                    };
                    listaCidades.Add(cidade);
                }
            }

            conexao.Close();

            return listaCidades;
        }

        public bool ValidarCadastroIgual(string nome, string uf, int id)
        {

            var stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["CidadeConexao"].ConnectionString;

            var sql = "SELECT * FROM mysqlcidades.cidades where nome = @nome and uf = @uf";
            if (id > 0)
                sql += " and id <> @id";
            var conexao = new MySqlConnection(stringConexao);

            var command = new MySqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@uf", uf);
            if (id > 0)
                command.Parameters.AddWithValue("@id", id);
            conexao.Open();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                    return false;
                else
                    return true;
            }
        }






    }

}
