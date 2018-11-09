using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Filmes.Models
{
    public class ComandosBancoDeDados
    {
        private string ConnectionString
        {
            get { return @"Data Source=senacturmati.database.windows.net;
                          Initial Catalog=Alysson;
                          User id=senac;
                          Password=Teste123#"; }
        }
        public List<Filmes> BuscarFilmes()
        {
            List<Filmes> listaFilmes = new List<Filmes>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Filmes", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Filmes filmes = new Filmes();
                            filmes.FilmeId = (Guid)reader["FilmeId"];
                            filmes.Quantidade = (int)reader["Quantidade"];
                            filmes.Nome = (string)reader["Nome"];
                            filmes.Diretor = (string)reader["Diretor"];
                            filmes.Genero = (string)reader["Genero"];
                            filmes.Censurado = (bool)reader["Censurado"];
                            listaFilmes.Add(filmes);
                        }
                    }
                }
            }
            return listaFilmes;
        }
        public Filmes BuscarFilmePorId(Guid FilmeId)
        {
            Filmes filmes = new Filmes();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Filmes where FilmeId = @FilmeId", con))
                {
                    command.Parameters.Add("@FilmeId", SqlDbType.UniqueIdentifier).Value = FilmeId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filmes.FilmeId = (Guid)reader["FilmeId"];
                            filmes.Quantidade = (int)reader["Quantidade"];
                            filmes.Nome = (string)reader["Nome"];
                            filmes.Diretor = (string)reader["Diretor"];
                            filmes.Genero = (string)reader["Genero"];
                            filmes.Censurado = (bool)reader["Censurado"];
                        }
                    }
                }
            }
            return filmes;
        }
        public void SalvarFilme(Filmes filmes)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"UPDATE Filmes SET Quantidade = @Quantidade,
                                                            Nome = @Nome,
                                                            Diretor = @Diretor,
                                                            Genero = @Genero,
                                                            Censurado = @Censurado
                                                        where FilmeId = @FilmeId", con))
                {
                    command.Parameters.Add("@FilmeId", SqlDbType.UniqueIdentifier).Value = filmes.FilmeId;
                    command.Parameters.Add("@Quantidade", SqlDbType.VarChar).Value = filmes.Quantidade;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = filmes.Nome;
                    command.Parameters.Add("@Diretor", SqlDbType.VarChar).Value = filmes.Diretor;
                    command.Parameters.Add("@Genero", SqlDbType.VarChar).Value = filmes.Genero;
                    command.Parameters.Add("@Censurado", SqlDbType.Bit).Value = filmes.Censurado;
                    command.ExecuteReader();
                }
            }
        }
        public void InserirFilme(Filmes filmes)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"INSERT INTO Filmes values (@FilmeId,
                                                                @Quantidade,
                                                                @Nome,
                                                                @Diretor,
                                                                @Genero,
                                                                @Censurado)", con))
                {
                    command.Parameters.Add("@FilmeId", SqlDbType.UniqueIdentifier).Value = filmes.FilmeId;
                    command.Parameters.Add("@Quantidade", SqlDbType.Int).Value = filmes.Quantidade;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = filmes.Nome;
                    command.Parameters.Add("@Diretor", SqlDbType.VarChar).Value = filmes.Diretor;
                    command.Parameters.Add("@Genero", SqlDbType.VarChar).Value = filmes.Genero;
                    command.Parameters.Add("@Censurado", SqlDbType.Bit).Value = filmes.Censurado;
                    command.ExecuteReader();
                }
            }
        }
        public void ExcluirFilme(Guid FilmeId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"DELETE Filmes where FilmeId = @FilmeId", con))
                {
                    command.Parameters.Add("@FilmeId", SqlDbType.UniqueIdentifier).Value = FilmeId;
                    command.ExecuteReader();
                }
            }
        }
    }
}