using Classes;
var gastos1 = new Alimentacao("Grazi", 0);
gastos1.EfetuarTransacao(360, DateTime.Now, "Comida");
gastos1.EfetuarTransacao(250, DateTime.Now, "Fast Food");
gastos1.MargemDeSeguranca();
Console.WriteLine(gastos1.HistoricoDeGastos());

var gastos2 = new Vestuario("Grazi", 0);
gastos2.EfetuarTransacao(100, DateTime.Now, "Vestido");
gastos2.EfetuarTransacao(40, DateTime.Now, "Tênis");
gastos2.MargemDeSeguranca();
Console.WriteLine(gastos2.HistoricoDeGastos());

var gastos3 = new Lazer("Grazi", 0);
gastos3.EfetuarTransacao(20, DateTime.Now, "Cinema");
gastos3.EfetuarTransacao(50, DateTime.Now, "Bonus");
gastos3.MargemDeSeguranca();
Console.WriteLine(gastos3.HistoricoDeGastos());

var gastos4 = new Educacao("Grazi", 0);
gastos4.EfetuarTransacao(100, DateTime.Now, "Curso");
gastos4.EfetuarTransacao(50, DateTime.Now, "Cursinho");
gastos4.MargemDeSeguranca();
Console.WriteLine(gastos4.HistoricoDeGastos());