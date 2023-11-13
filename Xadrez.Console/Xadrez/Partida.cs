using Tab = Xadrez.ConsoleApp.Tabuleiro.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Tabuleiro.Exceptions;
using Xadrez.ConsoleApp.Xadrez.Entities.Pecas;
using Xadrez.ConsoleApp.Xadrez.Entities;
using Xadrez.ConsoleApp.Tabuleiro.Entities;

namespace Xadrez.ConsoleApp.Xadrez
{
    internal class Partida
    {
        public Tab.Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Finalizada { get; private set; }

        public Partida()
        {
            Tabuleiro = new Tab.Tabuleiro(linha: 8, coluna: 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;

            ColocarPecas();
        }

        public void RealizarJogada()
        {
            var posicaoOrigem = ObterPosicao();
            var pecaSelecionada = Tabuleiro.ObterPeca(posicaoOrigem.ConverterParaPosicaoTabuleiro());

            if (pecaSelecionada is null)
                throw new TabuleiroException($"Não existe peça na posição {posicaoOrigem}");

            ValidarPecaSelecionada(pecaSelecionada);

            Console.Clear();

            Tela.ImprimirCabecalho(this);
            Tela.ImprimirTabuleiro(Tabuleiro, pecaSelecionada);

            Console.WriteLine("\n");

            Tela.ImprimirMensagem($"Caso queira selecionar uma peça diferente, digite {posicaoOrigem} novamente.\n");

            var posicaoDestino = ObterPosicao(origem: false);

            // Permite cancelar a seleção de uma peça atual
            if (posicaoOrigem == posicaoDestino)
                return;

            if (!pecaSelecionada.ValidarSePodeMoverPara(posicaoDestino.ConverterParaPosicaoTabuleiro()))
                throw new TabuleiroException($"A peça da posição {posicaoOrigem} não pode se mover para a posição {posicaoDestino}");

            ExecutarMovimento(posicaoOrigem, posicaoDestino);

            Turno++;
            AlterarJogadorAtual();
        }

        private void AlterarJogadorAtual() => JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;

        private void ValidarPecaSelecionada(Tab.Peca peca)
        {
            PosicaoXadrez posicaoXadrez = new(peca.Posicao);

            if (!ValidarSePecaDoJogadorAtual(peca))
                throw new TabuleiroException($"Você não pode selecionar a peça na posição {posicaoXadrez}");

            if (!peca.ValidarSeExisteMovimentoPossivel())
                throw new TabuleiroException($"Não existem movimentos possíveis para a peça na posição {posicaoXadrez}");
        }

        private bool ValidarSePecaDoJogadorAtual(Tab.Peca peca) => peca.Cor == JogadorAtual;

        private PosicaoXadrez ObterPosicao(bool origem = true)
        {
            try
            {
                Console.Write($"Posição de {(origem ? "origem" : "destino")}: ");
                string userInput = Console.ReadLine();

                char coluna = userInput[0];
                int linha = int.Parse(userInput[1].ToString());

                return new PosicaoXadrez(linha, coluna);
            }
            catch (Exception ex)
            {
                var exception = new TabuleiroException("Necessário informar uma posição válida", ex);

                throw exception;
            }
        }

        private void ExecutarMovimento(PosicaoXadrez origem, PosicaoXadrez destino)
        {
            var posicaoOrigemTabuleiro = origem?.ConverterParaPosicaoTabuleiro();
            var posicaoDestinoTabuleiro = destino?.ConverterParaPosicaoTabuleiro();

            Tab.Peca pecaOrigem;

            try
            {
                pecaOrigem = Tabuleiro.RetirarPeca(posicaoOrigemTabuleiro);
            }
            catch (TabuleiroException ex)
            {
                var exception = new TabuleiroException($"Erro ao retirar peça da origem: {ex.Message}", ex);

                throw exception;
            }

            if (pecaOrigem is null)
                throw new TabuleiroException("Não existe peça na posição de origem");

            Tab.Peca pecaDestino;

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
            Tab.Peca peca1 = new Torre(cor, Tabuleiro);
            Tab.Peca peca2 = new Torre(cor, Tabuleiro);
            PosicaoXadrez posicao1 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'a');
            PosicaoXadrez posicao2 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'h');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarCavalos(Cor cor = Cor.Branca)
        {
            Tab.Peca peca1 = new Cavalo(cor, Tabuleiro);
            Tab.Peca peca2 = new Cavalo(cor, Tabuleiro);
            PosicaoXadrez posicao1 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'b');
            PosicaoXadrez posicao2 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'g');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarBispos(Cor cor = Cor.Branca)
        {
            Tab.Peca peca1 = new Bispo(cor, Tabuleiro);
            Tab.Peca peca2 = new Bispo(cor, Tabuleiro);
            PosicaoXadrez posicao1 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'c');
            PosicaoXadrez posicao2 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'f');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarRei(Cor cor = Cor.Branca)
        {
            Tab.Peca peca = new Rei(cor, Tabuleiro);
            PosicaoXadrez posicao = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'e');

            Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarDama(Cor cor = Cor.Branca)
        {
            Tab.Peca peca = new Dama(cor, Tabuleiro);
            PosicaoXadrez posicao = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'd');

            Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
        }

        private void ColocarPeoes(Cor cor = Cor.Branca)
        {
            for (char i = 'a'; i < 'i'; i++)
            {
                Tab.Peca peca = new Peao(cor, Tabuleiro);
                PosicaoXadrez posicao = new(linha: cor == Cor.Branca ? 2 : 7, coluna: i);
                Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
            }
        }

        #endregion
    }
}
