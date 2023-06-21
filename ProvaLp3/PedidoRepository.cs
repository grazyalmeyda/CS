using ProvaLp3.Database;
using ProvaLp3.Models;
using Microsoft.Data.Sqlite;
namespace ProvaLp3.Repositories;
class PedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public PedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Pedido> Listar()
    {
        var Pedidos = new List<Pedido>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedido";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var CodPedido = reader.GetInt32(0);
            var PrazoEntrega = reader.GetDateTime(1);
            var DataPedido = reader.GetDateTime(2);
            var PedidoCodCliente = reader.GetInt32(3);
            var PedidoCodVendedor = reader.GetInt32(4);
            var Pedido = ReaderToPedidos(reader);
            Pedidos.Add(Pedido);
        }

        connection.Close();

        return Pedidos;
    }

    public Pedido Inserir(Pedido pedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Pedido VALUES($CodPedido, $PrazoEntrega, $DataPedido, $PedidoCodCliente, $PedidoCodVendedor)";
        command.Parameters.AddWithValue("$CodPedido", pedido.CodPedido);
        command.Parameters.AddWithValue("$PrazoEntrega", pedido.PrazoEntrega);
        command.Parameters.AddWithValue("$DataPedido", pedido.DataPedido);
        command.Parameters.AddWithValue("$PedidoCodCliente", pedido.PedidoCodCliente);
        command.Parameters.AddWithValue("$PedidoCodVendedor", pedido.PedidoCodVendedor);

        command.ExecuteNonQuery();
        connection.Close();

        return pedido;
    }

    public bool Apresentar(int CodPedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(CodPedido) FROM Pedido WHERE (CodPedido = $CodPedido)";
        command.Parameters.AddWithValue("$CodPedido", CodPedido);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Pedido GetById(int CodPedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedido WHERE (CodPedido = $CodPedido)";
        command.Parameters.AddWithValue("$CodPedido", CodPedido);

        var reader = command.ExecuteReader();
        reader.Read();

        var pedido = ReaderToPedidos(reader);

        connection.Close();

        return pedido;
    }

    private Pedido ReaderToPedidos(SqliteDataReader reader)
    {
        var Pedido = new Pedido(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetInt32(3), reader.GetInt32(4));

        return Pedido;
    }
}