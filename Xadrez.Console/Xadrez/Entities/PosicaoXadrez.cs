using Xadrez.ConsoleApp.Tabuleiro.Entities;

namespace Xadrez.ConsoleApp.Xadrez.Entities
{
    internal class PosicaoXadrez
    {
        public int Linha { get; set; }
        public char Coluna { get; set; }

        public PosicaoXadrez()
        {
            Linha = 1;
            Coluna = 'a';
        }

        public PosicaoXadrez(int linha, char coluna)
        {
            Linha = linha;
            Coluna = coluna.ToString().ToLower()[0];
        }

        public Posicao ConverterParaPosicaoTabuleiro()
        {
            var linha = 8 - Linha;
            var coluna = Coluna - 'a';

            return new Posicao(linha, coluna);
        }

        public override string ToString() => $"{Linha}, {Coluna.ToString().ToUpper()}";

        #region Métodos de Comparação

        public override int GetHashCode()
        {
            int baseNumber = 17;
            int hashAux = Linha.GetHashCode() + Coluna.GetHashCode();

            return hashAux * baseNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is not PosicaoXadrez)
                return false;

            if (ReferenceEquals(this, (PosicaoXadrez)obj))
                return true;

            var other = obj as PosicaoXadrez;

            return Linha.Equals(other.Linha) && Coluna.Equals(other.Coluna);
        }

        public static bool operator ==(PosicaoXadrez left, PosicaoXadrez right)
        {
            if (left is null || right is null)
                return false;

            return Equals(left, right) || left.Linha == right.Linha && left.Coluna == right.Coluna;
        }

        public static bool operator !=(PosicaoXadrez left, PosicaoXadrez right) => !(left == right);

        #endregion
    }
}
