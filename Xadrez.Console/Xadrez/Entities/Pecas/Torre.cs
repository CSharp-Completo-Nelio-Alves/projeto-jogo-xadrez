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

            MarcarMovimentosVerticalSuperior(movimentosPossiveis);
            MarcarMovimentosVerticalInferior(movimentosPossiveis);
            MarcarMovimentosHorizontalEsquerdo(movimentosPossiveis);
            MarcarMovimentosHorizontalDireito(movimentosPossiveis);

            return movimentosPossiveis;
        }

        private void MarcarMovimentosVerticalSuperior(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha - 1; i >= 0; i--)
            {
                Tab.Posicao posicao = new(i, Posicao.Coluna);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentosVerticalInferior(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha + 1; i < Tabuleiro.Linha; i++)
            {
                Tab.Posicao posicao = new(i, Posicao.Coluna);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentosHorizontalEsquerdo(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Coluna - 1; i >= 0; i--)
            {
                Tab.Posicao posicao = new(Posicao.Linha, i);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentosHorizontalDireito(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Coluna + 1; i < Tabuleiro.Coluna; i++)
            {
                Tab.Posicao posicao = new(Posicao.Linha, i);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private bool TratarPosicaoAtual(bool[,] movimentosPossiveis, Tab.Posicao posicao)
        {
            if (!ValidarMovimento(posicao))
                return false;

            movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            if (ValidarSeExistePecaAdversaria(posicao))
                return false;

            return true;
        }

        public override string ToString() => "T";
    }
}
