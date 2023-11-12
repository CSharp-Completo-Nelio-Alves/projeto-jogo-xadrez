namespace Xadrez.ConsoleApp.Tabuleiro
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

        public Peca ObterPeca(Posicao posicao)
        {
            if (posicao is null)
                return null;

            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
    }
}
