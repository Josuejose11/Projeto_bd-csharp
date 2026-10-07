using System.ComponentModel;
using System.Data.Common;
using CrudEF.Data;
using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

void denovo()
{
    while (true)
    {
        Console.Write("Deseja realizar outra operação? \n | 1 - Sim \n | 2 - Não \n | Digite aqui: ");
        var resposta = Console.ReadLine();
        switch (resposta)
        {
            case "1":
                return;
            case "2":
                Console.WriteLine("Saindo do sistema...");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                continue;
        }
    }
}


Console.Clear();
Console.WriteLine("============================================");
Console.WriteLine("Seja bem vindo ao nosso sistema de produtos!");
Console.WriteLine("============================================");

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
            Console.WriteLine("-------------------");
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

                nome = char.ToUpper(nome[0]) + nome.Substring(1).ToLower();
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
            denovo();
            break;

        // Listar produto 
        case "2":
            Console.WriteLine("-------------------");
            Produto.ListarProdutos();
            denovo();
            break;

        // Buscar produto 
        case "3":
            while (true)
            {
                Console.WriteLine("-------------------");
                Console.Write("Digite aqui o nome do produto: ");
                string? product = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(product))
                {
                    Console.WriteLine("Ocorreu um erro, tente novamente!");
                }
                product = char.ToUpper(product[0]) + product.Substring(1).ToLower();
                
                Produto.BuscaPorNome(product);
                break;
            }
            denovo();
            break;

        // Atualizar produto 
        case "4":
            Console.WriteLine("-------------------");
            Produto.ListarProdutos();

            // vaçlidacao do id
            Console.Write("Digite o id do produto que você quer atualizar: ");
            int id = int.Parse(Console.ReadLine().Trim());
            if (id <= 0)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Informe um id válido");
                Console.WriteLine("-------------------");
            }
            bool Booleano = await Produto.ValidarId(id);
            if (!Booleano)
            {
                Console.WriteLine("Id não encontrado, tente novamente");
                break;
            }

            // escolha do parametro a ser alterado
            Console.WriteLine("Selecione o parâmetro que você deseja alterar");
            Console.Write(" | 1 - Nome \n | 2 - Preço \n | 3 - Estoque \n | Digite aqui: ");
            string? parametro = Console.ReadLine().Replace(" ", "");
            switch (parametro)
            {
                // nome
                case "1":
                    Console.Write("Digite o novo nome: ");
                    string novoNome = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(novoNome))
                    {
                        Console.WriteLine("Nome inválido, tente novamente");
                        break;
                    }
                    novoNome = char.ToUpper(novoNome[0]) + novoNome.Substring(1).ToLower();
                    await Produto.AtualizarProduto(id, "Nome", novoNome);
                    break;

                // preço
                case "2":
                    Console.Write("Digite o novo preço: ");
                    decimal novoPreco;
                    try
                    {
                        novoPreco = decimal.Parse(Console.ReadLine()!);
                        if (novoPreco < 0)
                        {
                            Console.WriteLine("Preço inválido, tente novamente");
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Preço inválido, tente novamente");
                        break;
                    }
                    await Produto.AtualizarProduto(id, "Preco", novoPreco);
                    break;

                // estoque
                case "3":
                    Console.Write("Digite o novo estoque: ");
                    int novoEstoque;
                    try
                    {
                        novoEstoque = int.Parse(Console.ReadLine()!);
                        if (novoEstoque < 0)
                        {
                            Console.WriteLine("Estoque inválido, tente novamente");
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Estoque inválido, tente novamente");
                        break;
                    }
                    await Produto.AtualizarProduto(id, "Estoque", novoEstoque);
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }






            await Produto.AtualizarProduto(id);
            break;

        // Excluir produto
        case "5":
            await Produto.ExcluirProduto();
            break;

        // validacao 
        default:
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.WriteLine("--------------------------------");
            break;
    }
}












