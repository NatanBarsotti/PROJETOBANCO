using PROJETO_BANCO.Models;

namespace PROJETO_BANCO.Repositorio.Contrato
{
    public interface IUsuarioRepositorio
    {
        // Retorna uma lista de todos os usuários
        IEnumerable<Usuario> ObterTodosUsuarios();

        // Retorna um usuário pelo ID
        Usuario ObterUsuario(int Id);

        // Cadastra um novo usuário (void = não retorna nada)
        void Cadastrar(Usuario usuario);

        // Atualiza um usuário existente
        void Atualizar(Usuario usuario);

        // Exclui um usuário pelo ID
        void Excluir(int Id);
    }
}
