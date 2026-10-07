using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;
using CrudEF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

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
    public static async void ListarProdutos()
    {
        await using var db = new AppDbContext();
        var produtos = await db.Produtos.ToListAsync();

        foreach (var produto in produtos)
        {
            Console.WriteLine($" | Id: {produto.Id} | Nome: {produto.Nome} | Preço: {produto.Preco} | Estoque: {produto.Estoque}");
        } 
    }

    // Busca por nome 
    public static async void BuscaPorNome(string nome )
    {
        await using var db = new AppDbContext();
        var produto = await db.Produtos.FirstOrDefaultAsync(p => p.Nome == nome);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado");
            return;
        }
        
        Console.WriteLine($" | Id: {produto.Id} | Nome: {produto.Nome} | Preço: {produto.Preco} | Estoque: {produto.Estoque}");
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

    // Valida o id 
    public static async Task<bool> ValidarId(int id)
    {
        await using var db = new AppDbContext();
        return await db.Produtos.AnyAsync(p => p.Id == id);
    }





    //////////////
    /// UPDATE ///
    //////////////

    public static async Task AtualizarProduto(int id, string campo, object valor)
    {
        await using var db = new AppDbContext();
        var produto = await db.Produtos.FindAsync(id);
        if(produto == null)
        {
            Console.WriteLine("Produto não encontrado");
            return;
        }

        switch (campo)
        {
            case "Nome":
                produto.Nome = valor.ToString()!;
                break;
            case "Preco":
                produto.Preco = decimal.Parse(valor.ToString()!);
                break;
            case "Estoque":
                produto.Estoque = int.Parse(valor.ToString()!);
                break;
            default:
                Console.WriteLine("Campo inválido");
                return;
        }

        await db.SaveChangesAsync();
        Console.WriteLine("Produto atualizado com sucesso");
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