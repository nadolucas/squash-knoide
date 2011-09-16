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
    class Objeto
    {
        /// <summary>
        /// Usado para verificar a posição do inimigo, se houve alguma colisão e desenhar
        /// </summary>
        Rectangle objeto;

        /// <summary>
        /// Imagem do inimigo
        /// </summary>
        Texture2D imagemObjeto;

        /// <summary>
        /// Usado para verificar se o inimigo está 'ativo' (vivo ou morto), se houve alguma colisão e desenhar
        /// </summary>
        bool active;
        bool tempoAcabou;

        static int CIMA = 1;
        static int BAIXO = -1;
        static int PARADO = 0;
        
        /// <summary>
        /// Usado para colocar a lista de inimigos para se movimentar para um lado ou para outro
        /// </summary>
        static int ladodemovimento = PARADO;

        /// <summary>
        /// Lista que representa o conjunto de inimigos
        /// </summary>
        public static List<Objeto> list = new List<Objeto>();

        /// <summary>
        /// Construtor que recebe a imagem e a posicao do inimigo na lista
        /// </summary>
        /// <param name="imagemObjeto"></param>
        /// <param name="objeto"></param>
        public Objeto(Texture2D imagemObjeto, Rectangle objeto)
        {
            this.objeto = objeto;
            this.imagemObjeto = imagemObjeto;
            this.active = true;//todos começam ativos
            list.Add(this);//adicionando a lista no momento de criação
        }

        /// <summary>
        /// Mover os objetos da lista para o lado especificado
        /// </summary>
        /// <param name="ladodemovimento"></param>
        void MoverPara(int ladodemovimento)
        {
            this.objeto.Y += ladodemovimento;
        }

        public void Update(GameTime gameTime)
        {
            //variáveis criadas AQUI no update e usadas só aqui para controlar comportamentos

            int ultimovisivel = 0;// guarda o índice do último objeto da lista de inimigos visivel na DIREITA
            bool desceremY = false;// diz se a lista inteira deve se mover para baixo naquele momento

            foreach (Objeto obj in list)
            {
                if (obj.active)
                {
                    obj.MoverPara(CIMA);

                }
            }

            //ultimovisivel = list.IndexOf(this);

            //detecta q o ultimo visivel bateu no canto
          /*  foreach (Objeto obj in list)
            {
                if (list.IndexOf(this)==ultimovisivel)
                {
                    if (obj.objeto.X + obj.objeto.Width > 800)
                    {
                        if (obj.active)
                        {
                            ladodemovimento = BAIXO;
                            obj.active = false;//TESTE REMOVER COLOCAR NAO ATIVO QUANDO RECEBER TIRO
                            
                        }
                    }
                }
            }

            //detecta q o primeiro visivel bateu no canto da esquerda
            foreach (Enemy enemy in list)
            {
                if (enemy.active)
                {
                    if (enemy.colisionEnemy.X < 0)
                    {
                        ladodemovimento = DIREITA;
                        desceremY = true;
                        enemy.active = false;//TESTE REMOVER COLOCAR NAO ATIVO QUANDO RECEBER TIRO
                        REBATIDAS++;
                    }
                }
            }

            if (desceremY)
            {
                foreach (Enemy enemy in list)
                {
                    enemy.colisionEnemy.Y += 10;
                }
            }*/

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //for (int i = 0; i < list.Count; i++)
            foreach (Objeto obj in list)
            {
                if (obj.active)
                spriteBatch.Draw(obj.imagemObjeto, obj.objeto, Color.White);
            }
        }
    }

}
