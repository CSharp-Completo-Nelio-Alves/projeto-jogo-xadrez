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
			var posicaoOrigem = partida.ObterPosicao();
			var pecaSelecionada = partida.Tabuleiro.ObterPeca(posicaoOrigem.ConverterParaPosicaoTabuleiro());

			Console.Clear();
			Tela.ImprimirTabuleiro(partida.Tabuleiro, pecaSelecionada);
            Console.WriteLine("\n");

            var destino = partida.ObterPosicao(origem: false);

			partida.ExecutarMovimento(posicaoOrigem, destino);
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