// CONEXAO SQL 
using System.Runtime.InteropServices.Marshalling;
using MySqlConnector;

string connectionString = """
    Server=127.0.0.1;
    Port=3306;
    Database=controle_despesas;
    User ID=root;
    Password=Senac2026;
    """;

using var connection = new MySqlConnection(connectionString);

void denovo()
{
    while (true)
    {
        Console.Write("Deseja realizar outra operação? \n | 1 - Sim \n | 2 - Não \n | Escreva aqui: ");
        string resposta = Console.ReadLine().Trim();
        if (resposta == "1")
        {
            break;
        }
        else if (resposta == "2")
        {
            Console.WriteLine("Você saiu!");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.WriteLine("---------------");
            continue;
        }
    
    }
}




// MENU //
Console.WriteLine("Bem-vindo ao Controle de Despesas!");
var despesa = new Despesa();
while (true)
{   
    Console.WriteLine("Você pode utilizar nosso sistema da maneira que desejar \nPara isso, escolha uma das opções abaixo:");    
    Console.WriteLine(" | 0 - Sair");
    Console.WriteLine(" | 1 - Cadastrar despesa");
    Console.WriteLine(" | 2 - Listar todas as despesas");
    Console.WriteLine(" | 3 - Atualizar despesa");
    Console.WriteLine(" | 4 - Excluir despesa");
    Console.Write(" | Escreva aqui: ");

    switch (Console.ReadLine())
    {
        case "0":
            Console.WriteLine("Saindo do programa...");
            Environment.Exit(0);
            break;
        case "1":
            despesa.CadastrarDespesa(connection);
            denovo();
            break;
        case "2":
            despesa.LerDespesas(connection);
            denovo();
            break;
        case "3":
            // AtualizarDespesa(connection);
            denovo();
            break;
        case "4":
            // ExcluirDespesa(connection);
            denovo();
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.WriteLine("---------------");
            continue;
    }
}