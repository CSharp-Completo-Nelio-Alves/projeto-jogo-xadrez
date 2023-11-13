using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Pecas
{
    internal class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            var movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            // Vertical Superior
            for (int i = Posicao.Linha; i > 0; i--)
            {
                Tabuleiro.Posicao posicao = new(i - 1, i - 1);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.Coluna = i + 1;

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            // Vertical Inferior
            for (int i = Posicao.Linha; i < Tabuleiro.Linha; i++)
            {
                Tabuleiro.Posicao posicao = new(i + 1, i - 1);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.Coluna = i + 1;

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            return movimentosPossiveis;
        }

        public override string ToString() => "B";
    }
}
