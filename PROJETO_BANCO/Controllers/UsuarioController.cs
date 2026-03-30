using PROJETO_BANCO.Models;
using PROJETO_BANCO.Repositorio.Contrato;
using Microsoft.AspNetCore.Mvc;

namespace PROJETO_BANCO.Controllers
{
    public class UsuarioController : Controller
    {
        // O ASP.NET Core injeta automaticamente o repositório aqui
        private IUsuarioRepositorio _usuarioRepository;

        public UsuarioController(IUsuarioRepositorio usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public IActionResult Index()
        {
            return View(_usuarioRepository.ObterTodosUsuarios());
        }

        [HttpGet]
        public IActionResult CadastrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid) 
            {
                _usuarioRepository.Cadastrar(usuario);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AtualizarUsuario(int id)
        {
            return View(_usuarioRepository.ObterUsuario(id));
        }

        [HttpPost]
        public IActionResult AtualizarUsuario(Usuario usuario)
        {
            _usuarioRepository.Atualizar(usuario);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExcluirUsuario(int id)
        {
            _usuarioRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DetalhesUsuario(int id)
        {
            return View(_usuarioRepository.ObterUsuario(id));
        }
    }
}
