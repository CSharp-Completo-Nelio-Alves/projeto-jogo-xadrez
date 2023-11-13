using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Pecas
{
    internal class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            var movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            void atualizarMovimentosPossiveis(Tabuleiro.Posicao posicao)
            {
                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            for (int i = Posicao.Linha - 2; i <= Posicao.Linha + 2; i++)
            {
                if (i == Posicao.Linha - 2 || i == Posicao.Linha + 2)
                {
                    Tabuleiro.Posicao posicao = new(i, Posicao.Coluna - 1);

                    atualizarMovimentosPossiveis(posicao);

                    posicao.Coluna = Posicao.Coluna + 1;

                    atualizarMovimentosPossiveis(posicao);
                }
                else
                {
                    Tabuleiro.Posicao posicao = new(i, Posicao.Coluna - 2);

                    atualizarMovimentosPossiveis(posicao);

                    posicao.Coluna = Posicao.Coluna + 2;

                    atualizarMovimentosPossiveis(posicao);
                }

            }

            return movimentosPossiveis;
        }

        public override string ToString() => "C";
    }
}
