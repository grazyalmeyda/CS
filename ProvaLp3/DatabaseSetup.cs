using Microsoft.Data.Sqlite;

namespace ProvaLp3.Database;

class DatabaseSetup {
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateClienteTable();
        CreatePedidoTable();
        CreateItensPedidoTable();
        CreateVendedorTable();
        CreateProdutoTable();
    }

    private void CreateClienteTable() {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Cliente(
                codcliente int not null primary key,
                nome varchar(45) not null,
                endereco varchar(45) not null,
                cidade varchar(45) not null,
                cep varchar(11) not null,
                uf varchar(02) not null,
                ie varchar(12) not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }

    private void CreatePedidoTable() {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Pedido(
                codpedido int not null primary key,
                prazoentrega datetime not null,
                datapedido datetime not null,
                pedidocodcliente int not null,
                pedidocodvendedor int not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }

    private void CreateItensPedidoTable() {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS ItensPedido(
                coditempedido int not null primary key,
                itempedidocodpedido int not null,
                itempedidocodproduto int not null,
                quantidade int not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
        
    }

    private void CreateVendedorTable() {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Vendedor(
                codvendedor int not null primary key,
                nome varchar(45) not null,
                salariofixo decimal not null,
                faixacomissao varchar(5) not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
        
    }

    private void CreateProdutoTable() {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Produto(
                codproduto int not null primary key,
                descricao varchar(45) not null,
                valorunitario decimal not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
        
    }
    
}