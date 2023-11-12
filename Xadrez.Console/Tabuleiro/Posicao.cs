namespace Xadrez.Console.Tabuleiro
{
    internal class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao()
        {
            
        }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString() => $"{Linha}, {Coluna}";

        #region Compare Methods

        public override int GetHashCode()
        {
            int baseNumber = 3;
            int hashAux = Linha.GetHashCode() + Coluna.GetHashCode();

            return hashAux * baseNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is not Posicao)
                return false;

            if (ReferenceEquals(this, (Posicao)obj))
                return true;

            var other = obj as Posicao;

            return Linha.Equals(other.Linha) && Coluna.Equals(other.Coluna);
        }

        public static bool operator ==(Posicao left, Posicao right)
        {
            if (left is null || right is null)
                return false;

            return Equals(left, right) || left.Linha == right.Linha && left.Coluna == right.Coluna;
        }

        public static bool operator !=(Posicao left, Posicao right) => !(left == right);

        #endregion
    }
}
