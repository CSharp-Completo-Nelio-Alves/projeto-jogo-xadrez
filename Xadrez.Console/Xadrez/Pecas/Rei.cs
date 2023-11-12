using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Pecas
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            var movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            for (int i = Posicao.Linha - 1; i <= Posicao.Linha + 1; i++)
            {
                for (int j = Posicao.Coluna - 1; j <= Posicao.Coluna + 1; j++)
                {
                    Tabuleiro.Posicao posicao = new(i, j);

                    if (ValidarMovimento(posicao))
                        movimentosPossiveis[i, j] = true;
                }
            }

            return movimentosPossiveis;
        }

        public override string ToString() => "R";
    }
}
