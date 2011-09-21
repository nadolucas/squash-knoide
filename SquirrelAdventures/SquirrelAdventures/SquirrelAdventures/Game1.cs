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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Personagem jogador;
        Rectangle retJogador;

        Texture2D imagemJog;
        Vector2 posicaoJog = new Vector2(100, 100);

        Bola bola;
        const int COL_OBJETOS = 14;
        const int LIN_OBJETOS = 4;
        const int OBJETO_SIZE_X = 60;
        const int OBJETO_SIZE_Y = 40;

        List<Objeto> objetos = new List<Objeto>();

        Vector2 objetoPosOffSet = new Vector2(10, 10);

        Rectangle objetoBlocoBound = new Rectangle(10, 10, (OBJETO_SIZE_X * LIN_OBJETOS) - 10, (OBJETO_SIZE_Y * COL_OBJETOS) - 10);
        Rectangle objetoBlocoBoundCorrigido = new Rectangle(10, 10, (OBJETO_SIZE_X * LIN_OBJETOS) - 10, (OBJETO_SIZE_Y * COL_OBJETOS) - 10);


        Texture2D imagemBola;
        Vector2 posicaoBola = new Vector2(120, 100);

        //enum Telas { MENU, FASE1, FASE2, FASE3, GAMEOVER, FIM}

        //Telas tela_atual = Telas.MENU;
        Texture2D telaMenu, telaAjuda, telaCredito, telaGameOver, telaFim;

        TelaMenu menu;
        TelaAjuda ajuda;
        TelaCreditos creditos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();



            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D botaoVoltar = Content.Load<Texture2D>("botoesAjuda");
            Texture2D imagemMouse = Content.Load<Texture2D>("cursor");
            

            #region Tela Menu
            Texture2D imagem = Content.Load<Texture2D>("titulo");
            Texture2D botao = Content.Load<Texture2D>("botoes");
            Texture2D botaoAjuda = Content.Load<Texture2D>("botoesAjuda");
            Texture2D botaoCreditos = Content.Load<Texture2D>("botoesCreditos");
            Texture2D botaoFim = Content.Load<Texture2D>("botoesFim");
            menu = new TelaMenu(imagem, botao, botaoAjuda, botaoCreditos, botaoFim, spriteBatch);
            menu.setaImnagemMouse(imagemMouse);
            //menu.mensagemMenu = Mensagem.TELA_MENU;

            #endregion

            #region Tela Ajuda
            Texture2D telaAjuda = Content.Load<Texture2D>("ajuda");
            ajuda = new TelaAjuda(telaAjuda, botaoVoltar, spriteBatch, menu);
            ajuda.setaImnagemMouse(imagemMouse);
            //menu.mensagemMenu = Mensagem.TELA_AJUDA;
            #endregion

            #region Tela Creditos
            Texture2D telaCreditos = Content.Load<Texture2D>("creditos");
            creditos = new TelaCreditos(telaCreditos, botaoVoltar, spriteBatch, menu);
            creditos.setaImnagemMouse(imagemMouse);
            #endregion
            //telaGameOver = Content.Load<Texture2D>("gameover");
            //telaFim = Content.Load<Texture2D>("fim");


            #region Jogador
            imagemJog = Content.Load<Texture2D>("esquilo2");
            jogador = new Personagem(imagemJog);
            #endregion

            #region Bola
            imagemBola = Content.Load<Texture2D>("noz");
            bola = new Bola(imagemBola);
            #endregion

            #region Objeto
            CarregaObjetos();
            #endregion

        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (menu.mensagemMenu)
            {
                case Mensagem.TELA_MENU:
                    menu.update(gameTime);
                    break;
                case Mensagem.TELA_AJUDA:
                    ajuda.update(gameTime);
                    break;
                case Mensagem.TELA_CREDITOS:
                    creditos.update(gameTime);
                    break;
                case Mensagem.FIM:
                    this.Exit();
                    break;


            }


            jogador.Update(gameTime);

            retJogador = jogador.GetRetangulo();

            bola.ColisaoJogador(retJogador, imagemJog);

            bola.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (menu.mensagemMenu)
            {
                case Mensagem.TELA_MENU:
                    menu.draw(gameTime, spriteBatch);
                    break;
                case Mensagem.FASE_1:
                    jogador.Draw(spriteBatch);
                    bola.Draw(spriteBatch);

                    foreach (Objeto obj in objetos)
                    {
                        obj.draw(gameTime, spriteBatch);
                    }
                    break;
                case Mensagem.FASE_2:
                    break;
                case Mensagem.FASE_3:
                    break;
                case Mensagem.TELA_AJUDA:
                    ajuda.draw(gameTime, spriteBatch);
                    break;
                case Mensagem.TELA_CREDITOS:
                    creditos.draw(gameTime, spriteBatch);
                    break;
                case Mensagem.TELA_GAMEOVER:
                    break;
                case Mensagem.TELA_FIM:
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CarregaObjetos()
        {
            Random rnd = new Random();

            for (int lin = 0; lin < LIN_OBJETOS; lin++)
            {
                for (int col = 0; col < COL_OBJETOS; col++)
                {

                    int p = 10;
                    if (lin == 0) { p = 30; }
                    if (lin == 1) { p = 20; }

                    int cor = rnd.Next(0, 3);

                    switch (cor)
                    {
                        case 0://Vermelho
                            objetos.Add(new Objeto(Content.Load<Texture2D>("quadVermelho"), new Vector2(objetoBlocoBound.X, objetoBlocoBound.Y), new Vector2(OBJETO_SIZE_X, OBJETO_SIZE_Y), col, lin, p));
                            break;
                        case 1: //Azul
                            objetos.Add(new Objeto(Content.Load<Texture2D>("quadAzul"), new Vector2(objetoBlocoBound.X, objetoBlocoBound.Y), new Vector2(OBJETO_SIZE_X, OBJETO_SIZE_Y), col, lin, p));
                            break;
                        case 2: //Amarelo
                            objetos.Add(new Objeto(Content.Load<Texture2D>("quadAmarelo"), new Vector2(objetoBlocoBound.X, objetoBlocoBound.Y), new Vector2(OBJETO_SIZE_X, OBJETO_SIZE_Y), col, lin, p));
                            break;
                        case 3: //Verde
                            objetos.Add(new Objeto(Content.Load<Texture2D>("quadVerde"), new Vector2(objetoBlocoBound.X, objetoBlocoBound.Y), new Vector2(OBJETO_SIZE_X, OBJETO_SIZE_Y), col, lin, p));
                            break;
                    }

                }
            }
        }
    }
}
