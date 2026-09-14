// CONEXAO SQL 
using MySqlConnector;

string connectionString = """
    Server=127.0.0.1;
    Port=3306;
    Database=controle_despesas;
    User ID=root;
    Password=Senac2026;
    """;

using var connection = new MySqlConnection(connectionString);
                 


// MENU //
Console.WriteLine("Bem-vindo ao Controle de Despesas!");
var despesa = new Despesa();
while (true)
{   
    Console.WriteLine("Escolha uma opção:");    
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
            break;
        case "1":
            despesa.CadastrarDespesa(connection);
            break;
        case "2":
            despesa.LerDespesas(connection);
            break;
        case "3":
            // AtualizarDespesa(connection);
            break;
        case "4":
            // ExcluirDespesa(connection);
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}