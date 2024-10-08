using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("gerente")]
    public class Gerente
    {
        [Key]
        public int IdGerente { get; set; } // PK
        [Required]
        public string CPF { get; set; }    // CPF
        [Required]
        public string Nome { get; set; }   // Nome
        [Required]
        public string Email { get; set; }  // Email
        [Required]
        public string Senha { get; set; }  // Senha
    }
}
