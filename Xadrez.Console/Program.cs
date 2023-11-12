using Xadrez.ConsoleApp;
using Xadrez.ConsoleApp.Tabuleiro.Exceptions;
using Xadrez.ConsoleApp.Xadrez;

try
{
	Partida partida = new();

	while (!partida.Finalizada)
	{
		try
		{
			Tela.ImprimirTabuleiro(partida.Tabuleiro);

			Console.WriteLine("\n");
			var origem = partida.ObterPosicao();
			var destino = partida.ObterPosicao(origem: false);

			partida.ExecutarMovimento(origem, destino);
			Console.Clear();
		}
		catch (TabuleiroException ex)
		{
			Console.Clear();
            Console.WriteLine($"{ex.Message}\n");
        }
	}
}
catch (TabuleiroException ex)
{
	Console.Clear();
	Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.Clear();
    Console.WriteLine($"\nUnexpected erro: {ex.Message}");
}