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

            MarcarMovimentosVerticalSuperiorEsquerdo(movimentosPossiveis);
            MarcarMovimentosVerticalSuperioDireito(movimentosPossiveis);
            MarcarMovimentosVerticalInferiorDireito(movimentosPossiveis);
            MarcarMovimentosVerticalInferiorEsquerdo(movimentosPossiveis);

            return movimentosPossiveis;
        }

        private void MarcarMovimentosVerticalSuperiorEsquerdo(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha - 1; i >= 0; i--)
            {
                var qtdLinhasCaminhadas = Posicao.Linha - i;
                Tab.Posicao posicao = new(i, Posicao.Coluna - qtdLinhasCaminhadas);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentosVerticalSuperioDireito(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha - 1; i >= 0; i--)
            {
                var qtdLinhasCaminhadas = Posicao.Linha - i;
                Tab.Posicao posicao = new(i, Posicao.Coluna + qtdLinhasCaminhadas);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentosVerticalInferiorDireito(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha + 1; i < Tabuleiro.Linha; i++)
            {
                var qtdLinhasCaminhadas = i - Posicao.Linha;
                Tab.Posicao posicao = new(i, Posicao.Coluna + qtdLinhasCaminhadas);

                if (!TratarPosicaoAtual(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentosVerticalInferiorEsquerdo(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha + 1; i < Tabuleiro.Linha; i++)
            {
                var qtdLinhasCaminhadas = i - Posicao.Linha;
                Tab.Posicao posicao = new(i, Posicao.Coluna - qtdLinhasCaminhadas);

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

        public override string ToString() => "B";
    }
}
