using System.ComponentModel;
using System.Data.Common;
using System.Text.RegularExpressions;
using CrudEF.Data;
using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

// funcao para pedir para a pessoa fazer denovo
void denovo()
{
    while (true)
    {
        Console.Write("Deseja realizar outra operação? \n | 1 - Sim \n | 2 - Não \n | Digite aqui: ");
        var resposta = Console.ReadLine();
        switch (resposta)
        {
            case "1":
                Console.WriteLine("================");
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






// MENU PRODUTOS 
Console.Clear();
Console.WriteLine("============================================");
Console.WriteLine("Seja bem vindo ao nosso sistema de compras!");
Console.WriteLine("============================================");

while (true)
{
    Console.WriteLine("Escolha em qual categoria você gostaria de alterar: ");
    Console.Write(" | 0 - Sair \n | 1 - Produto \n | 2 - Cliente \n | 3 - Pedido\n | Escreva aqui: ");
    var escolha = Console.ReadLine().Trim();
    switch(escolha)
    {
        // Sair 
        case "0":
            Console.WriteLine("Saindo do sistema...");
            return;

        // produto 
        case "1":
            while(true)
            {
                Console.WriteLine("Selecione a ação que você gostaria de usar: ");
                Console.Write(" | 1 - Cadastrar \n | 2 - Listar \n | 3 - Buscar \n | 4 - Atualizar \n | 5 - Excluir \n | Digite aqui: ");
                var opcao = Console.ReadLine();
                switch (opcao)
        {
            
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
                    
                    if(!decimal.TryParse(Console.ReadLine(), out preco))
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

                break;

            // Excluir produto
            case "5":
            Console.WriteLine("-------------------");
                Produto.ListarProdutos();

                // vaçlidacao do id
                Console.Write("Digite o id do produto que você quer excluir: ");
                int idExcluir = int.Parse(Console.ReadLine().Trim());
                if (idExcluir <= 0)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Informe um id válido");
                    Console.WriteLine("-------------------");
                }
                bool BooleanoExcluir = await Produto.ValidarId(idExcluir);
                if (!BooleanoExcluir)
                {
                    Console.WriteLine("Id não encontrado, tente novamente");
                    break;
                }
                
                // deletando produto
                await Produto.DeletarProduto(idExcluir);
                denovo();
                break;

            // validacao 
            default:
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Opção inválida. Tente novamente.");
                Console.WriteLine("--------------------------------");
                break;
        }
            }
        
        // cliente
        case "2":
            while (true) 
            {
                Console.WriteLine("Selecione a ação que você gostaria de usar: ");
                Console.Write(" | 1 - Cadastrar \n | 2 - Listar \n | Escreva aqui: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    // adicionar cliente
                    case "1":
                        Console.WriteLine("-------------------");
                        Console.WriteLine("Digite as informações a seguir para o cadastro");
                        string nome;
                        string email;
                        
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

                        //Email 
                        while(true)
                        {
                            Console.Write(" | Email: ");
                            email = Console.ReadLine().Trim();
                            if (string.IsNullOrWhiteSpace(email))
                            {
                                Console.WriteLine("Email inválido, tente novamente"); continue;
                            }
                            else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                            {
                                Console.WriteLine("Email inválido, tente novamente"); continue;
                            }

                            break;
                        }
                       
                        await Cliente.CadastrarCliente(nome, email);
                        denovo();
                        break;

                    // Listar cliente
                    case "2":
                        Console.WriteLine("-------------------");
                        Cliente.ListarClientes();
                        denovo();
                        break;

                    // validacao
                    default:
                        Console.WriteLine("Valor inválido, tente novamente");
                        break;

                }

                

            }

        // pedido
        case "3":
            while (true) 
            {
                Console.WriteLine("Selecione a ação que você gostaria de usar: ");
                Console.Write(" | 1 - Cadastrar \n | 2 - Listar \n | 3- Listar pedidos de um cliente \n | 4 - Atualizar \n | 5 - Excluir \n | Escreva aqui: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    // adicionar pedido
                    case "1":
                        Console.WriteLine("-------------------");
                        Console.WriteLine("Digite as informações a seguir para o cadastro");
                        int clienteId;
                        int produtoId;
                        int quantidade;

                        // ClienteId
                        while(true)
                        {
                            Cliente.Ler_Cliente();
                            Console.Write(" | Cliente Id: ");
                            if(!int.TryParse(Console.ReadLine(), out clienteId))
                            {
                                Console.WriteLine("Cliente Id inválido, tente novamente"); continue;
                            }
                            break;
                        }

                        // ProdutoId
                        while(true)
                        {
                            Produto.Ler_Produto();
                            Console.Write(" | Produto Id: ");
                            if(!int.TryParse(Console.ReadLine(), out produtoId))
                            {
                                Console.WriteLine("Produto Id inválido, tente novamente"); continue;
                            }
                            break;
                        }

                        // Quantidade
                        while(true)
                        {
                            Console.Write(" | Quantidade: ");
                            if(!int.TryParse(Console.ReadLine(), out quantidade))
                            {
                                Console.WriteLine("Quantidade inválida, tente novamente"); continue;
                            }
                            break;
                        }

                        await Pedido.CadastrarPedido(clienteId, produtoId, quantidade);
                        denovo();
                        break;

                    // Listar pedido
                    case "2":
                        Console.WriteLine("-------------------");
                        Pedido.ListarPedidos();
                        denovo();
                        break;

                    // Listar pedidos de um cliente
                    case "3":
                        Console.WriteLine("-------------------");
                        Pedido.BuscarPedidosDeCliente();
                        denovo();
                        break;

                    // Atualizar pedido
                    case "4":
                        Console.WriteLine("-------------------");
                        Pedido.ListarPedidos();
                        Console.Write("Digite o id do pedido que você quer atualizar: ");
                        int id = int.Parse(Console.ReadLine().Trim());
                        
                        // validacao
                        if (id <= 0)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine("Informe um id válido");
                            Console.WriteLine("-------------------");
                        }
                        bool Booleano = await Pedido.ValidarId(id);
                        if (!Booleano)
                        {
                            Console.WriteLine("Id não encontrado, tente novamente");
                            break;
                        }
                
                        Console.WriteLine("-------------------");
                        Produto.ListarProdutos();


                        // escolha do parametro a ser alterado
                        Console.WriteLine("Selecione o parâmetro que você deseja alterar");
                        Console.Write(" | 1 - Data \n | 2 - Valor \n | Digite aqui: ");
                        string? parametro = Console.ReadLine().Replace(" ", "");
                        switch (parametro)
                        {
                            // data
                            case "1":
                                Console.Write("Digite a nova data: ");
                                string novaData = Console.ReadLine().Trim();
                                if (string.IsNullOrWhiteSpace(novaData))
                                {
                                    Console.WriteLine("Data inválida, tente novamente");
                                    break;
                                }
                                novaData = char.ToUpper(novaData[0]) + novaData.Substring(1).ToLower();
                                await Produto.AtualizarPedido(id, "Data", novaData);
                                break;

                            // valor
                            case "2":
                                Console.Write("Digite o novo valor: ");
                                decimal novoValor;
                                try
                                {
                                    novoValor = decimal.Parse(Console.ReadLine()!);
                                    if (novoValor < 0)
                                    {
                                        Console.WriteLine("Valor inválido, tente novamente");
                                        break;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Preço inválido, tente novamente");
                                    break;
                                }
                                await Produto.AtualizarPedido(id, "Valor", novoValor);
                                break;

                            default:
                                Console.WriteLine("Opção inválida. Tente novamente.");
                                break;
                        }
                        break;

                    // Excluir pedido
                    case "5":
                    while (true)
                        {
                            Console.WriteLine("-------------------");
                            Pedido.ListarPedidos();
                            Console.Write("Digite o id do pedido que você quer atualizar: ");
                            int id = int.Parse(Console.ReadLine().Trim());
                            
                            // validacao
                            if (id <= 0)
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine("Informe um id válido");
                                Console.WriteLine("-------------------");
                            }
                            bool Booleano1 = await Pedido.ValidarId(id);
                            if (!Booleano1)
                            {
                                Console.WriteLine("Id não encontrado, tente novamente");
                                break;
                            }
                            
                        }

                    
                    // validacao
                    default:
                        Console.WriteLine("Valor inválido, tente novamente");
                        break;
                }
            }

        default:
            Console.WriteLine("Valor inválido, tente novamente");
            break;
            




    }
















       
}

    










