using Xadrez.Console.Tabuleiro;

TestarPosicao();


static void TestarPosicao()
{
    Posicao posicao = new();

    Console.WriteLine($"Posição {posicao}");

    Posicao posicao1 = new(5, 9);

    Console.WriteLine($"Posição {posicao1}");

    Console.WriteLine($"Posições iguais? {posicao == posicao1}");
}
