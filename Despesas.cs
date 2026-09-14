class Despesa
{
    private decimal valor;
    private string descricao;
    private string categoria;
    private DateTime data;

    public Despesa(decimal valor, string descricao, string categoria, DateTime data)
    {
        this.valor = valor;
        this.descricao = descricao;
        this.categoria = categoria;
        this.data = data;
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
        set { categoria = value; }
    }
    public DateTime Data 
    {
        get { return data; }
        set { data = value; }
    }
}
