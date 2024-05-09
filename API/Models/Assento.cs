using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("assentos")]
    public class Assento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Identificador { get; set; }
        public bool Ocupado { get; set; }
        public Sala Sala { get; set; }  
    }
}
