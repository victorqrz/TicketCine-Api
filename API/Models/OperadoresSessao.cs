using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("operadores_sessoes")]
    public class OperadorSessao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOperadorSessao { get; set; }

        [ForeignKey("Operador")]
        public string CPF { get; set; } // Altere para string para corresponder ao tipo de CPF
        public virtual Operador Operador { get; set; }

        [ForeignKey("Sessao")]
        public int IdSessao { get; set; }
        public virtual Sessao Sessao { get; set; }
    }
}
