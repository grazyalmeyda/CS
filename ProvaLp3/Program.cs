using ProvaLp3.Database;
using ProvaLp3.Repositories;
using ProvaLp3.Models;
using Microsoft.Data.Sqlite;
var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var clienteRepository = new ClienteRepository(databaseConfig);
var itensPedidoRepository = new ItensPedidoRepository(databaseConfig);
var pedidoRepository = new PedidoRepository(databaseConfig);
var produtoRepository = new ProdutoRepository(databaseConfig);
var vendedorRepository = new VendedorRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];
if (modelName == "Cliente")
{
    if (modelAction == "Listar")
    {
        int indicador = 1;
        Console.WriteLine("\nListar Cliente");
        Console.WriteLine("Código Cliente   Nome Cliente         Endereço                     Cidade     CEP        UF   IE");
        foreach (var cliente in clienteRepository.Listar())
        {
            Console.WriteLine($"{cliente.CodCliente,-16} {cliente.Nome,-20} {cliente.Endereco,-28} {cliente.Cidade,-10} {cliente.Cep,-10} {cliente.Uf,-4} {cliente.Ie}");
            ++indicador;
        }
        indicador = 0;
    }

    else if (modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Cliente");
        Console.Write("Digite o Código do cliente   : ");
        int codcliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Nome do cliente     : ");
        string nome = Console.ReadLine();
        Console.Write("Digite o Endereço do cliente : ");
        string endereco = Console.ReadLine();
        Console.Write("Digite a Cidade do cliente   : ");
        string cidade = Console.ReadLine();
        Console.Write("Digite o CEP do cliente      : ");
        string cep = Console.ReadLine();
        Console.Write("Digite a UF do cliente       : ");
        string uf = Console.ReadLine();
        Console.Write("Digite a Inscrição Estadual  : ");
        string ie = Console.ReadLine();
        var cliente = new Cliente(codcliente, nome, endereco, cidade, cep, uf, ie);
        clienteRepository.Inserir(cliente);
    }

    else if (modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Cliente");
        Console.Write("Digite o código do cliente : ");
        int codcliente = Convert.ToInt32(Console.ReadLine());

        if (clienteRepository.Apresentar(codcliente))
        {
            var cliente = clienteRepository.GetById(codcliente);
            Console.Write($"{cliente.CodCliente}, {cliente.Nome}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Cep}, {cliente.Uf}, {cliente.Ie}");
        }
        else
        {
            Console.WriteLine($"O cliente com código {codcliente} não existe.");
        }
    }
}

else if (modelName == "Pedido")
{
    if (modelAction == "Listar")
    {
        Console.WriteLine("\nListar Pedido");
        Console.WriteLine("Código Pedido   Prazo Entrega         Data Pedido           Código Cliente   Código Vendedor");
        int indicador = 1;
        foreach (var pedido in pedidoRepository.Listar())
        {
            Console.WriteLine($"{pedido.CodPedido,-15} {pedido.PrazoEntrega,-21} {pedido.DataPedido,-21} {pedido.PedidoCodCliente,-16} {pedido.PedidoCodVendedor}");
            ++indicador;
        }
        indicador = 1;
    }

    else if (modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Pedido");
        Console.Write("Digite o código do pedido             : ");
        int codpedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o prazo de entrega do pedido   : ");
        DateTime prazoentrega = Convert.ToDateTime(Console.ReadLine());
        DateTime datapedido = DateTime.Now;
        Console.Write("Digite o código do cliente do pedido  : ");
        int pedidocodcliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do vendedor do pedido : ");
        int pedidocodvendedor = Convert.ToInt32(Console.ReadLine());
        var pedido = new Pedido(codpedido, prazoentrega, datapedido, pedidocodcliente, pedidocodvendedor);
        pedidoRepository.Inserir(pedido);
    }

    else if (modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Pedido");
        Console.Write("Digite o código do pedido : ");
        int codpedido = Convert.ToInt32(Console.ReadLine());

        if (pedidoRepository.Apresentar(codpedido))
        {
            var pedido = pedidoRepository.GetById(codpedido);
            Console.WriteLine($"{pedido.CodPedido}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.PedidoCodCliente}, {pedido.PedidoCodVendedor}");
        }
        else
        {
            Console.WriteLine($"O pedido com código {codpedido} não existe.");
        }
    }
}

else if (modelName == "ItensPedido")
{
    if (modelAction == "Listar")
    {
        Console.WriteLine("\nListar Itens do Pedido");
        Console.WriteLine("Código Item Pedido   Quantidade   Código Pedido   Código Produto");
        int indicador = 1;
        foreach (var itensPedido in itensPedidoRepository.Listar())
        {
            Console.WriteLine($"{itensPedido.CodItemPedido,-20} {itensPedido.Quantidade,-12} {itensPedido.ItemPedidoCodPedido,-15} {itensPedido.ItemPedidoCodProduto}");
            ++indicador;
        }
        indicador = 1;
    }

    else if (modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Itens do Pedido");
        Console.Write("Digite o código do item do pedido            : ");
        int coditempedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do pedido do item do pedido  : ");
        int itempedidocodpedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do produto do item do pedido : ");
        int itempedidocodproduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a quantidade do item do pedido        : ");
        int quantidade = Convert.ToInt32(Console.ReadLine());
        var itenspedido = new ItensPedido(coditempedido, itempedidocodpedido, itempedidocodproduto, quantidade);
        itensPedidoRepository.Inserir(itenspedido);
    }

    else if (modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Itens do Pedido");
        Console.Write("Digite o código do item do pedido : ");
        int coditempedido = Convert.ToInt32(Console.ReadLine());

        if (itensPedidoRepository.Apresentar(coditempedido))
        {
            var itenspedido = itensPedidoRepository.GetById(coditempedido);
            Console.WriteLine($"{itenspedido.CodItemPedido}, {itenspedido.ItemPedidoCodPedido}, {itenspedido.ItemPedidoCodProduto}, {itenspedido.Quantidade}");
        }
        else
        {
            Console.WriteLine($"O item do pedido com código {coditempedido} não existe.");
        }
    }
}

else if (modelName == "Produto")
{
    if (modelAction == "Listar")
    {
        Console.WriteLine("\nListar Produto");
        Console.WriteLine("Código Produto   Descrição   Valor Unitário");
        int indicador = 1;
        foreach (var produto in produtoRepository.Listar())
        {
            Console.WriteLine($"{produto.CodProduto,-16} {produto.Descricao,-11} {produto.ValorUnitario}");
            ++indicador;
        }
        indicador = 1;
    }

    else if (modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Produto");
        Console.Write("Digite o código do produto         : ");
        int codproduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a descrição do produto      : ");
        string descricao = Console.ReadLine();
        Console.Write("Digite o valor unitário do produto : ");
        decimal valorunitario = Convert.ToDecimal(Console.ReadLine());
        var produto = new Produto(codproduto, descricao, valorunitario);
        produtoRepository.Inserir(produto);
    }

    else if (modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Produto");
        Console.Write("Digite o código do produto : ");
        int codproduto = Convert.ToInt32(Console.ReadLine());

        if (produtoRepository.Apresentar(codproduto))
        {
            var produto = produtoRepository.GetById(codproduto);
            Console.WriteLine($"{produto.CodProduto}, {produto.Descricao}, {produto.ValorUnitario}");
        }
        else
        {
            Console.WriteLine($"O produto com código {codproduto} não existe.");
        }
    }
}

else if (modelName == "Vendedor")
{
    if (modelAction == "Listar")
    {
        Console.WriteLine("\nListar Vendedor");
        Console.WriteLine("Código Vendedor   Nome Vendedor      Salário Fixo   Faixa Comissão");
        int indicador = 1;
        foreach (var vendedor in vendedorRepository.Listar())
        {
            Console.WriteLine($"{vendedor.CodVendedor,-17} {vendedor.Nome,-18} {vendedor.SalarioFixo,-14} {vendedor.FaixaComissao}");
            ++indicador;
        }
        indicador = 1;
    }

    else if (modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Vendedor");
        Console.Write("Digite o código do vendedor            : ");
        int codvendedor = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o nome do vendedor              : ");
        string nome = Console.ReadLine();
        Console.Write("Digite o salário fixo do vendedor      : ");
        decimal salariofixo = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Digite o faixa de comissão do vendedor : ");
        string faixacomissao = Console.ReadLine();
        var vendedor = new Vendedor(codvendedor, nome, salariofixo, faixacomissao);
        vendedorRepository.Inserir(vendedor);
    }

    else if (modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Vendedor");
        Console.Write("Digite o código do Vendedor : ");
        int codvendedor = Convert.ToInt32(Console.ReadLine());

        if (vendedorRepository.Apresentar(codvendedor))
        {
            var vendedor = vendedorRepository.GetById(codvendedor);
            Console.WriteLine($"{vendedor.CodVendedor}, {vendedor.Nome}, {vendedor.SalarioFixo}, {vendedor.FaixaComissao}");
        }
        else
        {
            Console.WriteLine($"O vendedor com código {codvendedor} não existe.");
        }
    }
}