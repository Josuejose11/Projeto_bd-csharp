using System.ComponentModel.DataAnnotations.Schema;
using CrudEF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrudEF.Models;       // import        

public class Produto
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public decimal? Preco {get; set;}
    public int? Estoque {get; set;}

    //////////////
    /// CREATE ///  
    //////////////
    
    public static async Task<Produto> CreateProduto(string nome, decimal preco, int estoque)
    {
        await using var db = new AppDbContext();       // o Using abre e fecha conexao 
        var produto = new Produto
        {
            Nome = nome,
            Preco = preco,
            Estoque = estoque
        };
        db.Produtos.Add(produto);
        await db.SaveChangesAsync();
        return produto;
    }
    
    ////////////
    /// READ ///  
    ////////////
    
    // Retorna uma lista de todos os produtos 
    public static async Task<List<Produto>> ListarProdutos()
    {
        await using var db = new AppDbContext();
        return await db.Produtos.ToListAsync();
    }

    // Busca por nome 
    public static async Task<Produto?> BuscaPorNome(string nome )
    {
        await using var db = new AppDbContext();
        return await db.Produtos.FirstOrDefaultAsync(p => p.Nome == nome);
    }

    // Retorna produto por id 
    public static async Task<Produto?> GetProdutoById(int id)
    {
        await using var db = new AppDbContext();
        return await db.Produtos.FindAsync(id);
    }

    // read com os produtos com valor maior que...
    public static async Task<List<Produto>> ProdutosValorMaiorQue(decimal quantidade)
    {
        await using var db = new AppDbContext();
        return await db.Produtos 
        .Where(p => p.Preco > quantidade)
        .ToListAsync();
    }

    // Lista em ordem alfabetica 
    public static async Task<List<Produto>> ProdutosPorOrdemAlfabetica()
    {
        await using var db = new AppDbContext();
        return await db.Produtos
        .OrderBy (p => p.Nome)
        .ToListAsync();
    }

    //////////////
    /// UPDATE ///
    //////////////

    public static async Task AtualizarProduto(int id, Produto novoProduto)
    {
        await using var db = new AppDbContext();
        var produto = await db.Produtos.FindAsync(id);
        if(produto == null)
        {
            Console.WriteLine("Produto não encontrado");
            return;
        }

        produto.Nome = novoProduto.Nome ?? produto.Nome;
        produto.Preco = novoProduto.Preco ?? produto.Preco;
        produto.Estoque = novoProduto.Estoque ?? produto.Estoque;

        await db.SaveChangesAsync();
    }


    //////////////
    /// DELETE ///
    //////////////

    public static async Task DeletarProduto(int id)
    {
        await using var db = new AppDbContext();
        var produto = await db.Produtos.FindAsync(id);
        if(produto == null)
        {
            Console.WriteLine("Produto não encontrado");
            return;
        }

        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
    }
    
}