using Xadrez.ConsoleApp;
using Xadrez.ConsoleApp.Tabuleiro;
using Xadrez.ConsoleApp.Tabuleiro.Enums;
using Xadrez.ConsoleApp.Xadrez;

Tabuleiro tabuleiro = new(8, 8);

Peca torre = new Torre(Cor.Branca, tabuleiro);
Peca rei = new Rei(Cor.Branca, tabuleiro);
Torre torre2 = new(Cor.Branca, tabuleiro);

tabuleiro.ColocarPeca(torre, new Posicao());
tabuleiro.ColocarPeca(torre2, new Posicao());
tabuleiro.ColocarPeca(rei, new Posicao(0, 5));

Tela.ImprimirTabuleiro(tabuleiro);