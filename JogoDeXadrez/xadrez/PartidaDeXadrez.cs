﻿using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
         public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();

        }

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.retirarPeca(origem);
            p.incrementarMovimento();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            //#especial roque pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1 );
                Peca T = tabuleiro.retirarPeca(origemT);
                T.incrementarMovimento();
                tabuleiro.colocarPeca(T, destinoT);
            }
            //#especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tabuleiro.retirarPeca(origemT);
                T.incrementarMovimento();
                tabuleiro.colocarPeca(T, destinoT);
            }

            //$especial en passant
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tabuleiro.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void movimentoInvalido(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = tabuleiro.retirarPeca(destino);
            peca.decrementarMovimento();
            if (pecaCapturada != null)
            {
                tabuleiro.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tabuleiro.colocarPeca(peca, origem);

            //#especial roque pequeno
            if (peca is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tabuleiro.retirarPeca(destinoT);
                T.decrementarMovimento();
                tabuleiro.colocarPeca(T, origemT);
            }

            //#especial roque grande
            if (peca is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tabuleiro.retirarPeca(destinoT);
                T.decrementarMovimento();
                tabuleiro.colocarPeca(T, origemT);
            }

            //#especial en passant
            if(peca is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tabuleiro.retirarPeca(destino);
                    Posicao posP;
                    if(peca.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tabuleiro.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimento(origem, destino);

            if (emXeque(jogadorAtual))
            {
                movimentoInvalido(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tabuleiro.peca(destino);

            //# especial promocao
            if(p is Peao)
            {
                if((p.cor == Cor.Branca && destino.linha == 0)||(p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tabuleiro.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(p.cor, tabuleiro);
                    tabuleiro.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (emXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (xequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            

            //# jogada enPassant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
         

        }

        public void validarPosicaoDeOrigem(Posicao posicao)
        {
            if (tabuleiro.peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }
            if (jogadorAtual != tabuleiro.peca(posicao).cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!tabuleiro.peca(posicao).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não a movimentos possiveis para esta peça!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }

        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool emXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool xequeMate(Cor cor)
        {
            if (!emXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i<tabuleiro.linhas; i++)
                {
                    for(int j = 0; j<tabuleiro.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool xequeMate = emXeque(cor);
                            movimentoInvalido(origem, destino, pecaCapturada);
                            if (!xeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {

            colocarNovaPeca('a', 1, new Torre(Cor.Branca, tabuleiro));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tabuleiro));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, tabuleiro));
            colocarNovaPeca('d', 1, new Dama(Cor.Branca, tabuleiro));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, tabuleiro));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tabuleiro));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, tabuleiro));
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, tabuleiro, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, tabuleiro, this));



            colocarNovaPeca('a', 8, new Torre(Cor.Preta, tabuleiro));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tabuleiro));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, tabuleiro));
            colocarNovaPeca('d', 8, new Dama(Cor.Preta, tabuleiro));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, tabuleiro));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tabuleiro));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, tabuleiro));
            colocarNovaPeca('a', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, tabuleiro, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, tabuleiro, this));
            Console.WriteLine();

        }
    }
}
