namespace Classes;

public class Educacao : Gastos
{
public Educacao(string cliente, decimal valorInicial) : base(cliente, valorInicial){}

    public override void MargemDeSeguranca()
    {
        decimal margem = ValorAcumulado * 0.04m;
        EfetuarTransacao(margem, DateTime.Now, "Margem de segurança para Educacao");
    }
}