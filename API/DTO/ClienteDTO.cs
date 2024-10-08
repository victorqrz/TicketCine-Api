namespace API.DTO
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
    }
}
