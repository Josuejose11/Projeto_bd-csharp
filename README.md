# Projeto_bd-csharp

Projeto de Programação Orientada a Objetos (POO) em C# integrado com um banco de dados, desenvolvido como controle de despesas.

## Integrantes

- *(Gustavo, Josué e Gabriel Lopes)*

## Banco de dados utilizado

- **MySQL**
- Banco: `controle_despesas`
- Tabela principal: `despesas` (`id_dps`, `descricao_dps`, `valor_dps`, `categoria_dps`, `data_dps`)

O script de criação do banco está em [`DB-C#.sql`](./DB-C%23.sql).

## Biblioteca/driver utilizado

- **[MySqlConnector](https://www.nuget.org/packages/MySqlConnector)** — driver ADO.NET para conexão com MySQL a partir do C#.

## Como instalar as dependências

Com o [.NET SDK](https://dotnet.microsoft.com/download) instalado (o projeto usa `net10.0`), rode na raiz do repositório:

```bash
dotnet restore
dotnet add package MySqlConnector
```

## Como configurar o banco

Tenha o mysql aberto no seu dispositivo

## Como executar o projeto

```bash
dotnet run
```

## Como funciona a conexão

O projeto utiliza o driver ADO.NET (`MySqlConnector`) para se comunicar com o MySQL. O fluxo básico é:

1. Uma `MySqlConnection` é criada a partir da connection string configurada.
2. A conexão é aberta com `connection.Open()`.
3. Comandos SQL (`SELECT`, `INSERT`, `UPDATE`, `DELETE`) são executados na tabela `despesas` através de objetos `MySqlCommand`, geralmente usando parâmetros para evitar SQL Injection.
4. Os resultados são lidos com um `MySqlDataReader` (para consultas) ou o número de linhas afetadas é retornado (para operações de escrita).
5. A conexão é fechada/descartada ao final (idealmente com `using`, para garantir o fechamento automático).

Esse fluxo é organizado em classes do projeto (camada de acesso a dados), seguindo os princípios de POO para separar a lógica de negócio da lógica de persistência.
