using System.ComponentModel.DataAnnotations;

namespace PROJETO_BANCO.Models
{
    public class Usuario
    {
        // [Display] define o nome que aparece na tela
        [Display(Name = "Código")]
        public int IdUsu { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string nomeUsu { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo Cargo é obrigatório")]
        public string Cargo { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "O campo nascimento é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime DataNasc { get; set; }
    }
}