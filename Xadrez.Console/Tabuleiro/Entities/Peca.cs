﻿using Xadrez.ConsoleApp.Tabuleiro.Enums;

namespace Xadrez.ConsoleApp.Tabuleiro.Entities
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

        public virtual bool ValidarMovimento(Posicao posicaoDestino)
        {
            if (!Tabuleiro.ValidarPosicao(posicaoDestino))
                return false;

            Peca peca = Tabuleiro.ObterPeca(posicaoDestino);

            if (peca is not null && peca.Cor == Cor)
                return false;

            return true;
        }

        public bool ValidarSeExistePecaAdversaria(Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.ObterPeca(posicaoDestino);

            return peca is not null && peca.Cor != Cor;
        }

        public bool ValidarSeExisteMovimentoPossivel()
        {
            var movimentosPossiveis = RetornarMovimentosPossiveis();

            foreach (var movimentoPossivel in movimentosPossiveis)
            {
                if (movimentoPossivel)
                    return true;
            }

            return false;
        }

        public bool ValidarSePodeMoverPara(Posicao posicaoDestino) => RetornarMovimentosPossiveis()[posicaoDestino.Linha, posicaoDestino.Coluna];

        public void IncrementarMovimento() => QuantidadeMovimento++;
        public void DecrementarMovimento() => QuantidadeMovimento--;

        public abstract bool[,] RetornarMovimentosPossiveis();

        public List<Posicao> RetornarPosicoesParaMovimento()
        {
            var lista = new List<Posicao>();
            var movimentosPossiveis = RetornarMovimentosPossiveis();

            for (var i = 0; i < movimentosPossiveis.GetLength(0); i++)
            {
                for (var j = 0; j < movimentosPossiveis.GetLength(1); j++)
                {
                    if (movimentosPossiveis[i, j])
                        lista.Add(new Posicao(i, j));
                }
            }

            return lista;
        }

        #region Métodos de Comparação

        public override int GetHashCode()
        {
            int baseNumber = 5;
            int hashAux = Cor.GetHashCode() + Posicao.GetHashCode();

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

            return Cor.Equals(other.Cor) && Posicao.Equals(other.Posicao);
        }

        public static bool operator ==(Peca left, Peca right)
        {
            if (left is null || right is null)
                return false;

            return Equals(left, right) || left.GetType() == right.GetType() && left.Cor == right.Cor && left.Posicao == right.Posicao;
        }

        public static bool operator !=(Peca left, Peca right) => !(left == right);

        #endregion
    }
}
