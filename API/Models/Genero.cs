using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("generos")]
    public class Genero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGenero { get; set; }
        public string Tipo { get; set; }
        //public ICollection<Filme> Filmes { get; set; }

    }
}
