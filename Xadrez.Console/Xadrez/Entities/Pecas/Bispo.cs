using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Entities.Pecas
{
    internal class Bispo : Tab.Peca
    {
        public Bispo(Cor cor, Tab.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            var movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            // Vertical Superior
            for (int i = Posicao.Linha; i > 0; i--)
            {
                Tab.Posicao posicao = new(i - 1, i - 1);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.Coluna = i + 1;

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            // Vertical Inferior
            for (int i = Posicao.Linha; i < Tabuleiro.Linha; i++)
            {
                Tab.Posicao posicao = new(i + 1, i - 1);

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
