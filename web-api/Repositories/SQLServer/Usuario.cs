using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace web_api.Repositories.SQLServer
{
    public class Usuario
    {
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;

        public Usuario(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand() { Connection = conn };
        }

        public async Task<List<Models.Usuario>> Select()
        {
            var usuarios = new List<Models.Usuario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, senha FROM Usuario;";
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var usuario = new Models.Usuario();
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            usuario.Senha = dr["senha"].ToString();

                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }

        public async Task<Models.Usuario> Select(int id)
        {
            Models.Usuario usuario = null;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, senha FROM Usuario WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            usuario = new Models.Usuario();
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            usuario.Senha = dr["senha"].ToString();
                        }
                    }
                }
            }

            return usuario;
        }

        public async Task<List<Models.Usuario>> Select(string nome)
        {
            var usuarios = new List<Models.Usuario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT id, nome, email, senha FROM Usuario WHERE nome LIKE @nome;";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = $"%{nome}%";

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var usuario = new Models.Usuario();
                            usuario.Id = (int)dr["id"];
                            usuario.Nome = dr["nome"].ToString();
                            usuario.Email = dr["email"].ToString();
                            usuario.Senha = dr["senha"].ToString();

                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }

        public async Task<bool> Insert(Models.Usuario usuario)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = @"INSERT INTO usuario(nome, email, senha) VALUES
                    (@nome, @email, @senha); SELECT CONVERT(int, scope_identity());";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = usuario.Nome;
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = usuario.Email;
                    cmd.Parameters.Add(new SqlParameter("@senha", SqlDbType.VarChar)).Value = usuario.Senha;

                    usuario.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }

            return usuario.Id != 0;
        }

        public async Task<bool> Update(Models.Usuario usuario)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE usuario SET nome = @nome, email = @email, senha = @senha WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = usuario.Nome;
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = usuario.Email;
                    cmd.Parameters.Add(new SqlParameter("@senha", SqlDbType.VarChar)).Value = usuario.Senha;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = usuario.Id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas == 1;
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE FROM usuario WHERE id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas == 1;
        }
    }
}