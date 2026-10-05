using CrudEF.Data;
using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

// // create 
// var produto = await Produto.CreateProduto("Betoneira", 10.99m, 100);

// Console.WriteLine($"Id: {produto.Id} | Nome: {produto.Nome} \n | Seu produto foi adicionado com sucesso!");

// // read 
// foreach (var p in await Produto.ListarProdutos())
// {
//     Console.WriteLine($"Id: {p.Id} | Nome: {p.Nome} | Preço: {p.Preco} | Estoque: {p.Estoque}");
// }
Console.Clear();
Console.WriteLine("Seja bem vindo ao nosso sistema de produtos!");

// MENU
while (true)
{
    Console.WriteLine("Selecione a ação que você gostaria de usar: ");
    Console.Write("| 0 - Sair \n | 1 - Cadastrar \n | 2 - Listar \n | 3 - Buscar \n | 4 - Atualizar \n | 5 - Excluir \n | Digite aqui: ");
    Console.ReadLine();
}












