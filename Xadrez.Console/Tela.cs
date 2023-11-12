using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp
{
    internal static class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro.Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linha ; i++)
            {
                EscreverIdentificacaoLinha(i);

                for (int j = 0; j < tabuleiro.Coluna; j++)
                {
                    var peca = tabuleiro.ObterPeca(new Posicao(i, j));

                    EscreverPeca(peca);
                }

                Console.WriteLine();
            }

            Console.Write("  ");

            EscreverIdentificacaoColuna();
        }

        private static void EscreverIdentificacaoLinha(int linhaTabuleiro)
        {
            var linhaXadrez = 8 - linhaTabuleiro;

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write($"{linhaXadrez} ");

            Console.ResetColor();
        }

        private static void EscreverPeca(Peca peca)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;

            if (peca is null)
                Console.ForegroundColor = ConsoleColor.Gray;
            else if (peca.Cor == Cor.Preta)
                Console.ForegroundColor= ConsoleColor.Black;
            else
                Console.ForegroundColor = ConsoleColor.White;

            Console.Write($" {(peca is null ? "-" : peca.ToString())} ");

            Console.ResetColor();
        }

        private static void EscreverIdentificacaoColuna()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write($" A  B  C  D  E  F  G  H");

            Console.ResetColor();
        }
    }
}
