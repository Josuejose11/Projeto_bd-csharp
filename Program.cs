// CONEXAO SQL 
using MySqlConnector;

string connectionString = """
    Server=127.0.0.1;
    Port=3306;
    Database=controle_despesa;
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