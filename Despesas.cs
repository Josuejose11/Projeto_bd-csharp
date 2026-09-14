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
    }
}
