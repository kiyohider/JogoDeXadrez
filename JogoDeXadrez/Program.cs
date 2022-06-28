using System;
using tabuleiro;
using xadrez;

namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

                while (!partidaDeXadrez.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.tabuleiro);

                    Console.WriteLine();

                    Console.Write("escolha uma peça: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    Console.Write("larga peça: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partidaDeXadrez.executarMovimento(origem, destino); 
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }



            Console.ReadLine();
        }


    }
}
