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

namespace SquirrelAdventures
{
    class Personagem
    {
        Rectangle jogador;
        Texture2D imagem;
        KeyboardState tecladoAtual, tecladoAnterior;
        Vector2 origemXY;

        enum Direcao { DIREITA, ESQUERDA }
        Direcao direcao;
        Direcao direcaoAnt;

        Vector2 posicaoJog = new Vector2(100, 400);

        float orientacao;


        public Personagem(Rectangle jogador)
        {
            this.jogador = jogador;
            this.origemXY = new Vector2(jogador.X, jogador.Y);
            
        }
        
        public void Load(Texture2D imagem)
        {
            this.imagem = imagem;
            
        }

        public void Update(GameTime gameTime)
        {
            tecladoAtual = Keyboard.GetState();

            #region MOVIMENTAÇÃO DO JOGADOR

            direcaoAnt = direcao;

            if (tecladoAtual.IsKeyDown(Keys.Right))
            {
                jogador.X+=5;
                direcao = Direcao.DIREITA;
                orientacao = 0f;
            }
            if (tecladoAtual.IsKeyDown(Keys.Left))
            {
                jogador.X-=5;
                direcao = Direcao.ESQUERDA;
                orientacao = 0f;
            }

            #endregion

            #region COLISÃO DO JOGADOR COM A TELA

            if (jogador.X > 800 - jogador.Width)
            {
                jogador.X = 800-jogador.Width ;
            }
            if (jogador.X < 0)
            {
                jogador.X = 0;
            }

            #endregion 

            tecladoAnterior = tecladoAtual;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (direcao == Direcao.ESQUERDA)
            {
                //spriteBatch.Draw(imagem, jogador, jogador, Color.White, 0, origemXY, SpriteEffects.FlipHorizontally, 0.5f);
            spriteBatch.Draw(imagem, new Rectangle((int)jogador.X, (int)jogador.Y, imagem.Width / 2, imagem.Height / 2), new Rectangle(0, 0, imagem.Width, imagem.Height), Color.White, orientacao,
                            new Vector2(imagem.Width / 2, imagem.Height / 2), SpriteEffects.FlipHorizontally, 0);
            }
            else
            {  
                spriteBatch.Draw(imagem, new Rectangle((int)jogador.X, (int)jogador.Y, imagem.Width / 2, imagem.Height / 2), new Rectangle(0, 0, imagem.Width, imagem.Height), Color.White, orientacao,
                           new Vector2(imagem.Width / 2, imagem.Height / 2), SpriteEffects.None, 0);
            }

            


        }
    }

   }
