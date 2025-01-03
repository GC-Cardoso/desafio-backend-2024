using Desafio.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Desafio.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options) 
        {
        
        }
        public DbSet<Conta> Contas { get; set; } = null!;
        public DbSet<Movimento> Movimentos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
