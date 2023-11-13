using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Xadrez;

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

        public static void ImprimirCabecalho(Partida partida)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("Partida de Xadrez\n");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine($"Turno {partida.Turno}");
            Console.WriteLine($"Jogador Atual: Peças {partida.JogadorAtual}\n");

            Console.WriteLine("Peças Capturadas:\n");
            Console.ResetColor();

            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
        }

        public static void ImprimirMensagem(string mensagem, bool ehError = false)
        {
            if (ehError)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(mensagem);

            Console.ResetColor();
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
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" - ");
            }
            else
            {
                ConsoleColor corTextoPeca = peca.Cor == Cor.Preta ? ConsoleColor.Black : ConsoleColor.White;

                Console.ForegroundColor = corTextoPeca;

                if (marcarPeca)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write("[");

                    Console.ForegroundColor = corTextoPeca;

                    Console.Write(peca);

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write("]");
                }
                else
                    Console.Write($" {peca} ");
            }

            Console.ResetColor();
        }

        private static void EscreverIdentificacaoColuna()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write($" A  B  C  D  E  F  G  H");

            Console.ResetColor();
        }

        private static void ImprimirPecasCapturadas(Partida partida)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;

            ConsoleColor corLabel = ConsoleColor.Yellow;

            Console.ForegroundColor = corLabel;

            Console.Write("Brancas: [");

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(string.Join(',', partida.ObterPecasCapturadas(Cor.Branca)));

            Console.ForegroundColor = corLabel;

            Console.WriteLine("]");

            Console.Write("Pretas: [");

            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write(string.Join(',', partida.ObterPecasCapturadas(Cor.Preta).Select(p => p)));

            Console.ForegroundColor = corLabel;

            Console.WriteLine("]");

            Console.ResetColor();
        }
    }
}
