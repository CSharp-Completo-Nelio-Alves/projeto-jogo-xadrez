using Xadrez.ConsoleApp;
using Xadrez.ConsoleApp.Tabuleiro.Exceptions;
using Xadrez.ConsoleApp.Xadrez;

Partida partida = new();

while (!partida.Finalizada)
{
    try
    {
        Tela.ImprimirPartida(partida);

        Console.WriteLine("\n");

        partida.RealizarJogada();
        
        Console.Clear();
    }
    catch (TabuleiroException ex)
    {
        Console.ResetColor();
        Console.Clear();
        Tela.ImprimirMensagem($"{ex.Message}\n", ehError: true);
    }
    catch (Exception ex)
    {
        Console.ResetColor();
        Console.Clear();
        Tela.ImprimirMensagem($"\nUnexpected erro: {ex.Message}\n", ehError: true);
    }
}