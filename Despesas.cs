using MySqlConnector;

class Despesa
{
    private decimal valor;
    private string? descricao;
    private string categoria;
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


        /////////////
        // MÉTODOS // 
        /////////////

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
            connection.Close();
            return;
        }

        Console.WriteLine("Despesas cadastradas:");
        foreach (var d in lista)
        {
            Console.WriteLine($" - {d.Descricao}: R$ {d.Valor:F2} ({d.Categoria}) - {d.Data:dd/MM/yyyy}");
        }
    
        connection.Close();
        
    }

    // metodo para salvar no banco de dados
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

    // metodo para cadastrar despesa, pegar os dados do usuario
    public void CadastrarDespesa(MySqlConnection connection)
    {
        // Valor da despesa
        while (true)
        {
            Console.WriteLine();
            Console.Write("Digite o valor da despesa (Em reais, e apenas números): ");
            decimal valorDespesa = 0;
            try
            {
                valorDespesa = decimal.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de valor inválido. Tente novamente.");
                continue;
            }

            // validacao
            if (string.IsNullOrWhiteSpace(valorDespesa.ToString()) || valorDespesa <= 0)
            {
                Console.WriteLine("Digite um valor válido. Tente novamente.");
                continue;
            }

            if (this.Valor <= 0)
            {
                Console.WriteLine("O valor da despesa não pode ser negativo ou igual a zero. Tente novamente.");
                continue;
            }
            
            //  define o valor do atributo
            try
            {
                this.Valor = valorDespesa;
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de valor inválido. Tente novamente.");
                continue;
            }
           
            break;
        }

        // categoria da despesa
        while (true)
        {
            // Lista de categorias válidas
            string[] categoriasValidas = ["alimentação", "alimentacao", "alimentaçao", "alimentacão", "transporte", "saúde", "saude", "educação", "educaçao", "educacão", "lazer"];
            
            Console.WriteLine();
            Console.Write("Digite a categoria da despesa (Alimentação, Transporte, Saúde, Educação ou Lazer): ");
            string categoria = Console.ReadLine();

            // validacao
            if (string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("Valor inválido, Tente novamente.");
                continue;
            }
            
            if (categoriasValidas.Contains(categoria.ToLower()))
            {
                this.Categoria = categoria;
            }
            else
            {
                Console.WriteLine("Categoria inválida. As categorias válidas são: Alimentação, Transporte, Saúde, Educação, Lazer.\nTente novamente.");
                continue;
            }
            break;
        }

        // Descrição da despesa
        while (true)
        {
            Console.WriteLine();
            Console.Write("Digite a descrição da despesa (Digite 0 caso não queira informar): ");
            string descricao = Console.ReadLine();
            if (descricao == "")    
            {
                Console.WriteLine("Descrição inválida. Tente novamente.");
                continue;
            }
            this.Descricao = descricao == "0" ? null : descricao;
            break;
        }

        // Data da despesa
        while (true)
        {
            Console.WriteLine();
            Console.Write("Digite a data da despesa (formato: yyyy-MM-dd): ");
            try
            {
                DateTime data = DateTime.Parse(Console.ReadLine());
                this.Data = data ;
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de data inválido. Tente novamente.");
                continue;
            }
        }

        this.SalvarDespesa(connection);
        Console.WriteLine("Despesa cadastrada com sucesso!");
        
       
    }
}


