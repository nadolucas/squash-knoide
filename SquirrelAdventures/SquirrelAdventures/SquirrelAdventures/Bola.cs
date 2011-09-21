using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Design;

namespace SquirrelAdventures
{
    class Bola
    {
        Rectangle bola;
        Texture2D imagem;
        KeyboardState tecladoAtual, tecladoAnterior;
       
        enum Direcao { DIREITA, ESQUERDA, CIMA, BAIXO, DIAGSUPDIR, DIAGSUPESQ, DIAGINFDIR, DIAGINFESQ, PARADA }
        
        Direcao direcao, direcaoAnt;
                
        public Bola( Texture2D imagem)
        {
            this.imagem = imagem;
            this.bola = new Rectangle(350,600,imagem.Width, imagem.Height);
            
        }
        
        public void Update(GameTime gameTime)
        {
            tecladoAtual = Keyboard.GetState();

            MovimentoBola(tecladoAtual);
            ColisaoTela();         

            tecladoAnterior = tecladoAtual;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(imagem, new Rectangle(bola.X, bola.Y, bola.Width/2, bola.Height/2), new Rectangle(0, 0, bola.Width, bola.Height), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            
        }

        #region COLISÃO DA BOLA COM O JOGADOR
        public void ColisaoJogador(Rectangle jogador, Texture2D imgJogador)
        {

            bool colidiu = false;
            
            Color[] dadosTexturaBola;
            Color[] dadosTexturaJogador;

            dadosTexturaBola = new Color[imagem.Width  * imagem.Height];
            imagem.GetData(dadosTexturaBola);

            dadosTexturaJogador = new Color[imgJogador.Width * imgJogador.Height];
            imgJogador.GetData(dadosTexturaJogador);

            int cima = Math.Max((int)(bola.Top / 2), (int)(jogador.Top / 2));
            int baixo = Math.Min((int)(bola.Bottom / 2), (int)(jogador.Bottom / 2));
            int esquerda = Math.Max((int)(bola.Left / 2), (int)(jogador.Left / 2));
            int direita = Math.Min((int)(bola.Right / 2), (int)(jogador.Right / 2));


            for (int y = cima; y < baixo; y++)
            {
                for (int x = esquerda; x < direita; x++)
                {
                    Color color1 = dadosTexturaBola[(x - (int)(bola.Left / 2)) + (y - (int)(bola.Top / 2)) * (int)(bola.Width / 2)];
                    Color color2 = dadosTexturaJogador[(x - (int)(jogador.Left / 2)) + (y - (int)(jogador.Top / 2)) * (int)(jogador.Width / 2)];

                    if (color1.A != 0 && color2.A != 0)
                    {
                        colidiu = true;
                    }

                }
            }


            if (colidiu)
            {

                if (direcao == Direcao.DIAGINFESQ)
                {
                    direcao = Direcao.DIAGSUPESQ;
                }

                if (direcao == Direcao.DIAGINFDIR)
                {
                    direcao = Direcao.DIAGSUPDIR;
                }

                if (direcao == Direcao.BAIXO)
                {
                    direcao = Direcao.CIMA;
                }

            }

        }
        #endregion

        #region COLISÃO DA BOLA COM A TELA
        public void ColisaoTela()
        {
            #region Lateral direita
            if (bola.X > 800 - bola.Width / 2)
            {
                if (direcao == Direcao.DIAGSUPDIR)
                {
                    direcao = Direcao.DIAGSUPESQ;
                }

                if (direcao == Direcao.DIAGINFDIR)
                {
                    direcao = Direcao.DIAGINFESQ;
                }

                if (direcao == Direcao.DIREITA)
                {
                    direcao = Direcao.ESQUERDA;
                }


            }

            #endregion
            #region Lateral esquerda
            if (bola.X <= 0)
            {
                if (direcao == Direcao.DIAGSUPESQ)
                {
                    direcao = Direcao.DIAGSUPDIR;
                }

                if (direcao == Direcao.DIAGINFESQ)
                {
                    direcao = Direcao.DIAGINFDIR;
                }

                if (direcao == Direcao.ESQUERDA)
                {
                    direcao = Direcao.DIREITA;
                }

            }

            #endregion
            #region piso

            if (bola.Y > 600 - bola.Width / 2)
            {

                direcao = Direcao.PARADA;

            }
            #endregion
            #region teto

            if (bola.Y <= 0)
            {
                if (direcao == Direcao.DIAGSUPESQ)
                {
                    direcao = Direcao.DIAGINFESQ;
                }

                if (direcao == Direcao.DIAGSUPDIR)
                {
                    direcao = Direcao.DIAGINFDIR;
                }

                if (direcao == Direcao.CIMA)
                {
                    direcao = Direcao.BAIXO;
                }

            }

            #endregion

        }
        #endregion

        #region MOVIMENTAÇÃO DA BOLA
        public void MovimentoBola(KeyboardState tecladoAtual)
        {
            direcaoAnt = direcao;
            if (tecladoAtual.IsKeyDown(Keys.Space))
            {
                if (direcao == Direcao.PARADA)
                {
                    direcao = Direcao.DIAGSUPDIR;
                }

            }
            #region direcoes verticais
            if (direcao == Direcao.CIMA)
            {
                bola.Y -= 5;
            }

            if (direcao == Direcao.BAIXO)
            {
                bola.Y += 5;
            }

            #endregion
            #region direcoes horizontais
            if (direcao == Direcao.DIREITA)
            {
                bola.X += 5;
            }

            if (direcao == Direcao.ESQUERDA)
            {
                bola.X -= 5;
            }
            #endregion
            #region direcoes diagonais
            if (direcao == Direcao.DIAGSUPDIR)
            {
                bola.X += 5;
                bola.Y -= 5;
            }

            if (direcao == Direcao.DIAGSUPESQ)
            {
                bola.X -= 5;
                bola.Y -= 5;
            }

            if (direcao == Direcao.DIAGINFDIR)
            {
                bola.X += 5;
                bola.Y += 5;
            }

            if (direcao == Direcao.DIAGINFESQ)
            {
                bola.X -= 5;
                bola.Y += 5;
            }
            #endregion

        }
        #endregion
 

    }

   }

