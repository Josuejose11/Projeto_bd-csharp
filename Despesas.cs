using MySqlConnector;

class Despesa
{
    private decimal valor;
    private string descricao= "";
    private string categoria= "";
    private DateTime data;

//CONSTRUTOR
    // Com parametros
    public Despesa(decimal valor, string descricao, string categoria, DateTime data)
    {
        this.valor = valor;
        this.descricao = descricao;
        this.categoria = categoria;
        this.data = data;
    }
    // Sem parametros
    public Despesa()
    {
    }

// GET E SET 
    public decimal Valor
    {
        get { return valor; }
        set 
        { 
            if (value < 0)
            {
                Console.WriteLine("O valor da despesa não pode ser negativo.");
            }
            else{valor = value;}
        }
    }
    public string Descricao 
    {
        get { return descricao; }
        set 
        {
            if (value.Length > 0)
            { 
                if (value.Length > 300)
                {
                    Console.WriteLine("A descrição da despesa não pode ter mais de 300 caracteres.");
                }
                else
                {
                    descricao = value;
                }
            }
            else
            {
                Console.WriteLine("A descrição da despesa não pode ser vazia.");
            }
        }
    }
    public string Categoria 
    {
        get { return categoria; }
        set 
        { 
            string[] categoriasValidas = { "alimentação","alimentacao","alimentaçao","alimentacão", "transporte", "saúde","saude", "educação","educaçao","educacão", "lazer" };
            if (categoriasValidas.Contains(value.ToLower()))
            {
                categoria = value;
            }
            else
            {
                Console.WriteLine("Categoria inválida. As categorias válidas são: Alimentação, Transporte, Saúde, Educação, Lazer, Outros.\nTente novamente.");
            }
        }
    }
    public DateTime Data 
    {
        get { return data; }
        set { data = value; }
    }


// MÉTODOS 

    // Ler despesas do banco de dados
    public void LerDespesas(MySqlConnection connection)
    {
        connection.Open();

        var lista = new List<Despesa>();
        string sql = "SELECT * FROM despesas";
                                 
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var despesa = new Despesa();
            despesa.Valor = reader.GetDecimal("valor");
            despesa.Descricao = reader.GetString("descricao");
            despesa.Categoria = reader.GetString("categoria");
            despesa.Data = reader.GetDateTime("data");
            lista.Add(despesa);
        }
    
        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhuma despesa cadastrada.");
            return;
        }

        Console.WriteLine("Despesas cadastradas:");
        foreach (var d in lista)
        {
            Console.WriteLine($" - {d.Descricao}: R$ {d.Valor:F2} ({d.Categoria}) - {d.Data:dd/MM/yyyy}");
        }
    
        connection.Close();
        
    }




    public void SalvarDespesa(MySqlConnection connection)
    {
        connection.Open();

        string sql = "INSERT INTO despesas (valor, descricao, categoria, data) VALUES (@valor, @descricao, @categoria, @data)";
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@valor", this.valor);
        command.Parameters.AddWithValue("@descricao", this.descricao);
        command.Parameters.AddWithValue("@categoria", this.categoria);
        command.Parameters.AddWithValue("@data", this.data);
        command.ExecuteNonQuery();

        connection.Close();
    }


    public void CadastrarDespesa(MySqlConnection connection)
    {
        connection.Open();

        Console.WriteLine();
        Console.Write("Digite o valor da despesa (Em reais, e apenas números): ");
        this.Valor = Convert.ToDecimal(Console.ReadLine());
        if (this.Valor <= 0)
        {
            Console.WriteLine("O valor da despesa não pode ser negativo ou igual a zero. Tente novamente.");
            return;
        }
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Digite apenas números para o valor da despesa.");
            return;
        }

        Console.WriteLine();
        Console.Write("Digite a descrição da despesa: ");
        this.Descricao = Console.ReadLine();

        Console.WriteLine();
        Console.Write("Digite a categoria da despesa (Alimentação, Transporte, Saúde, Educação ou Lazer): ");
        this.Categoria = Console.ReadLine();

        Console.WriteLine();
        Console.Write("Digite a data da despesa (formato: yyyy-MM-dd): ");
        this.Data = DateTime.Parse(Console.ReadLine());

        this.SalvarDespesa(connection);
        Console.WriteLine("Despesa cadastrada com sucesso!");

        connection.Close();
    }
}
