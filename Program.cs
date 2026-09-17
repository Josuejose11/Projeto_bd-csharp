// CONEXÃO SQL
using MySqlConnector;

// Conexão inicial: NÃO informa o banco
string connectionString = """
    Server=127.0.0.1;
    Port=3306;
    User ID=root;
    Password=Senac2026;
    """;

using var connectioninicial  = new MySqlConnection(connectionString);

connectioninicial.Open();
// Cria o banco
try
{
    string sqlBanco = """
        CREATE DATABASE IF NOT EXISTS controle_despesas;
        USE controle_despesas;
        CREATE TABLE IF NOT EXISTS despesas (
            id_dps INT AUTO_INCREMENT PRIMARY KEY,
            titulo_dps VARCHAR(150),
            descricao_dps VARCHAR(300),
            valor_dps DECIMAL(10,2),
            categoria_dps VARCHAR(150),
            data_dps DATETIME
        );
        """;

    using (var commandBanco = new MySqlCommand(sqlBanco, connectioninicial))
    {
        commandBanco.ExecuteNonQuery();
    }
}
finally
{
    connectioninicial.Close();
}

// Agora conecta ao banco que acabou de ser criado
string connectionStringBanco = """
    Server=127.0.0.1;
    Port=3306;
    Database=controle_despesas;
    User ID=root;
    Password=Senac2026;
    """;

using var connection = new MySqlConnection(connectionStringBanco);

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

    switch (Console.ReadLine().Replace(" ", ""))
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
            despesa.ExcluirDespesa(connection);
            denovo();
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.WriteLine("---------------");
            continue;
    }
}