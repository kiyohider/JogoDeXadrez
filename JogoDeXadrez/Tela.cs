using System;
using tabuleiro;
using xadrez;

namespace JogoDeXadrez
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partidaDeXadrez)
        {
            imprimirTabuleiro(partidaDeXadrez.tabuleiro);
            Console.WriteLine();
            imprimirPecasCapturadas(partidaDeXadrez);
            Console.WriteLine();

            if (!partidaDeXadrez.terminada)
            {
                Console.WriteLine("Turno: " + partidaDeXadrez.turno);
                Console.WriteLine("Aguardando jogada: " + partidaDeXadrez.jogadorAtual);
                if (partidaDeXadrez.xeque)
                {
                    Console.WriteLine("XEQUE !");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partidaDeXadrez.jogadorAtual);
            }
            
           
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partidaDeXadrez)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partidaDeXadrez.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partidaDeXadrez.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");
        }
        public static void imprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    imprimirPeca(tabuleiro.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void imprimirTabuleiro(Tabuleiro tabuleiro, bool[,] movimentosPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoMarcado = ConsoleColor.Green;

            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    if (movimentosPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoMarcado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tabuleiro.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

    }
}
