using System;
using tabuleiro;
using xadrez;

namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new Tabuleiro(8,8);
            
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
            tabuleiro.colocarPeca(new Cavalo(Cor.Preta, tabuleiro), new Posicao(0, 1));
            tabuleiro.colocarPeca(new Bispo(Cor.Preta, tabuleiro), new Posicao(0, 2));
            tabuleiro.colocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(0, 3));
            tabuleiro.colocarPeca(new Dama(Cor.Preta, tabuleiro), new Posicao(0, 4));
            tabuleiro.colocarPeca(new Bispo(Cor.Preta, tabuleiro), new Posicao(0, 5));
            tabuleiro.colocarPeca(new Cavalo(Cor.Preta, tabuleiro), new Posicao(0, 6));
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 7));


            Tela.imprimirTabuleiro(tabuleiro); 

        }


    }
}
