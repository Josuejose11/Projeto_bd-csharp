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

try
{
    connection.Open();
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}


// MENU //
Console.WriteLine("Bem-vindo ao Controle de Despesas!");
Console.WriteLine("Escolha uma opção:");    
Console.WriteLine("0 - Sair");
Console.WriteLine("1 - Cadastrar despesa");
Console.WriteLine("2 - Listar despesas");
Console.WriteLine("3 - Atualizar despesas");
Console.WriteLine("4 - Excluir despesas");



