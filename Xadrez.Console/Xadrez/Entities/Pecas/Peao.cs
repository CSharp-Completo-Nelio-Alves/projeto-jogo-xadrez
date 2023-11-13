using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Tabuleiro.Entities;

namespace Xadrez.ConsoleApp.Xadrez.Entities.Pecas
{
    internal class Peao : Tab.Peca
    {
        public Peao(Cor cor, Tab.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            var podeRealizarCaptura = true; // Só pode realizar captura nas posições adjacentes uma casa acima
            var totalCasasPermitidoMovimentar = QuantidadeMovimento == 0 ? 2 : 1;

            // CUIDADO: No começo da partida, na visão do tabuleiro, as peças brancas iniciam na casa de linha 7. Inverter lógicas a seguir se alterar para iniciar na casa 0
            int linhaAnaliseAtual = Cor == Cor.Branca ? Posicao.Linha - 1 : Posicao.Linha + 1;

            bool continuarMarcacao()
            {
                if (Cor == Cor.Branca && linhaAnaliseAtual >= Posicao.Linha - totalCasasPermitidoMovimentar)
                    return true;

                if (Cor == Cor.Preta && linhaAnaliseAtual <= Posicao.Linha + totalCasasPermitidoMovimentar)
                    return true;

                return false;
            }

            void avancarLinha()
            {
                if (Cor == Cor.Branca)
                    linhaAnaliseAtual--;
                else
                    linhaAnaliseAtual++;
            }

            for (; continuarMarcacao(); avancarLinha())
            {
                Tab.Posicao posicao = new(linhaAnaliseAtual, Posicao.Coluna);

                if (podeRealizarCaptura)
                {
                    TratarMovimentoCaptura(movimentosPossiveis, posicao);
                    podeRealizarCaptura = false;
                }

                if (!ValidarMovimento(posicao))
                    break;

                movimentosPossiveis[posicao.Linha, Posicao.Coluna] = true;
            }

            return movimentosPossiveis;
        }

        public override bool ValidarMovimento(Posicao posicaoDestino)
        {
            if (!Tabuleiro.ValidarPosicao(posicaoDestino))
                return false;

            Peca peca = Tabuleiro.ObterPeca(posicaoDestino);

            if (peca is not null)
                return false;

            return true;
        }

        public override string ToString() => "P";

        #region Métodos Privados para Tratar Movimentos Possíveis

        private void TratarMovimentoCaptura(bool[,] movimentosPossiveis, Posicao posicaoDestino)
        {
            Tab.Posicao posicao = new(posicaoDestino.Linha, posicaoDestino.Coluna - 1);

            MarcarMovimentoCaptura(movimentosPossiveis, posicao);

            posicao.Coluna = posicaoDestino.Coluna + 1;

            MarcarMovimentoCaptura(movimentosPossiveis, posicao);
        }

        private void MarcarMovimentoCaptura(bool[,] movimentosPossiveis, Posicao posicao)
        {
            if (base.ValidarMovimento(posicao) && ValidarSeExistePecaAdversaria(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }

        #endregion
    }
}
