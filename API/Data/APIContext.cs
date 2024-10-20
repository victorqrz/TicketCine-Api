using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;

namespace API.Data
{
    public class APIContext : IdentityDbContext
    {
        public APIContext(DbContextOptions<APIContext> options) :
          base(options)
        {
        }

        // DbSet para suas tabelas no banco de dados
        public DbSet<Assento> Assentos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }   
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Filme> Filmes { get; set; }    
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }
        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<OperadorSessao> OperadorSessao { get; set; }
        public DbSet<Gerente> Gerentes { get; set; } // Adicionando o DbSet para a tabela Gerente

        // Override do método OnModelCreating para configurar o seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chamar o base para as configurações de IdentityDbContext

            // Configurando o seed de dados para Gerente
            var gerente = new Gerente
            {
                IdGerente = 1,
                CPF = "12345678900",
                Nome = "Admin",
                Email = "admin@admin.com",
                // Criptografando a senha
                Senha = CriptografiaService.GerarHashMd5("senhaSegura123") // Substitua por sua função de criptografia
            };

            modelBuilder.Entity<Gerente>().HasData(gerente); // Seed do Gerente
        }
    }
}
