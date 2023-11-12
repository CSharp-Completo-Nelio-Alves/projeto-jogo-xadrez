using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Xadrez.Pecas
{
    internal class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro.Tabuleiro tabuleiro)
            : base(cor, tabuleiro)
        {
        }

        public override string ToString() => "P";
    }
}
