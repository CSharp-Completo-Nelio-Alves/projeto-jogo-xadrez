using Xadrez.ConsoleApp;
using Tabuleiro = Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Jogo = Xadrez.ConsoleApp.Xadrez;

Tabuleiro.Tabuleiro tabuleiro = new(8, 8);

Tabuleiro.Peca torre = new Jogo.Torre(Cor.Branca, tabuleiro);
Tabuleiro.Peca rei = new Jogo.Rei(Cor.Branca, tabuleiro);
Jogo.Torre torre2 = new(Cor.Branca, tabuleiro);
Jogo.Torre torre3 = new(Cor.Preta, tabuleiro);

var posicaoTorre = new Jogo.Posicao();
var posicaoTorre2 = new Jogo.Posicao(3, 'd');
var posicaoRei = new Jogo.Posicao(7, 'F');
var posicaoTorre3 = new Jogo.Posicao(5, 'e');

var posicao1 = posicaoTorre.ConverterParaPosicaoTabuleiro();
var posicao2 = posicaoTorre2.ConverterParaPosicaoTabuleiro();
var posicao3 = posicaoRei.ConverterParaPosicaoTabuleiro();
var posicao4 = posicaoTorre3.ConverterParaPosicaoTabuleiro();

tabuleiro.ColocarPeca(torre, posicao1);
tabuleiro.ColocarPeca(torre2, posicao2);
tabuleiro.ColocarPeca(rei, posicao3);
tabuleiro.ColocarPeca(torre3, posicao4);

Tela.ImprimirTabuleiro(tabuleiro);