using ProvaLp3.Database;
using ProvaLp3.Models;
using Microsoft.Data.Sqlite;
namespace ProvaLp3.Repositories;

class VendedorRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public VendedorRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    
    public List<Vendedor> Listar()
    {
        var Vendedor = new List<Vendedor>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedor";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var CodVendedor = reader.GetInt32(0);
            var Nome = reader.GetString(1);
            var SalarioFixo = reader.GetDecimal(2);
            var FaixaComissao = reader.GetString(3);


            var vendedor = ReaderToVendedor(reader);
            Vendedor.Add(vendedor);
        }

        connection.Close();

        return Vendedor;
    }

    public Vendedor Inserir(Vendedor vendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Vendedor VALUES($codvendedor, $nome, $salariofixo, $faixacomissao)";
        command.Parameters.AddWithValue("$codvendedor", vendedor.CodVendedor);
        command.Parameters.AddWithValue("$nome", vendedor.Nome);
        command.Parameters.AddWithValue("$salariofixo", vendedor.SalarioFixo);
        command.Parameters.AddWithValue("$faixacomissao", vendedor.FaixaComissao);

        command.ExecuteNonQuery();
        connection.Close();

        return vendedor;
    }

    public bool Apresentar(int CodVendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codvendedor) FROM Vendedor WHERE (CodVendedor = $codvendedor)";
        command.Parameters.AddWithValue("$codvendedor", CodVendedor);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Vendedor GetById(int CodVendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedor WHERE (CodVendedor = $codvendedor)";
        command.Parameters.AddWithValue("$codvendedor", CodVendedor);

        var reader = command.ExecuteReader();
        reader.Read();

        var vendedor = ReaderToVendedor(reader);

        connection.Close();

        return vendedor;
    }

    private Vendedor ReaderToVendedor(SqliteDataReader reader)
    {
        var vendedor = new Vendedor(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3));

        return vendedor;
    }
}