namespace Classes;

public class Alimentacao : Gastos
{
    public Alimentacao(string cliente, decimal valorInicial) : base(cliente, valorInicial){}
    public override void MargemDeSeguranca()
    {
        decimal margem = ValorAcumulado * 0.01m;
        EfetuarTransacao(margem, DateTime.Now, "Margem de segurança para alimentacao");
    }
}