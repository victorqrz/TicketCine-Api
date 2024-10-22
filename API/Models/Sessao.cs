using API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("sessoes")]
public class Sessao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdSessao { get; set; }

    public DateTime HoraInicio { get; set; }
    public DateTime HoraFim { get; set; }
    public decimal Valor { get; set; }

    // Chave estrangeira explícita para Filme
    [ForeignKey("Filme")]
    [Column("FilmeIdFilme")]
    public int IdFilme { get; set; }
    public virtual Filme Filme { get; set; }

    // Chave estrangeira explícita para Sala
    [ForeignKey("Sala")]
    [Column("SalaIdSala")]
    public int IdSala { get; set; }
    public virtual Sala Sala { get; set; }
}
