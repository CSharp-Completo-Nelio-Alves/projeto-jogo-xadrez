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

            MarcarMovimentoHorizontalEsquerdo(movimentosPossiveis);
            MarcarMovimentoHorizontalDireito(movimentosPossiveis);

            MarcarMovimentoVerticalSuperior(movimentosPossiveis);
            MarcarMovimentoVerticalInferior(movimentosPossiveis);

            MarcarMovimentoDiagonalSuperiorEsquerdo(movimentosPossiveis);
            MarcarMovimentoDiagonalSuperiorDireito(movimentosPossiveis);
            MarcarMovimentoDiagonalInferiorDireito(movimentosPossiveis);
            MarcarMovimentoDiagonalInferiorEsquerdo(movimentosPossiveis);

            return movimentosPossiveis;
        }

        public override string ToString() => "D";

        #region Métodos para Tratar Marcação de Movimentos Possíveis

        private void MarcarMovimentoHorizontalEsquerdo(bool[,] movimentosPossiveis)
        {
            for (var i = Posicao.Coluna - 1; i >= 0; i--)
            {
                Tab.Posicao posicao = new(Posicao.Linha, i);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoHorizontalDireito(bool[,] movimentosPossiveis)
        {
            for (var i = Posicao.Coluna + 1; i < Tabuleiro.Coluna; i++)
            {
                Tab.Posicao posicao = new(Posicao.Linha, i);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoVerticalSuperior(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha - 1; i >= 0; i--)
            {
                Tab.Posicao posicao = new(i, Posicao.Coluna);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoVerticalInferior(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha + 1; i < Tabuleiro.Linha; i++)
            {
                Tab.Posicao posicao = new(i, Posicao.Coluna);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoDiagonalSuperiorEsquerdo(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha - 1; i >= 0; i--)
            {
                var qtdLinhasCaminhadas = Posicao.Linha - i;
                Tab.Posicao posicao = new(i, Posicao.Coluna - qtdLinhasCaminhadas);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoDiagonalSuperiorDireito(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha - 1; i >= 0; i--)
            {
                var qtdLinhasCaminhadas = Posicao.Linha - i;
                Tab.Posicao posicao = new(i, Posicao.Coluna + qtdLinhasCaminhadas);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoDiagonalInferiorDireito(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha + 1; i < Tabuleiro.Linha; i++)
            {
                var qtdLinhasCaminhadas = i - Posicao.Linha;
                Tab.Posicao posicao = new(i, Posicao.Coluna + qtdLinhasCaminhadas);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private void MarcarMovimentoDiagonalInferiorEsquerdo(bool[,] movimentosPossiveis)
        {
            for (int i = Posicao.Linha + 1; i < Tabuleiro.Linha; i++)
            {
                var qtdLinhasCaminhadas = i - Posicao.Linha;
                Tab.Posicao posicao = new(i, Posicao.Coluna - qtdLinhasCaminhadas);

                if (!TratarPosicao(movimentosPossiveis, posicao))
                    break;
            }
        }

        private bool TratarPosicao(bool[,] movimentosPossiveis, Tab.Posicao posicao)
        {
            if (!ValidarMovimento(posicao))
                return false;

            movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            if (ValidarSeExistePecaAdversaria(posicao))
                return false;

            return true;
        }

        #endregion
    }
}