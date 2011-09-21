using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SquirrelAdventures
{
    class TelaCreditos
    {
        private Texture2D telaCreditos;
        private SpriteBatch render;
        private Texture2D botao;
        private Texture2D ponteiroMouse;
        private Vector2 posicaoMouseXY = Vector2.Zero;
        private int statusBotao;

        Mensagem mensagem;

        TelaMenu menu;


        public TelaCreditos(Texture2D telaCreditos, Texture2D botao, SpriteBatch render, TelaMenu menu)
        {
            this.telaCreditos = telaCreditos;
            this.render = render;
            this.botao = botao;
            this.menu = menu;
            
            mensagem = Mensagem.TELA_CREDITOS;
            
        }

        public void setaImnagemMouse(Texture2D imagem)
        {
            ponteiroMouse = imagem;
        }

        
        public void inicializa()
        {
            
        }

        public Mensagem mensagemAjuda
        {
            set
            {
                mensagem = value;
            }

            get
            {
                return mensagem;
            }

        }


        public void update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            posicaoMouseXY = new Vector2(mouse.X, mouse.Y);

            #region botao voltar
            if ((posicaoMouseXY.X > 600 && posicaoMouseXY.X < 600 + 200) && (posicaoMouseXY.Y > 500 && posicaoMouseXY.Y < 500 + 50))
            {
                statusBotao = 1;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    menu.mensagemMenu = Mensagem.TELA_MENU;
                    mensagem = Mensagem.TELA_MENU;
                }


            }
            #endregion
            else
            {
                statusBotao = 0;
                        
            }

        }

        public void draw(GameTime gameTime, SpriteBatch render)
        {

            render.Draw(telaCreditos, new Rectangle(0, 0, 800, 600), Color.White);
            render.Draw(botao, new Vector2(600, 500), new Rectangle(200 * statusBotao, 0, 200, 50), Color.White);
            render.Draw(ponteiroMouse, posicaoMouseXY, Color.White);

            menu.mensagemMenu = Mensagem.TELA_CREDITOS;
        }

        
    }

}