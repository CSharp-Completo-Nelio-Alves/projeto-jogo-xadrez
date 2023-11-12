using Xadrez.ConsoleApp.Tabuleiro;

namespace Xadrez.ConsoleApp
{
    internal static class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro.Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linha; i++)
            {
                for(int j = 0; j < tabuleiro.Coluna; j++)
                {
                    Posicao posicao = new(i, j);
                    Peca peca = tabuleiro.ObterPeca(posicao);

                    Console.Write((peca is null ? "- " : $"{peca} "));
                }

                Console.WriteLine();
            }
        }
    }
}
