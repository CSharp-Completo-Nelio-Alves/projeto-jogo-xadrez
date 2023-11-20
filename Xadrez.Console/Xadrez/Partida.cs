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
        private readonly List<Peca> _pecasCapturada;
        private readonly List<Peca> _pecasEmJogo;

        public Tab.Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Finalizada { get; private set; }
        public bool ReiEmXeque { get; private set; }

        public Partida()
        {
            Tabuleiro = new Tab.Tabuleiro(linha: 8, coluna: 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;

            _pecasCapturada = new List<Peca>();
            _pecasEmJogo = new List<Peca>();

            ColocarPecas();
        }

        public void RealizarJogada()
        {
            var posicaoOrigem = ObterPosicao();
            var pecaSelecionada = Tabuleiro.ObterPeca(posicaoOrigem.ConverterParaPosicaoTabuleiro()) ?? throw new TabuleiroException($"Não existe peça na posição {posicaoOrigem}");

            ValidarPecaSelecionada(pecaSelecionada);

            Console.Clear();

            Tela.ImprimirPartida(this, pecaSelecionada);

            Console.WriteLine("\n");

            Tela.ImprimirMensagem($"Caso queira selecionar uma peça diferente, digite {posicaoOrigem} novamente.\n");

            var posicaoDestino = ObterPosicao(origem: false);

            // Permite cancelar a seleção de uma peça atual
            if (posicaoOrigem == posicaoDestino)
                return;

            if (!pecaSelecionada.ValidarSePodeMoverPara(posicaoDestino.ConverterParaPosicaoTabuleiro()))
                throw new TabuleiroException($"A peça da posição {posicaoOrigem} não pode se mover para a posição {posicaoDestino}");

            var ehMovimentoEspecial = ValidarSeMovimentoEspecial(pecaSelecionada, posicaoDestino.ConverterParaPosicaoTabuleiro());

            // TODO: Colocar execução de um movimento dentro do try/catch para desfazer o movimento caso dê erro
            var pecaCapturada = ExecutarMovimento(posicaoOrigem, posicaoDestino, ehMovimentoEspecial);

            if (ValidarSeReiEstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(pecaSelecionada, posicaoOrigem.ConverterParaPosicaoTabuleiro(), pecaCapturada, ehMovimentoEspecial);

                string mensagemException;

                if (ReiEmXeque)
                    mensagemException = "Seu rei está em xeque. Retire-o dessa situação";
                else
                    mensagemException = $"Você não pode movimentar a peça da posição {posicaoOrigem} para a posição {posicaoDestino}, pois colocará seu rei em Xeque";

                throw new TabuleiroException(mensagemException);
            }

            // Caso o rei tenha sido colocado em xeque pelo adversário na partida anterior e não esteja mais em xeque após movimento na partida atual
            if (ReiEmXeque)
            {
                var reiAtual = _pecasEmJogo.Find(p => p is Rei && p.Cor == JogadorAtual) as Rei;

                ReiEmXeque = false;
                reiAtual.EmXeque = false;
            }

            if (ValidarSeReiEstaEmXeque(RetornarCorAdversaria()))
            {
                var reiAdversario = _pecasEmJogo.Find(p => p is Rei && p.Cor == RetornarCorAdversaria()) as Rei;

                ReiEmXeque = true;
                reiAdversario.EmXeque = true;

                if (ValidarXequeMate(RetornarCorAdversaria()))
                {
                    Finalizada = true;

                    Console.Clear();
                    Tela.ImprimirPartida(this, pecaSelecionada);
                    Console.WriteLine("\n");
                    Tela.ImprimirMensagem("XEQUE-MATE!!!");
                    Tela.ImprimirMensagem($"\nPartida Finalizada. O vencedor são as Peças {JogadorAtual}");

                    Console.ReadKey();

                    return;
                }
            }

            DesfazerMarcacaoMovimentoEnPassant(JogadorAtual);
            TratarMovimentoEnPassantPeaoAdversario(pecaSelecionada, posicaoOrigem.ConverterParaPosicaoTabuleiro());

            Turno++;
            AlterarJogadorAtual();
        }

        public IEnumerable<Peca> ObterPecasCapturadas(Cor cor) => _pecasCapturada.Where(p => p.Cor == cor);

        #region Métodos Auxiliares

        private void AlterarJogadorAtual() => JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;

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

        private Peca ExecutarMovimento(PosicaoXadrez origem, PosicaoXadrez destino, bool validarSeMovimentoEspecial = false)
        {
            var posicaoOrigemTabuleiro = origem?.ConverterParaPosicaoTabuleiro();
            var posicaoDestinoTabuleiro = destino?.ConverterParaPosicaoTabuleiro();

            Peca pecaOrigem;

            try
            {
                pecaOrigem = Tabuleiro.RetirarPeca(posicaoOrigemTabuleiro) ?? throw new TabuleiroException($"Não existe peça na posição {origem}");
            }
            catch (TabuleiroException ex)
            {
                var exception = new TabuleiroException($"Erro ao retirar peça da origem: {ex.Message}", ex);

                throw exception;
            }

            //if (pecaOrigem is null)
            //    throw new TabuleiroException("Não existe peça na posição de origem");

            Peca pecaCapturada;

            try
            {
                //pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestinoTabuleiro);
                pecaCapturada = CapturarPeca(posicaoDestinoTabuleiro);
            }
            catch (TabuleiroException ex)
            {
                var exception = new TabuleiroException($"Erro ao capturar peça no destino: {ex.Message}", ex);

                // TODO: Chamar no método de desfazer movimento no catch do método que realiza jogada
                Tabuleiro.ColocarPeca(pecaOrigem, posicaoOrigemTabuleiro);

                throw exception;
            }

            Tabuleiro.ColocarPeca(pecaOrigem, posicaoDestinoTabuleiro);
            pecaOrigem.IncrementarMovimento();

            if (validarSeMovimentoEspecial)
                pecaCapturada = ExecutarMovimentoEspecial(pecaOrigem, posicaoOrigemTabuleiro, posicaoDestinoTabuleiro);

            //if (pecaCapturada is not null)
            //{
            //    _pecasCapturada.Add(pecaCapturada);
            //    var result = _pecasEmJogo.Remove(pecaCapturada);
            //}

            return pecaCapturada;
        }

        private Peca CapturarPeca(Posicao posicaoDestino)
        {
            Peca pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestino);

            if (pecaCapturada is not null)
            {
                _pecasCapturada.Add(pecaCapturada);
                _pecasEmJogo.Remove(pecaCapturada);
            }

            return pecaCapturada;
        }

        private Peca ExecutarMovimentoEspecial(Peca peca, Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            var rei = peca as Rei;

            if (rei is not null)
            {
                ExecutarMovimentoEspecialRoque(rei);

                return null;
            }

            var peao = peca as Peao;

            if (peao is not null)
                return ExecutarCapturaEnPassant(posicaoOrigem, posicaoDestino);

            return null;
        }

        private Peca ExecutarCapturaEnPassant(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            var posicaoPeaoAdversario = new Posicao(posicaoOrigem.Linha, posicaoDestino.Coluna);
            Peca peaoCapturado = CapturarPeca(posicaoPeaoAdversario) ?? throw new TabuleiroException("Não existe peão adversário que permita realizar captura en-passant.");

            return peaoCapturado;
        }

        private void ExecutarMovimentoEspecialRoque(Rei rei)
        {
            var torre = Tabuleiro.ObterPeca(new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna + 1));
            var posicaoDestinoTorre = new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna - 1);

            if (torre is null)
            {
                torre = Tabuleiro.ObterPeca(new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna - 2));
                posicaoDestinoTorre = new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna + 1);
            }

            if (torre is not null)
                ExecutarMovimento(new PosicaoXadrez(torre.Posicao), new PosicaoXadrez(posicaoDestinoTorre));
        }

        private bool ValidarSeMovimentoEspecial(Peca peca, Posicao posicaoDestino)
        {
            var rei = peca as Rei;

            if (rei is not null && (rei.ValidarSePodeRealizarMovimentoRoquePequeno(posicaoDestino) || rei.ValidarSePodeRealizarMovimentoRoqueGrande(posicaoDestino)))
                return true;

            var peao = peca as Peao;

            if (peao is not null && peao.ValidarSePodeRealizarMovimentoEnPassant(posicaoDestino))
                return true;

            return false;
        }

        private void DesfazerMovimento(Peca peca, Posicao posicaoOrigem, Peca pecaCapturada = null, bool ehMovimentoEspecial = false)
        {
            var posicaoAtual = peca.Posicao;

            Tabuleiro.RetirarPeca(peca.Posicao);
            Tabuleiro.ColocarPeca(peca, posicaoOrigem);
            peca.DecrementarMovimento();

            if (ehMovimentoEspecial)
                DesfazerMovimentoEspecial(peca);

            if (pecaCapturada is not null)
            {
                _pecasCapturada.Remove(pecaCapturada);
                Tabuleiro.ColocarPeca(pecaCapturada, posicaoAtual);
                _pecasEmJogo.Add(pecaCapturada);
            }
        }

        private void DesfazerMovimentoEspecial(Peca peca)
        {
            var pecaAux = peca as Rei;

            if (peca is not null)
                DesfazerMovimentoRoque(pecaAux);
        }

        private void DesfazerMovimentoRoque(Rei rei)
        {
            var torre = Tabuleiro.ObterPeca(new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna + 1)) as Torre;
            var posicaoOrigemTorre = new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna + 3);

            if (torre is null)
            {
                torre = Tabuleiro.ObterPeca(new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna - 1)) as Torre;
                posicaoOrigemTorre = new Posicao(rei.Posicao.Linha, rei.Posicao.Coluna - 4);
            }

            if (torre is not null)
                DesfazerMovimento(torre, posicaoOrigemTorre);
        }

        private bool ValidarSeReiEstaEmXeque(Cor cor)
        {
            var rei = _pecasEmJogo.First(p => p.Cor == cor && p is Rei);

            foreach (var peca in _pecasEmJogo.Where(p => p.Cor != cor))
            {
                if (peca.ValidarSePodeMoverPara(rei.Posicao))
                    return true;
            }

            return false;
        }

        private Cor RetornarCorAdversaria() => JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;

        private bool ValidarXequeMate(Cor cor)
        {
            if (!ReiEmXeque)
                return false;

            var pecasTeste = _pecasEmJogo.Where(p => p.Cor == cor).ToList().AsReadOnly();

            foreach (var peca in pecasTeste)
            {
                var posicaoOrigem = peca.Posicao;

                foreach (var posicaoDestino in peca.RetornarPosicoesParaMovimento())
                {
                    var pecaCapturada = ExecutarMovimento(new PosicaoXadrez(posicaoOrigem), new PosicaoXadrez(posicaoDestino));
                    var movimentoTirouReiDoXeque = !ValidarSeReiEstaEmXeque(cor);

                    DesfazerMovimento(peca, posicaoOrigem, pecaCapturada);

                    if (movimentoTirouReiDoXeque)
                        return false;
                }
            }

            return true;
        }

        private void TratarMovimentoEnPassantPeaoAdversario(Peca pecaMovimentada, Posicao posicaoOrigem)
        {
            if (pecaMovimentada is not Peao)
                return;

            var qtdCasasAvancadas = Math.Abs(pecaMovimentada.Posicao.Linha - posicaoOrigem.Linha);

            if (qtdCasasAvancadas != 2)
                return;

            Posicao posicaoPeaoAdversario = new(pecaMovimentada.Posicao.Linha, pecaMovimentada.Posicao.Coluna + 1);

            MarcarMovimentoEnPassantPeaoAdversario(posicaoPeaoAdversario);

            posicaoPeaoAdversario.Coluna = pecaMovimentada.Posicao.Coluna - 1;

            MarcarMovimentoEnPassantPeaoAdversario(posicaoPeaoAdversario);
        }

        private void MarcarMovimentoEnPassantPeaoAdversario(Posicao posicao)
        {
            if (!Tabuleiro.ValidarPosicao(posicao))
                return;

            Peao peao = Tabuleiro.ObterPeca(posicao) as Peao;

            if (peao is not null && peao.Cor == RetornarCorAdversaria())
                peao.PodeCapturarEnPassant = true;
        }

        private void DesfazerMarcacaoMovimentoEnPassant(Cor corJogador)
        {
            foreach (var peca in _pecasEmJogo.Where(p => p is Peao && p.Cor == corJogador && (p as Peao).PodeCapturarEnPassant).Select(p => p as Peao))
                peca.PodeCapturarEnPassant = false;
        }

        #endregion

        #region Validações

        private void ValidarPecaSelecionada(Tab.Peca peca)
        {
            PosicaoXadrez posicaoXadrez = new(peca.Posicao);

            if (!ValidarSePecaDoJogadorAtual(peca))
                throw new TabuleiroException($"Você não pode selecionar a peça na posição {posicaoXadrez}");

            if (!peca.ValidarSeExisteMovimentoPossivel())
                throw new TabuleiroException($"Não existem movimentos possíveis para a peça na posição {posicaoXadrez}");
        }

        private bool ValidarSePecaDoJogadorAtual(Tab.Peca peca) => peca.Cor == JogadorAtual;

        #endregion

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

            _pecasEmJogo.Add(peca1);
            _pecasEmJogo.Add(peca2);
        }

        private void ColocarCavalos(Cor cor = Cor.Branca)
        {
            Tab.Peca peca1 = new Cavalo(cor, Tabuleiro);
            Tab.Peca peca2 = new Cavalo(cor, Tabuleiro);
            PosicaoXadrez posicao1 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'b');
            PosicaoXadrez posicao2 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'g');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());

            _pecasEmJogo.Add(peca1);
            _pecasEmJogo.Add(peca2);
        }

        private void ColocarBispos(Cor cor = Cor.Branca)
        {
            Tab.Peca peca1 = new Bispo(cor, Tabuleiro);
            Tab.Peca peca2 = new Bispo(cor, Tabuleiro);
            PosicaoXadrez posicao1 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'c');
            PosicaoXadrez posicao2 = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'f');

            Tabuleiro.ColocarPeca(peca1, posicao1.ConverterParaPosicaoTabuleiro());
            Tabuleiro.ColocarPeca(peca2, posicao2.ConverterParaPosicaoTabuleiro());

            _pecasEmJogo.Add(peca1);
            _pecasEmJogo.Add(peca2);
        }

        private void ColocarRei(Cor cor = Cor.Branca)
        {
            Tab.Peca peca = new Rei(cor, Tabuleiro);
            PosicaoXadrez posicao = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'e');

            Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
            _pecasEmJogo.Add(peca);
        }

        private void ColocarDama(Cor cor = Cor.Branca)
        {
            Tab.Peca peca = new Dama(cor, Tabuleiro);
            PosicaoXadrez posicao = new(linha: cor == Cor.Branca ? 1 : 8, coluna: 'd');

            Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
            _pecasEmJogo.Add(peca);
        }

        private void ColocarPeoes(Cor cor = Cor.Branca)
        {
            for (char i = 'a'; i < 'i'; i++)
            {
                Tab.Peca peca = new Peao(cor, Tabuleiro);
                PosicaoXadrez posicao = new(linha: cor == Cor.Branca ? 2 : 7, coluna: i);

                Tabuleiro.ColocarPeca(peca, posicao.ConverterParaPosicaoTabuleiro());
                _pecasEmJogo.Add(peca);
            }
        }

        #endregion
    }
}
