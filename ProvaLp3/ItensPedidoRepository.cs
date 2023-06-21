using ProvaLp3.Database;
using ProvaLp3.Models;
using Microsoft.Data.Sqlite;
namespace ProvaLp3.Repositories;

class ItensPedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ItensPedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<ItensPedido> Listar()
    {
        var itensPedidos = new List<ItensPedido>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var coditempedido = reader.GetInt32(0);
            var itempedidocodpedido = reader.GetString(1);
            var itempedidocodproduto = reader.GetString(2);
            var quantidade = reader.GetString(3);
            var itemPedido = ReaderToItensPedidos(reader);
            itensPedidos.Add(itemPedido);
        }

        connection.Close();

        return itensPedidos;

    }

    public ItensPedido Inserir(ItensPedido itensPedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO ItensPedido VALUES($coditempedido, $itempedidocodpedido, $itempedidocodproduto, $quantidade)";
        command.Parameters.AddWithValue("$coditempedido", itensPedido.CodItemPedido);
        command.Parameters.AddWithValue("$itempedidocodpedido", itensPedido.ItemPedidoCodPedido);
        command.Parameters.AddWithValue("$itempedidocodproduto", itensPedido.ItemPedidoCodProduto);
        command.Parameters.AddWithValue("$quantidade", itensPedido.Quantidade);

        command.ExecuteNonQuery();
        connection.Close();

        return itensPedido;
    }

    public bool Apresentar(int itensPedidosid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodItemPedido) FROM ItensPedido WHERE (CodItemPedido = $coditempedido)";
        command.Parameters.AddWithValue("$coditempedido", itensPedidosid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public ItensPedido GetById(int itensPedidosid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido WHERE (CodItemPedido = $coditempedido)";
        command.Parameters.AddWithValue("$coditempedido", itensPedidosid);

        var reader = command.ExecuteReader();
        reader.Read();

        var itemPedido = ReaderToItensPedidos(reader);

        connection.Close();

        return itemPedido;
    }

    private ItensPedido ReaderToItensPedidos(SqliteDataReader reader)
    {
        var ItemPedido = new ItensPedido(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));

        return ItemPedido;
    }
}