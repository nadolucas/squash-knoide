using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SquirrelAdventures
{
    class Objeto
    {
        private Texture2D imagem;
        private Vector2 posicaoOrigem;
        private Vector2 posicao;
        public int pontuacao;
        public Rectangle rect;

        public Objeto(Texture2D imagem, Vector2 posicao, Vector2 tam, int col, int lin, int pontuacao)
        {
            // TODO: Construct any child components here
            this.imagem = imagem;
            this.posicaoOrigem = new Vector2(posicao.X + (tam.X * col) + ((tam.X - imagem.Width) / 2), posicao.Y + tam.Y * lin);
            this.posicao = this.posicaoOrigem;
            this.pontuacao = pontuacao;
            this.rect = new Rectangle((int)this.posicao.X, (int)this.posicao.Y, this.imagem.Width, this.imagem.Height);

        }


        public void ajustaOffSet(Vector2 newOffSet)
        {
            this.posicaoOrigem.X += newOffSet.X;
            this.posicaoOrigem.Y += newOffSet.Y;
        }

        public void update(GameTime gameTime, Vector2 newPos)
        {
            this.posicao.X = this.posicaoOrigem.X + newPos.X;
            this.posicao.Y = this.posicaoOrigem.Y + newPos.Y;
            this.rect = new Rectangle((int)this.posicao.X, (int)this.posicao.Y, this.imagem.Width, this.imagem.Height);
        }

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.imagem, this.posicao,
                                       new Rectangle(0, 0, this.imagem.Bounds.Width,
                                       this.imagem.Bounds.Height),
                                       Color.White, 0f, Vector2.Zero,
                                       1f,
                                       SpriteEffects.None,
                                       0.1f
                                       );
        }
    }

}




