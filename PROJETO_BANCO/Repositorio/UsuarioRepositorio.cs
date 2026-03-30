using PROJETO_BANCO.Models;
using PROJETO_BANCO.Repositorio.Contrato;
using MySql.Data.MySqlClient;
using System.Data;

namespace PROJETO_BANCO.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // Propriedade que guardará a string de conexão
        private readonly string _conexaoMySQL;

        // Construtor: recebe IConfiguration para acessar o appsettings.json
        public UsuarioRepositorio(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Usuario usuario)
        {
            // "using" garante que a conexão será fechada mesmo se der erro
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO tbUsuario(nomeUsu, Cargo, DataNasc) " +
                    "VALUES (@nomeUsu, @Cargo, @DataNasc)", conexao);

                // Adiciona os valores dos parâmetros
                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value =
                    usuario.DataNasc.ToString("yyyy/MM/dd"); 

                // ExecuteNonQuery para INSERT, UPDATE, DELETE
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            List<Usuario> UsuarioList = new List<Usuario>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario", conexao);

                // DataAdapter + DataTable: carrega todos os dados de uma vez
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                // Percorre cada linha da tabela e cria um objeto Usuario
                foreach (DataRow dr in dt.Rows)
                {
                    UsuarioList.Add(new Usuario
                    {
                        IdUsu = Convert.ToInt32(dr["IdUsu"]),
                        nomeUsu = (string)dr["nomeUsu"],
                        Cargo = (string)dr["Cargo"],
                        DataNasc = Convert.ToDateTime(dr["DataNasc"])
                    });
                }
            }

            return UsuarioList;
        }

        public Usuario ObterUsuario(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM usuario WHERE IdUsu=@IdUsu", conexao);

                cmd.Parameters.AddWithValue("@IdUsu", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();

                // ExecuteReader retorna os dados linha por linha
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    usuario.IdUsu = Convert.ToInt32(dr["IdUsu"]);
                    usuario.nomeUsu = (string)dr["nomeUsu"];
                    usuario.Cargo = (string)dr["Cargo"];
                    usuario.DataNasc = Convert.ToDateTime(dr["DataNasc"]);
                }

                // Sempre feche o DataReader!
                dr.Close();
                return usuario;
            }
        }
        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE usuario SET nomeUsu=@nomeUsu, Cargo=@Cargo, " +
                    "DataNasc=@DataNasc WHERE IdUsu=@IdUsu", conexao);

                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value =
                    usuario.DataNasc.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@IdUsu", MySqlDbType.VarChar).Value = usuario.IdUsu;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "DELETE FROM usuario WHERE IdUsu=@IdUsu", conexao);

                cmd.Parameters.AddWithValue("@IdUsu", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
