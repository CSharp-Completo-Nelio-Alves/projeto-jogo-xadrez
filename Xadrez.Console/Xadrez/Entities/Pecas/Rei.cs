using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Tabuleiro.Entities;

namespace Xadrez.ConsoleApp.Xadrez.Entities.Pecas
{
    internal class Rei : Tab.Peca
    {
        public bool EmXeque { get; set; }

        public Rei(Cor cor, Tab.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            var movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            for (int i = Posicao.Linha - 1; i <= Posicao.Linha + 1; i++)
            {
                for (int j = Posicao.Coluna - 2; j <= Posicao.Coluna + 2; j++)
                {
                    Tab.Posicao posicao = new(i, j);

                    if (ValidarMovimento(posicao))
                        movimentosPossiveis[i, j] = true;
                }
            }

            return movimentosPossiveis;
        }

        public override bool ValidarMovimento(Posicao posicaoDestino)
        {
            if (!base.ValidarMovimento(posicaoDestino))
                return false;

            var movimentoPadraoValido = ValidarSeMovimentoPadraoValido(posicaoDestino);
            var movimentoRoquePequenoValido = ValidarSePodeRealizarMovimentoRoquePequeno(posicaoDestino);
            var movimentoRoqueGrandeValido = ValidarSePodeRealizarMovimentoRoqueGrande(posicaoDestino);

            if (!movimentoPadraoValido && !movimentoRoquePequenoValido && !movimentoRoqueGrandeValido)
                return false;

            return true;
        }

        private bool ValidarSeMovimentoPadraoValido(Posicao posicao)
        {
            if (posicao.Coluna < Posicao.Coluna - 1 || posicao.Coluna > Posicao.Coluna + 1)
                return false;

            return true;
        }

        #region Movimento Especial Roque

        private bool ValidarSePodeRealizarMovimentoRoque(Posicao posicaoDestino)
        {
            if (EmXeque)
                return false;

            if (QuantidadeMovimento > 0)
                return false;

            if (posicaoDestino.Linha != Posicao.Linha)
                return false;

            return true;
        }

        #region Roque Pequeno

        public bool ValidarSePodeRealizarMovimentoRoquePequeno(Posicao posicao)
        {
            if (!ValidarSePodeRealizarMovimentoRoque(posicao))
                return false;

            if (!ValidarSeExisteTorreParaRealizarRoquePequeno())
                return false;

            if (posicao.Coluna != Posicao.Coluna + 2)
                return false;

            if (!ValidarSeCaminhoLivreParaRoquePequeno(posicao))
                return false;

            return true;
        }

        private bool ValidarSeExisteTorreParaRealizarRoquePequeno()
        {
            var posicaoTorre = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
            var torre = Tabuleiro.ObterPeca(posicaoTorre);

            return torre is not null && torre.QuantidadeMovimento == 0 && torre.Cor == Cor;
        }

        private bool ValidarSeCaminhoLivreParaRoquePequeno(Posicao posicao)
        {
            if (posicao.Coluna != Posicao.Coluna + 2)
                return false;

            var posicaoCaminhada = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

            if (ValidarSeExistePecaAdversaria(posicaoCaminhada))
                return false;

            posicaoCaminhada.Coluna = Posicao.Coluna + 2;

            if (ValidarSeExistePecaAdversaria(posicaoCaminhada))
                return false;

            return true;
        }

        #endregion

        #region Roque Grande

        public bool ValidarSePodeRealizarMovimentoRoqueGrande(Posicao posicao)
        {
            if (!ValidarSePodeRealizarMovimentoRoque(posicao))
                return false;

            if (!ValidarSeExisteTorreParaRealizarRoqueGrande())
                return false;

            if (posicao.Coluna != Posicao.Coluna - 2)
                return false;

            if (!ValidarSeCaminhoLivreParaRoqueGrande(posicao))
                return false;

            return true;
        }

        private bool ValidarSeExisteTorreParaRealizarRoqueGrande()
        {
            var posicaoTorre = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
            var torre = Tabuleiro.ObterPeca(posicaoTorre);

            return torre is not null && torre.QuantidadeMovimento == 0 && torre.Cor == Cor;
        }

        private bool ValidarSeCaminhoLivreParaRoqueGrande(Posicao posicao)
        {
            if (posicao.Coluna != Posicao.Coluna - 2)
                return false;

            var posicaoCaminhada = new Posicao(Posicao.Linha, Posicao.Coluna - 1);

            if (ValidarSeExistePecaAdversaria(posicaoCaminhada))
                return false;

            posicaoCaminhada.Coluna = Posicao.Coluna - 2;

            if (ValidarSeExistePecaAdversaria(posicaoCaminhada))
                return false;

            // Validar se existe peça no caminho até a torre
            posicaoCaminhada.Coluna = Posicao.Coluna - 3;

            if (!base.ValidarMovimento(posicaoCaminhada) || ValidarSeExistePecaAdversaria(posicaoCaminhada))
                return false;

            return true;
        }

        #endregion

        #endregion

        public override string ToString() => "R";
    }
}
