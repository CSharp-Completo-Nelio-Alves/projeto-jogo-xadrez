using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp
{
    internal static class Tela
    {
        public static void ImprimirTabuleiro(Tab.Tabuleiro tabuleiro, Tab.Peca pecaSelecionada = null)
        {
            bool[,] movimentosPossíveis = pecaSelecionada?.RetornarMovimentosPossiveis();

            for (int i = 0; i < tabuleiro.Linha ; i++)
            {
                EscreverIdentificacaoLinha(i);

                for (int j = 0; j < tabuleiro.Coluna; j++)
                {
                    var peca = tabuleiro.ObterPeca(new Tab.Posicao(i, j));
                    
                    bool marcarMovimentoPossivel = movimentosPossíveis is not null && movimentosPossíveis[i, j];
                    bool marcarPeca = pecaSelecionada is not null && pecaSelecionada.Equals(peca) && pecaSelecionada.Posicao.Equals(peca.Posicao);

                    EscreverPeca(peca, marcarPeca, marcarMovimentoPossivel);
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

        private static void EscreverPeca(Tab.Peca peca, bool marcarPeca = false, bool marcarMovimentoPossivel = false)
        {
            if (marcarMovimentoPossivel)
                Console.BackgroundColor = ConsoleColor.Blue;
            else
                Console.BackgroundColor = ConsoleColor.DarkGray;

            if (peca is null)
                Console.ForegroundColor = ConsoleColor.Gray;
            else if (peca.Cor == Cor.Preta)
                Console.ForegroundColor= ConsoleColor.Black;
            else
                Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"{(peca is null ? " - " : marcarPeca ? $"[{peca}]" : $" {peca} ")}");

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
