using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }    
        private int turno;
        private Cor joadorAtual;
        public bool terminada { get; private set; } 

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            joadorAtual = Cor.Branca;
            terminada = false;   
            colocarPecas();

        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.retirarPeca(origem);
            p.incrementarMovimento();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);
        }

        private void colocarPecas()
        {
           
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('a', 1).toPosicao());
            //tabuleiro.colocarPeca(new Cavalo(Cor.Preta, tabuleiro), new PosicaoXadrez('b', 1).toPosicao());
            //tabuleiro.colocarPeca(new Bispo(Cor.Preta, tabuleiro), new PosicaoXadrez('c', 1).toPosicao());
            tabuleiro.colocarPeca(new Rei(Cor.Preta, tabuleiro), new PosicaoXadrez('d', 1).toPosicao());
            //tabuleiro.colocarPeca(new Dama(Cor.Preta, tabuleiro), new PosicaoXadrez('e', 1).toPosicao());
            //tabuleiro.colocarPeca(new Bispo(Cor.Preta, tabuleiro), new PosicaoXadrez('f', 1).toPosicao());
            //tabuleiro.colocarPeca(new Cavalo(Cor.Preta, tabuleiro), new PosicaoXadrez('g', 1).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('h', 1).toPosicao());

            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('a', 8).toPosicao());
            //tabuleiro.colocarPeca(new Cavalo(Cor.Branca, tabuleiro), new PosicaoXadrez('b', 8).toPosicao());
            //tabuleiro.colocarPeca(new Bispo(Cor.Branca, tabuleiro), new PosicaoXadrez('c', 8).toPosicao());
            tabuleiro.colocarPeca(new Rei(Cor.Branca, tabuleiro), new PosicaoXadrez('d', 8).toPosicao());
            //tabuleiro.colocarPeca(new Dama(Cor.Branca, tabuleiro), new PosicaoXadrez('e', 8).toPosicao());
            //tabuleiro.colocarPeca(new Bispo(Cor.Branca, tabuleiro), new PosicaoXadrez('f', 8).toPosicao());
            //tabuleiro.colocarPeca(new Cavalo(Cor.Branca, tabuleiro), new PosicaoXadrez('g', 8).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('h', 8).toPosicao());
            Console.WriteLine();
            
        }
    }
}
