﻿using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Xadrez;

namespace Xadrez.ConsoleApp.Tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; private set; }
        public int QuantidadeMovimento { get; private set; }
        public Tabuleiro Tabuleiro { get; private set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Cor = cor;
            Tabuleiro = tabuleiro;
        }

        #region Métodos de Comparação

        public override int GetHashCode()
        {
            int baseNumber = 5;
            int hashAux = Cor.GetHashCode() + GetType().GetHashCode();

            return hashAux * baseNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is not Peca)
                return false;

            if (GetType() != obj.GetType())
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as Peca;

            return Cor.Equals(other.Cor);
        }

        public static bool operator ==(Peca left, Peca right)
        {
            if (left is null || right is null)
                return false;

            return Equals(left, right) || left.GetType() == right.GetType() && left.Cor == right.Cor;
        }

        public static bool operator !=(Peca left, Peca right) => !(left == right);

        #endregion
    }
}