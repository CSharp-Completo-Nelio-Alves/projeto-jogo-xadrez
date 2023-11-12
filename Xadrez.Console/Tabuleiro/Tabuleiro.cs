namespace Xadrez.Console.Tabuleiro
{
    internal class Tabuleiro
    {
        private readonly Peca[,] _pecas;

        public int Linha { get; private set; }
        public int Coluna { get; private set; }

        public Tabuleiro(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;

            _pecas = new Peca[Linha, Coluna];
        }
    }
}
