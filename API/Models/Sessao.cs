using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("sessoes")]
    public class Sessao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSessao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public decimal Valor { get; set; }

        public int IdFilme { get; set; } // Alterado para int
        public int IdSala { get; set; }  // Alterado para int

        // Navegação opcional, sem impacto no POST
        public Filme Filme { get; set; }
        public Sala Sala { get; set; }
    }
}
