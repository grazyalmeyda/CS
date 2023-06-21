using ProvaLp3.Database;
using ProvaLp3.Models;
using Microsoft.Data.Sqlite;
namespace ProvaLp3.Repositories;

class ProdutoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ProdutoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Produto> Listar()
    {
        var Produtos = new List<Produto>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Produto";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var CodProduto = reader.GetInt32(0);
            var Descricao = reader.GetString(1);
            var ValorUnitario = reader.GetDecimal(2);


            var Produto = ReadertoProdutos(reader);
            Produtos.Add(Produto);
        }

        connection.Close();

        return Produtos;
    }

    public Produto Inserir(Produto produto)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Produto VALUES($codproduto, $descricao, $valorunitario)";
        command.Parameters.AddWithValue("$codproduto", produto.CodProduto);
        command.Parameters.AddWithValue("$descricao", produto.Descricao);
        command.Parameters.AddWithValue("$valorunitario", produto.ValorUnitario);

        command.ExecuteNonQuery();
        connection.Close();

        return produto;
    }

    public bool Apresentar(int CodProduto)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codproduto) FROM Produto WHERE (CodProduto = $codproduto)";
        command.Parameters.AddWithValue("$codproduto", CodProduto);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Produto GetById(int CodProduto)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Produto WHERE (CodProduto = $CodProduto)";
        command.Parameters.AddWithValue("$CodProduto", CodProduto);

        var reader = command.ExecuteReader();
        reader.Read();

        var produto = ReadertoProdutos(reader);

        connection.Close();

        return produto;
    }

    private Produto ReadertoProdutos(SqliteDataReader reader)
    {
        var Produto = new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));

        return Produto;
    }
}