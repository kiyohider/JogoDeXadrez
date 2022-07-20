using System;
using tabuleiro;
using xadrez;

namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {

            PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

            while (!partidaDeXadrez.terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.imprimirPartida(partidaDeXadrez);


                    Console.Write("escolha uma peça: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    partidaDeXadrez.validarPosicaoDeOrigem(origem);

                    bool[,] movimentosPossiveis = partidaDeXadrez.tabuleiro.peca(origem).movimentosPossiveis();

                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.tabuleiro, movimentosPossiveis);


                    Console.WriteLine();
                    Console.Write("movimento da peça: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    partidaDeXadrez.validarPosicaoDeDestino(origem, destino);

                    partidaDeXadrez.realizaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Tela.imprimirPartida(partidaDeXadrez);




        }


    }
}
