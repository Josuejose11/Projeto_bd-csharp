using CrudEF.Data;
using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

await using var db = new AppDbContext();

var produto = new Produto
{
    Nome = "Teclado",
    Preco = 150.00m,
    Estoque = 10 
};


Console.WriteLine("===================");
Console.WriteLine("Sistema de produtos");
Console.WriteLine("===================");

// Adiciona produto ao banco de dados 
db.Produtos.Add(produto);

// Salva as alterações no banco de dados
await db.SaveChangesAsync();

// Exibe o id do preoduto adicionado a uma mensagem 
Console.WriteLine($"Id: {produto.Id} | Produto '{produto.Nome}' Adicionado com sucesso!");






