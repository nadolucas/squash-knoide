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
        
        enum Direcao { DIREITA, ESQUERDA }
        Direcao direcao;
        Direcao direcaoAnt;

        public Personagem( Texture2D imagem)
        {
            this.imagem = imagem;
            this.jogador = new Rectangle(350,500,imagem.Width, imagem.Height);
            
        }
        
        public Rectangle GetRetangulo()
        {
            return jogador;
            
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
            
            if (direcao == Direcao.ESQUERDA)
            {
                spriteBatch.Draw(imagem, new Rectangle(jogador.X, jogador.Y, jogador.Width / 2, jogador.Height / 2), new Rectangle(0, 0, jogador.Width, jogador.Height), Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
            else
            {
                spriteBatch.Draw(imagem, new Rectangle(jogador.X, jogador.Y, jogador.Width / 2, jogador.Height / 2), new Rectangle(0, 0, jogador.Width, jogador.Height), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            }

        }

        #region COLISÃO DA BOLA COM A TELA
        public void ColisaoTela()
        {
            if (jogador.X > 800 - jogador.Width / 2)
            {
                jogador.X = 800 - jogador.Width / 2;
            }

            if (jogador.X <= 0)
            {
                jogador.X = 0;
            }

        }
        #endregion

        #region MOVIMENTAÇÃO DA BOLA
        public void MovimentoBola(KeyboardState tecladoAtual)
        {
            direcaoAnt = direcao;

            if (tecladoAtual.IsKeyDown(Keys.Right))
            {
                jogador.X += 5;
                direcao = Direcao.DIREITA;

            }
            if (tecladoAtual.IsKeyDown(Keys.Left))
            {
                jogador.X -= 5;
                direcao = Direcao.ESQUERDA;

            }

        }
        #endregion

    }

   }
