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
    Console.Write(" | 0 - Sair \n | 1 - Cadastrar \n | 2 - Listar \n | 3 - Buscar \n | 4 - Atualizar \n | 5 - Excluir \n | Digite aqui: ");
    var opcao = Console.ReadLine();
    switch (opcao)
    {
        // Sair 
        case "0":
            Console.WriteLine("Saindo do sistema...");
            return;

        // Cadastrar produto
        case "1":
            Console.WriteLine("Digite as informações a seguir para cadastrar o seu produto");
            string nome;
            decimal preco;
            int estoque;


            // Nome
            while(true)
            {
                Console.Write(" | Nome: ");
                nome = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("Nome inválido, tente novamente"); continue;
                }
                break;
            }

            // preco 
            while(true)
            {
                Console.Write(" | Preço: "); 
                try
                {
                    preco = decimal.Parse(Console.ReadLine()!);
                    
                }
                catch
                {
                    Console.WriteLine("Preço inválido, tente novamente");
                    continue;
                }
                if (preco < 0){Console.WriteLine("Seu preço não pode ser negativo");}

                break;
            }
            
            // Estoque 
            while (true)
            {
                Console.Write(" | Estoque: "); 
                try
                {
                    estoque = int.Parse(Console.ReadLine()!);
                    
                }
                catch
                {
                    Console.WriteLine("Estoque inválido, tente novamente");
                    continue;
                }
                if (estoque < 0){Console.WriteLine("Seu estoque não pode ser negativo");}

                break;
            }
       
            await Produto.CreateProduto(nome, preco, estoque);
            break;

        // Listar produto 
        case "2":
            await Produto.ListarProdutos();
            break;

        // Buscar produto 
        case "3":
            await Produto.BuscarProduto();
            break;

        // Atualizar produto 
        case "4":
            await Produto.AtualizarProduto();
            break;

        // Excluir produto
        case "5":
            await Produto.ExcluirProduto();
            break;

        // validacao 
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}












