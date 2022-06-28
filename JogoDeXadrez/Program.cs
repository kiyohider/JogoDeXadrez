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

                Tela.imprimirTabuleiro(partidaDeXadrez.tabuleiro);

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }



            Console.ReadLine();
        }


    }
}
