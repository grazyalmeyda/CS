namespace ProvaLp3.Models;

class ItensPedido {

    public int CodItemPedido { get; set; }
    public int ItemPedidoCodPedido { get; set; }
    public int ItemPedidoCodProduto { get; set; }
    public int Quantidade { get; set; }

    public ItensPedido(int coditempedido, int itempedidocodpedido, int itempedidocodproduto, int quantidade) {
        CodItemPedido = coditempedido;
        ItemPedidoCodPedido = itempedidocodpedido;
        ItemPedidoCodProduto = itempedidocodproduto;
        Quantidade = quantidade;
    }
}