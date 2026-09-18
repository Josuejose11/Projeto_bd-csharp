using MySqlConnector;

class Despesa
{
    private int id;
    private decimal valor;
    private string? descricao;
    private string titulo;
    private string categoria;
    private DateTime data;

//CONSTRUTOR
    // Com parametros
    public Despesa(int id, decimal valor,  string descricao, string titulo, string categoria, DateTime data)
    {
        this.id = id;
        this.valor = valor;
        this.descricao = descricao;
        this.titulo = titulo;
        this.categoria = categoria;
        this.data = data;
    }
    // Sem parametros
    public Despesa()
    {
    }

// GET E SET 
    private int Id
    {
        get { return id; }
        set { id = value; }
    }
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
            if (string.IsNullOrEmpty(value))
            {
                descricao = null;
            }
            else if (value.Length > 0)
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
    public string Titulo
    {
        get { return titulo; }
        set 
        { 
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("O título da despesa não pode ser vazio.");
            }
            else
            {
                titulo = value;
            }
        }
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
            despesa.Id = reader.GetInt32("id_dps");
            despesa.Valor = reader.GetDecimal("valor_dps");
            despesa.Titulo = reader.GetString("titulo_dps");
            despesa.Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_dps")) ? null : reader.GetString("descricao_dps");
            despesa.Categoria = reader.GetString("categoria_dps");
            despesa.Data = reader.GetDateTime("data_dps");
            lista.Add(despesa);
        }
    
        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhuma despesa cadastrada.");
            Console.WriteLine("---------------");
            connection.Close();
            return;
        }

        Console.WriteLine("Despesas cadastradas:");
        foreach (var d in lista)
        {
            Console.WriteLine($" Id: {d.Id} | Título: {d.Titulo} | Valor: R$ {d.Valor:F2} | Categoria: {d.Categoria} | Data: {d.Data:dd/MM/yyyy} | Descrição: {d.Descricao}");
        }
        Console.WriteLine("================");
        connection.Close();
    }

    // metodo para salvar no banco de dados
    public void SalvarDespesa(MySqlConnection connection)
    {
        connection.Open();

        string sql = "INSERT INTO despesas (valor_dps, titulo_dps, descricao_dps, categoria_dps, data_dps) VALUES (@valor, @titulo, @descricao, @categoria, @data)";
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@valor", this.valor);
        command.Parameters.AddWithValue("@titulo", this.titulo);
        command.Parameters.AddWithValue("@descricao", this.descricao);
        command.Parameters.AddWithValue("@categoria", this.categoria);
        command.Parameters.AddWithValue("@data", this.data);
        command.ExecuteNonQuery();

        connection.Close();
        SalvarId(connection);


    }

    // metodo para cadastrar despesa, pegar os dados do usuario
    public void CadastrarDespesa(MySqlConnection connection)
    {
        // Valor da despesa
        while (true)
        {
            Console.Write("Digite o valor da despesa (Em reais, e apenas números): ");
            decimal valorDespesa = 0;
            try
            {
                valorDespesa = decimal.Parse(Console.ReadLine().Replace(",", ".").Replace(" ", ""));
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de valor inválido. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }

            // validacao
            if (string.IsNullOrWhiteSpace(valorDespesa.ToString()) || valorDespesa <= 0)
            {
                Console.WriteLine("Digite um valor válido. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }

            if (valorDespesa <= 0)
            {
                Console.WriteLine("O valor da despesa não pode ser negativo ou igual a zero. Tente novamente.");
                Console.WriteLine("---------------");
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

        // Titulo da despesa
        while (true)
        {
            Console.Write("Digite o título da despesa: ");
            string titulo = Console.ReadLine().Trim();
            if (titulo == "")    
            {
                Console.WriteLine("Título inválido. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }
            this.Titulo = titulo; 
            break;
        }

        // categoria da despesa
        while (true)
        {
            // Lista de categorias válidas
            string[] categoriasValidas = ["alimentação", "alimentacao", "alimentaçao", "alimentacão", "transporte", "saúde", "saude", "educação", "educaçao", "educacão", "lazer"];
            
            Console.Write("Digite a categoria da despesa (Alimentação, Transporte, Saúde, Educação ou Lazer): ");
            string categoria = Console.ReadLine().ToLower().Replace(" ", "");

            // validacao
            if (string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("Valor inválido, Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }
            
            if (categoriasValidas.Contains(categoria.ToLower()))
            {
                this.Categoria = categoria;
            }
            else
            {
                Console.WriteLine("Categoria inválida. As categorias válidas são: Alimentação, Transporte, Saúde, Educação, Lazer.\nTente novamente.");
                Console.WriteLine("---------------");
                continue;
            }
            break;
        }

        // descricao da despesa
        while (true)
        {
            Console.Write("Digite a descrição da despesa (Digite 0 caso não queira informar): ");
            string descricao = Console.ReadLine()?.Trim();
            if (descricao?.Length > 300)
            {
                Console.WriteLine("A descrição da despesa não pode ter mais de 300 caracteres. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }
            else if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("Descrição inválida. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }
            
            this.Descricao = descricao == "0" ? null : descricao;
            break;
        }

        // data da despesa
        while (true)
        {
            Console.Write("Digite a data da despesa (formato: yyyy-MM-dd): ");
            try
            {
                DateTime data = DateTime.Parse(Console.ReadLine().Trim());
                this.Data = data ;
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de data inválido. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }
        }

        this.SalvarDespesa(connection);
        Console.WriteLine("Despesa cadastrada com sucesso!");
    }

    // salva o id
    private void SalvarId(MySqlConnection connection)
    {
        connection.Open();

        string sql = "SELECT LAST_INSERT_ID()";
        using var command = new MySqlCommand(sql, connection);
        this.Id = Convert.ToInt32(command.ExecuteScalar());

        connection.Close();
    }

 // metodo para buscar despesa por id
    public void BuscarDespesa(MySqlConnection connection)
    {
        while (true){
            Console.Write("Digite o nome da despesa que deseja buscar: ");
            string nome = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido, Tente novamente.");
                Console.WriteLine("---------------");
                break;
            }

            connection.Open();

            string sql = "SELECT * FROM despesas WHERE titulo_dps LIKE @busca ORDER BY titulo_dps;";
            using var command = new MySqlCommand(sql, connection);
            if (command.Parameters.AddWithValue("@busca", $"%{nome}%") == null)
            {
                Console.WriteLine("A despesa não foi encontrada.");
                connection.Close();
                return;
            }

            using var reader = command.ExecuteReader();
            var lista = new List<Despesa>();

   
            while (reader.Read())
            {
                var despesa = new Despesa();
                despesa.Id = reader.GetInt32("id_dps");
                despesa.Valor = reader.GetDecimal("valor_dps");
                despesa.Titulo = reader.GetString("titulo_dps");
                despesa.Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_dps")) ? null : reader.GetString("descricao_dps");
                despesa.Categoria = reader.GetString("categoria_dps");
                despesa.Data = reader.GetDateTime("data_dps");
                lista.Add(despesa);
            }
        
            if (lista.Count == 0)
            {
                Console.WriteLine($"Não encontramos nenhuma despesa com '{nome}'.");
                Console.WriteLine("---------------");
                connection.Close();
                return;
            }

            Console.WriteLine("Despesas cadastradas:");
            foreach (var d in lista)
            {
                Console.WriteLine($" Id: {d.Id} | Título: {d.Titulo} | Valor: R$ {d.Valor:F2} | Categoria: {d.Categoria} | Data: {d.Data:dd/MM/yyyy}");
            }
            Console.WriteLine("================");
            connection.Close();  

        }
    }
   
    //exclui despesa
    public void ExcluirDespesa(MySqlConnection connection)
    {
        LerDespesas(connection);

        while (true)
        {
            Console.Write("Digite o ID da despesa que deseja excluir: ");

            int id;

            try
            {
                id = int.Parse(Console.ReadLine().Trim());
            }
            catch (FormatException)
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                Console.WriteLine("---------------");
                continue;
            }

            connection.Open();

            // Verifica se a despesa existe antes de tentar excluir
            string sql = "SELECT * FROM despesas WHERE id_dps = @id";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                // exclui despesa
                connection.Close();

                connection.Open();

                string sqlExcluir = "DELETE FROM despesas WHERE id_dps = @id";

                using var commandExcluir = new MySqlCommand(sqlExcluir, connection);
                commandExcluir.Parameters.AddWithValue("@id", id);
                commandExcluir.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine("===============");
                Console.WriteLine("Despesa excluída com sucesso!");
                Console.WriteLine("===============");
                break;
            }
            // caso nao tenha encontrado a despesa, informa o usuario e pede para tentar novamente
            else
            {
                connection.Close();

                Console.WriteLine("Despesa não encontrada. ");
                Console.WriteLine("---------------");
                while (true)
                {
                    Console.Write("Deseja tentar novamente? \n | 1 - Sim \n | 2 - Não \n | Escreva aqui: ");
                    string resposta = Console.ReadLine().Trim();
                    if (resposta == "1")
                    {
                        Console.WriteLine("================");
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
        }
    }

    public void AtualizarDespesa(MySqlConnection connection)
{
    LerDespesas(connection);

    while (true)
    {
        Console.Write("Digite o ID da despesa que deseja atualizar: ");
        int id;

        try
        {
            id = int.Parse(Console.ReadLine().Trim());
        }
        catch (FormatException)
        {
            Console.WriteLine("ID inválido. Tente novamente.");
            Console.WriteLine("---------------");
            continue;
        }

        connection.Open();

        string sql = "SELECT * FROM despesas WHERE id_dps = @id";

        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var despesa = new Despesa();

            despesa.Id = reader.GetInt32("id_dps");
            despesa.Valor = reader.GetDecimal("valor_dps");
            despesa.Titulo = reader.GetString("titulo_dps");
            despesa.Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_dps")) ? null : reader.GetString("descricao_dps");
            despesa.Categoria = reader.GetString("categoria_dps");
            despesa.Data = reader.GetDateTime("data_dps");

            connection.Close(); // Fecha a conexão do SELECT antes de ler os novos dados

            Console.WriteLine("\n--- Atualização de Dados ---");
            Console.WriteLine("Dica: Deixe em branco e aperte Enter para manter o valor atual.");

            // 1. Atualizar Valor
            Console.Write($"Novo valor (Atual: {despesa.Valor}): ");
            string inputValor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputValor))
            {
                despesa.Valor = decimal.Parse(inputValor.Replace(",", "."));
            }

            // 2. Atualizar Título
            Console.Write($"Novo título (Atual: {despesa.Titulo}): ");
            string inputTitulo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputTitulo))
            {
                despesa.Titulo = inputTitulo;
            }

            // 3. Atualizar Categoria
            Console.Write($"Nova categoria (Atual: {despesa.Categoria}): ");
            string inputCategoria = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputCategoria))
            {
                despesa.Categoria = inputCategoria;
            }

            // 4. Atualizar Descrição
            Console.Write($"Nova descrição (Atual: {despesa.Descricao ?? "Nenhuma"}) [Digite 0 para apagar]: ");
            string inputDescricao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputDescricao))
            {
                despesa.Descricao = inputDescricao == "0" ? null : inputDescricao;
            }

            // 5. Atualizar Data
            Console.Write($"Nova data (Atual: {despesa.Data:yyyy-MM-dd}): ");
            string inputData = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputData))
            {
                despesa.Data = DateTime.Parse(inputData);
            }

            // UPDATE
            connection.Open();

            string sqlUpdate = @"
                UPDATE despesas
                SET 
                    valor_dps = @valor,
                    titulo_dps = @titulo,
                    descricao_dps = @descricao,
                    categoria_dps = @categoria,
                    data_dps = @data
                WHERE id_dps = @id";

            using var commandUpdate = new MySqlCommand(sqlUpdate, connection);

            commandUpdate.Parameters.AddWithValue("@valor", despesa.Valor);
            commandUpdate.Parameters.AddWithValue("@titulo", despesa.Titulo);
            commandUpdate.Parameters.AddWithValue("@descricao", despesa.Descricao);
            commandUpdate.Parameters.AddWithValue("@categoria", despesa.Categoria);
            commandUpdate.Parameters.AddWithValue("@data", despesa.Data);
            commandUpdate.Parameters.AddWithValue("@id", despesa.Id);

            commandUpdate.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Despesa atualizada com sucesso!\n");
            break;
        }
        else
        {
            connection.Close();
            Console.WriteLine("Despesa não encontrada. Tente novamente.");
            Console.WriteLine("---------------");
            continue;
        }
    }
}
}


