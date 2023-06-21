using ProvaLp3.Database;
using ProvaLp3.Models;
using Microsoft.Data.Sqlite;
namespace ProvaLp3.Repositories;
class ClienteRepository {
    private readonly DatabaseConfig _databaseConfig;
    public ClienteRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Cliente> Listar()
    {
        var clientes = new List<Cliente>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Cliente";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var codcliente = reader.GetInt32(0);
            var nome = reader.GetString(1);
            var endereco = reader.GetString(2);
            var cidade = reader.GetString(3);
            var cep = reader.GetString(4);
            var uf = reader.GetString(5);
            var ie = reader.GetString(6);
            var cliente =  ReaderToCliente(reader);
            clientes.Add(cliente);
        }

        connection.Close();
        
        return clientes;
    }

    public Cliente Inserir(Cliente cliente)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Cliente VALUES($codcliente, $nome, $endereco, $cidade, $cep, $uf, $ie)";
        command.Parameters.AddWithValue("$codcliente", cliente.CodCliente);
        command.Parameters.AddWithValue("$nome", cliente.Nome);
        command.Parameters.AddWithValue("$endereco", cliente.Endereco);
        command.Parameters.AddWithValue("$cidade", cliente.Cidade);
        command.Parameters.AddWithValue("$cep", cliente.Cep);
        command.Parameters.AddWithValue("$uf", cliente.Uf);
        command.Parameters.AddWithValue("$ie", cliente.Ie);

        command.ExecuteNonQuery();
        connection.Close();

        return cliente;
    }
    public bool Apresentar(int codcliente)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codcliente) FROM Cliente WHERE (codcliente = $codcliente)";
        command.Parameters.AddWithValue("$codcliente", codcliente);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Cliente GetById(int codcliente)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Cliente WHERE (codcliente = $codcliente)";
        command.Parameters.AddWithValue("$codcliente", codcliente);

        var reader = command.ExecuteReader();
        reader.Read();

        var cliente = ReaderToCliente(reader);

        connection.Close(); 

        return cliente;
    }
    private Cliente ReaderToCliente(SqliteDataReader reader)
    {
        var cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));

        return cliente;
    }
}