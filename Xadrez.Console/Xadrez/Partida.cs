using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Tabuleiro.Exceptions;
using Xadrez.ConsoleApp.Xadrez.Pecas;

namespace Xadrez.ConsoleApp.Xadrez
{
    internal class Partida
    {
        public Tabuleiro.Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; set; }
        public bool Finalizada { get; private set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro.Tabuleiro(linha: 8, coluna: 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;

            ColocarPecas();
        }

        public Posicao ObterPosicao(bool origem = true)
        {
            Console.Write($"Posição de {(origem ? "origem" : "destino")}: ");
            string userInput = Console.ReadLine();

            char coluna = userInput[0];
            int linha = int.Parse(userInput[1].ToString());

            return new Posicao(linha, coluna);
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            var posicaoOrigemTabuleiro = origem?.ConverterParaPosicaoTabuleiro();
            var posicaoDestinoTabuleiro = destino?.ConverterParaPosicaoTabuleiro();

            Peca pecaOrigem;

            try
            {
                pecaOrigem = Tabuleiro.RetirarPeca(posicaoOrigemTabuleiro);
            }
            catch (TabuleiroException ex)
            {
                var exception = new TabuleiroException($"Erro ao retirar peça da origem: {ex.Message}", ex);

                throw exception;
            }

            Peca pecaDestino;

            try
            {
                pecaDestino = Tabuleiro.RetirarPeca(posicaoDestinoTabuleiro);
            }
            catch (TabuleiroException ex)
            {
                var exception = new TabuleiroException($"Erro ao retirar peça do destino: {ex.Message}", ex);
                
                throw exception;
            }

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
