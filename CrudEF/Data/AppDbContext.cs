// Arquivo que conecta com o sql 
// dotnet tool install --global dotnet-ef             //ef migrations remove                 //dotnet ef migrations add InitialCreate
using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEF.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=crud.dc");
    }


}