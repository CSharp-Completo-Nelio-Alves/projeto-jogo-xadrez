using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Pecas
{
    internal class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override bool[,] RetornarMovimentosPossiveis()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => "B";
    }
}
