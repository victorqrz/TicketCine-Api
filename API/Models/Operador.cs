using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("operadores")]
    public class Operador
    {
        [Key]
        public string CPF { get; set; }
        public string Nome { get; set;}
        public string Email { get; set; }
    }
}
