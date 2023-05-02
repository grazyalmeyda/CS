namespace Classes;

public class Vestuario : Gastos
{
    public Vestuario(string cliente, decimal valorInicial) : base(cliente, valorInicial){}
    public override void MargemDeSeguranca()
    {
        decimal margem = ValorAcumulado * 0.02m;
        EfetuarTransacao(margem, DateTime.Now, "Margem de segurança para Vestuário");
    }
}