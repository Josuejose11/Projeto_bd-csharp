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

// def denovo 
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

// executar o banco de dados
string createDatabaseQuery = "CREATE DATABASE IF NOT EXISTS controle_despesas";
using (var command = new MySqlCommand(createDatabaseQuery, connection))
{
    connection.Open();
    command.ExecuteNonQuery();
    connection.Close();
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
    Console.WriteLine(" | 3 - Buscar despesa");
    Console.WriteLine(" | 4 - Atualizar despesa");
    Console.WriteLine(" | 5 - Excluir despesa");
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
            despesa.BuscarDespesa(connection);
            denovo();
            break;
        case "4":
            // AtualizarDespesa(connection);
            denovo();
            break;
        case "5":
            // ExcluirDespesa(connection);
            denovo();
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.WriteLine("---------------");
            continue;
    }
}