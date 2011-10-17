using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SquirrelAdventures
{
    class TelaMenu
    {
        private Texture2D telaMenu;
        private SpriteBatch render;
        private Texture2D botao, botaoAjuda, botaoCredito, botaoFim;
        private Texture2D ponteiroMouse;
        private Vector2 posicaoMouseXY = Vector2.Zero;
        private int statusBotaoStart, statusBotaoAjuda, statusBotaoCreditos, statusBotaoFim;

        Mensagem mensagem;

        public TelaMenu(Texture2D telaMenu, Texture2D botao, Texture2D botaoAjuda, Texture2D botaoCredito, Texture2D botaoFim, SpriteBatch render)
        {
            this.telaMenu = telaMenu;
            this.render = render;
            this.botao = botao;
            this.botaoAjuda = botaoAjuda;
            this.botaoCredito = botaoCredito;
            this.botaoFim = botaoFim;

            mensagem = Mensagem.TELA_MENU;
            
        }

        public void setaImnagemMouse(Texture2D imagem)
        {
            ponteiroMouse = imagem;
        }

        public Mensagem mensagemMenu
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

            #region botao start
            if ((posicaoMouseXY.X > 320 && posicaoMouseXY.X < 320 + 200) && (posicaoMouseXY.Y > 300 && posicaoMouseXY.Y < 300 + 50))
            {
                statusBotaoStart = 1;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    mensagem = Mensagem.FASE_1;
                }


            }
            #endregion
            else
            {
                #region botao ajuda
                if ((posicaoMouseXY.X > 320 && posicaoMouseXY.X < 320 + 200) && (posicaoMouseXY.Y > 360 && posicaoMouseXY.Y < 360 + 50))
                {
                    statusBotaoAjuda = 1;

                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        mensagem = Mensagem.TELA_AJUDA;
                    }


                }
                #endregion
                else
                {
                    #region botao creditos
                    if ((posicaoMouseXY.X > 320 && posicaoMouseXY.X < 320 + 200) && (posicaoMouseXY.Y > 420 && posicaoMouseXY.Y < 420 + 50))
                    {
                        statusBotaoCreditos = 1;

                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            mensagem = Mensagem.TELA_CREDITOS;
                        }


                    }
                    #endregion
                    else
                    {
                        #region botao sair
                        if ((posicaoMouseXY.X > 320 && posicaoMouseXY.X < 320 + 200) && (posicaoMouseXY.Y > 480 && posicaoMouseXY.Y < 480 + 50))
                        {
                            statusBotaoFim = 1;

                            if (mouse.LeftButton == ButtonState.Pressed)
                            {
                                mensagem = Mensagem.FIM;
                            }


                        }
                        #endregion
                        else
                        {
                            statusBotaoStart = 0;
                            statusBotaoAjuda = 0;
                            statusBotaoCreditos = 0;
                            statusBotaoFim = 0;
                        }
                    }
                }
            }

        }

        public void draw(GameTime gameTime, SpriteBatch render)
        {

            render.Draw(telaMenu, new Rectangle(0, 0, 800, 600), Color.White);
            render.Draw(botao, new Vector2(320, 300), new Rectangle(200 * statusBotaoStart, 0, 200, 50), Color.White);
            render.Draw(botaoAjuda, new Vector2(320, 360), new Rectangle(200 * statusBotaoAjuda, 0, 200, 50), Color.White);
            render.Draw(botaoCredito, new Vector2(320, 420), new Rectangle(200 * statusBotaoCreditos, 0, 200, 50), Color.White);
            render.Draw(botaoFim, new Vector2(320, 480), new Rectangle(200 * statusBotaoFim, 0, 200, 50), Color.White);
            render.Draw(ponteiroMouse, posicaoMouseXY, Color.White);
        }

    }

}