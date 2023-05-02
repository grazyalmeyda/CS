namespace Classes;

public class Lazer : Gastos
{
    private readonly decimal _bonificacao = -50m;

    public Lazer(string cliente, decimal valorInicial, decimal bonus = -50) : base(cliente, valorInicial)
    => _bonificacao = bonus;

    public override void MargemDeSeguranca()
{
    decimal margem = ValorAcumulado * 0.03m;
    EfetuarTransacao(margem, DateTime.Now, "Margem de segurança para lazer");
    if (_bonificacao != 0)
    {
        EfetuarTransacao(_bonificacao, DateTime.Now, "Bonus para lazer");
    }
}
}