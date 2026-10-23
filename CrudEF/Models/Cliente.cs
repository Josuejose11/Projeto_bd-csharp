using CrudEF.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudEF.Models;

public class Cliente
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public List<Pedido> Pedidos {get; set;} = []; 

    /////////////////
    /// VALIDACAO ///
    /////////////////
    
    public static async Task<bool> ValidarId(int clienteId)
    {
        await using var db = new AppDbContext();

        List<int> ids =  await db.Clientes
            .Select(c => c.Id)
            .ToListAsync();
        return ids.Contains(clienteId);
    }

    //////////////
    /// CREATE ///  
    //////////////
    
    public static async Task<Cliente> CadastrarCliente(string nome, string email)
    {
        await using var db = new AppDbContext();       // o Using abre e fecha conexao 
        var cliente = new Cliente
        {
            Nome = nome,
            Email = email
        };
        db.Clientes.Add(cliente);
        await db.SaveChangesAsync();
        return cliente;
    }
    
    ////////////
    /// READ ///  
    ////////////
    
    // Retorna uma lista de todos os produtos 
    public static async void LerClientes()
    {
        await using var db = new AppDbContext();
        var clientes = await db.Clientes.ToListAsync();

        foreach (var cliente in clientes)
        {
            Console.WriteLine($" | Id: {cliente.Id} | Nome: {cliente.Nome} | Email: {cliente.Email}");
        }
        
    }

    

    
    
    
}