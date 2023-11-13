using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Entities.Pecas
{
    internal class Dama : Tab.Peca
    {
        public Dama(Cor cor, Tab.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            var movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            // Movimento Vertical
            for (int i = 0; i < Tabuleiro.Linha; i++)
            {
                Tab.Posicao posicao = new(i, Posicao.Coluna);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[i, Posicao.Coluna] = true;
            }

            // Movimento Horizontal
            for (int i = 0; i < Tabuleiro.Coluna; i++)
            {
                Tab.Posicao posicao = new(Posicao.Linha, i);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[Posicao.Linha, i] = true;
            }

            // Vertical superior

            for (int i = Posicao.Linha; i > 0; i--)
            {
                Tab.Posicao posicao = new(i - 1, i - 1);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.Coluna = i + 1;

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            // Vertical inferior

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

        public override string ToString() => "D";
    }
}