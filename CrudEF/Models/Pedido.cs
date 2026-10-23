using CrudEF.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudEF.Models;

public class Pedido
{
    public int Id {get; set;}
    public DateTime Data {get; set;}
    public decimal Valor {get; set;}
    public int ClienteId {get; set;}
    public Cliente Cliente {get; set;} = null!;

    


    //////////////
    /// CREATE ///  
    //////////////
    
    public static async Task<Pedido> CadastrarPedido(int clienteId, decimal valor)
    {
        DateTime data = DateTime.Now;
        await using var db = new AppDbContext();       // o Using abre e fecha conexao 
        var pedido = new Pedido
        {
            ClienteId = clienteId,
            Data = data,
            Valor = valor
        };
        db.Pedidos.Add(pedido);
        await db.SaveChangesAsync();
        return pedido;
    }

    
      
    ////////////
    /// READ ///  
    ////////////
    
    // Retorna uma lista de todos os produtos 
    public static async void ListarPedidos()
    {
        await using var db = new AppDbContext();
        var pedidos = await db.Pedidos.ToListAsync();

        foreach (var pedido in pedidos)
        {
            Console.WriteLine($" | Id: {pedido.Id} | Data: {pedido.Data} | Valor: {pedido.Valor} | Cliente Id: {pedido.ClienteId}");
        } 
    }

    // buscando pedidos de um cliente 
    public static async void BuscarPedidosDeCliente()
    {
        using (var db = new AppDbContext())
        {
            var pedidos = await db.Pedidos
                .Where(p => p.ClienteId == 1)
                .ToListAsync();
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"Pedido Id: {pedido.Id} | Valor: {pedido.Valor}");
            } 
        }
    }

    // Lista clientes e seus pedidos 
    public static async void ListarClientesPedidos()
    {
        using (var db = new AppDbContext())
        {
            var clientes = await db.Clientes
                .Include(c => c.Pedidos)
                .ToListAsync();
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Cliente: {cliente.Nome} | Email: {cliente.Email}");
                foreach (var pedido in cliente.Pedidos)
                {
                    Console.WriteLine($"Pedido Id {pedido.Id} | Valor: {pedido.Valor}");
                }
            }
        }
    }
}