using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Entities.Pecas
{
    internal class Torre : Tab.Peca
    {
        public Torre(Cor cor, Tab.Tabuleiro tabuleiro)
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
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            // Movimento Horizontal
            for (int i = 0; i < Tabuleiro.Coluna; i++)
            {
                Tab.Posicao posicao = new(Posicao.Linha, i);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            return movimentosPossiveis;
        }

        public override string ToString() => "T";
    }
}
