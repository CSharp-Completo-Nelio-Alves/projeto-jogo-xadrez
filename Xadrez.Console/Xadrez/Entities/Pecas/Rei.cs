using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Entities.Pecas
{
    internal class Rei : Tab.Peca
    {
        public Rei(Cor cor, Tab.Tabuleiro tabuleiro)
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
                    Tab.Posicao posicao = new(i, j);

                    if (ValidarMovimento(posicao))
                        movimentosPossiveis[i, j] = true;
                }
            }

            return movimentosPossiveis;
        }

        public override string ToString() => "R";
    }
}
