using System.Diagnostics;
using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Xadrez.Pecas;

namespace Xadrez.ConsoleApp.Xadrez
{
    internal class Partida
    {
        public Tabuleiro.Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro.Tabuleiro(linha: 8, coluna: 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;

            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            var posicaoOrigemTabuleiro = origem?.ConverterParaPosicaoTabuleiro();
            var posicaoDestinoTabuleiro = destino?.ConverterParaPosicaoTabuleiro();

            var pecaOrigem = Tabuleiro.RetirarPeca(posicaoOrigemTabuleiro);
            var pecaDestino = Tabuleiro.RetirarPeca(posicaoDestinoTabuleiro);

            Tabuleiro.ColocarPeca(pecaOrigem, posicaoDestinoTabuleiro);
            pecaOrigem.IncrementarMovimento();
        }

        #region Colocar Peças

        private void ColocarPecas()
        {
            ColocarPecasBrancas();
            ColocarPecasPretas();
        }

        private void ColocarPecasBrancas()
        {
            ColocarTorres();
            ColocarCavalos();
            ColocarBispos();

            ColocarRei();
            ColocarDama();

            ColocarPeoes();
        }

        private void ColocarPecasPretas()
        {
            ColocarTorres(Cor.Preta);
            ColocarCavalos(Cor.Preta);
            ColocarBispos(Cor.Preta);

            ColocarRei(Cor.Preta);
            ColocarDama(Cor.Preta);

            ColocarPeoes(Cor.Preta);
        }

        private void ColocarTorres(Cor cor = Cor.Branca)
        {
            Peca peca1 = new Torre(cor, Tabuleiro);
            Peca peca2 = new Torre(cor, Tabuleiro);
            Posicao posicao1 = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'a');
            Posicao posicao2 = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'h');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarCavalos(Cor cor = Cor.Branca)
        {
            Peca peca1 = new Cavalo(cor, Tabuleiro);
            Peca peca2 = new Cavalo(cor, Tabuleiro);
            Posicao posicao1 = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'b');
            Posicao posicao2 = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'g');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarBispos(Cor cor = Cor.Branca)
        {
            Peca peca1 = new Bispo(cor, Tabuleiro);
            Peca peca2 = new Bispo(cor, Tabuleiro);
            Posicao posicao1 = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'c');
            Posicao posicao2 = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'f');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarRei(Cor cor = Cor.Branca)
        {
            Peca peca = new Rei(cor, Tabuleiro);
            Posicao posicao = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'd');

            Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarDama(Cor cor = Cor.Branca)
        {
            Peca peca = new Dama(cor, Tabuleiro);
            Posicao posicao = new(linha: cor == Cor.Branca ? 8 : 1, coluna: 'e');

            Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarPeoes(Cor cor = Cor.Branca)
        {
            for (char i = 'a'; i < 'i'; i++)
            {
                Peca peca = new Peao(cor, Tabuleiro);
                Posicao posicao = new Posicao(linha: cor == Cor.Branca ? 7 : 2, coluna: i);
                Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
            }
        }

        #endregion
    }
}
