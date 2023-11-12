using Xadrez.ConsoleApp.Tabuleiro.Exceptions;

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

            if (!ValidarPosicao(posicao))
                throw new TabuleiroException("Posição inválida");

            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public bool VerificarSeExistePeca(Posicao posicao)
        {
            if (posicao is null)
                throw new TabuleiroException("Posição não informada");

            if (!ValidarPosicao(posicao))
                throw new TabuleiroException("Posição inválida");

            Peca peca = ObterPeca(posicao);

            return peca is not null;
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (peca is null)
                throw new TabuleiroException("Peça não informada");

            if (VerificarSeExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça na posição informada");

            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (!VerificarSeExistePeca(posicao))
                return null;

            Peca peca = ObterPeca(posicao);

            peca.Posicao = null;
            _pecas[posicao.Linha, posicao.Coluna] = null;

            return peca;
        }

        #region Métodos de Validação

        public bool ValidarPosicao(Posicao posicao) =>
            posicao.Linha >= 0 && posicao.Linha < Linha && posicao.Coluna >= 0 && posicao.Coluna < Coluna;

        #endregion
    }
}
