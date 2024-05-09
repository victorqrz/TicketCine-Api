using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("filmes")]
    public class Filme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFilme { get; set; }
        public string Titulo { get; set; }    
        public string Descricao { get; set; }
        public string Poster { get; set; }
        public int Duracao { get; set; }
        public int? IdGenero { get; set; }
    }
}
