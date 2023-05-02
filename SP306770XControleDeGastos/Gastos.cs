namespace Classes;

public class Gastos
{
    public string Cliente {get; set;}
    public decimal ValorAcumulado {
    get
    {
        decimal saldo = 0m;
        foreach (var item in todasTransacoes)
        {
            saldo += item.Valor;
        }

        return saldo;
    }
    }
    

    public Gastos(string nome, decimal valorInicial)
    {
        this.Cliente = nome;
        EfetuarTransacao(valorInicial, DateTime.Now, "Valor Inicial");
    }
    
    private List<Transacao> todasTransacoes = new List<Transacao>();

    public void EfetuarTransacao(decimal valor, DateTime data, string descricao)
    {
    var transacao = new Transacao(valor, data, descricao);
    todasTransacoes.Add(transacao);
    }

    public string HistoricoDeGastos()
    {
    var relatorio = new System.Text.StringBuilder();

    decimal saldo = 0m;
    relatorio.AppendLine("Data            Valor   Valor Acumulado    Descrição");
    foreach (var item in todasTransacoes)
    {
        saldo += item.Valor;
        relatorio.AppendLine($"{item.Data.ToShortDateString(), -10}{item.Valor, 11}{saldo, 18}{"    "}{item.Descricao}");
    }

    return relatorio.ToString();
    }
    public virtual void MargemDeSeguranca() { }
}