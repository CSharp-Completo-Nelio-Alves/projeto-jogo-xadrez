using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

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

            var qtdLinhasPermitdoMover = QuantidadeMovimento == 0 ? 2 : 1;
            int linhaAnaliseAtual = Cor == Cor.Branca ? Posicao.Linha - qtdLinhasPermitdoMover : Posicao.Linha + qtdLinhasPermitdoMover;

            bool continuarAnalise()
            {
                if (Cor == Cor.Branca && linhaAnaliseAtual < Posicao.Linha)
                    return true;

                if (Cor == Cor.Preta && linhaAnaliseAtual > Posicao.Linha)
                    return true;

                return false;
            }

            int avancarLinha() => Cor == Cor.Branca ? linhaAnaliseAtual + 1 : linhaAnaliseAtual - 1;

            while (continuarAnalise())
            {
                Tab.Posicao posicao = new(linhaAnaliseAtual, Posicao.Coluna);

                if (ValidarMovimento(posicao))
                    movimentosPossiveis[linhaAnaliseAtual, Posicao.Coluna] = true;

                // Peão pode capturar peças uma linha acima que estejam em colunas adjacentes
                if (QuantidadeMovimento > 0)
                {
                    for (int i = Posicao.Coluna - 1; i <= Posicao.Coluna + 1; i++)
                    {
                        posicao = new(linhaAnaliseAtual, i);

                        if (ValidarMovimento(posicao) && Tabuleiro.ObterPeca(posicao) is not null)
                            movimentosPossiveis[linhaAnaliseAtual, Posicao.Coluna] = true;
                    }
                }

                linhaAnaliseAtual = avancarLinha();
            }

            return movimentosPossiveis;
        }

        public override string ToString() => "P";
    }
}
