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


namespace DesireForAcceleration
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EnemyCar : Microsoft.Xna.Framework.DrawableGameComponent
    {
        float timer = 0;
        float resetTimer = 0;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 defaultPosition;
        private Vector2 speed;
        private Color color;
        private Random r = new Random();   

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public EnemyCar(Game game, SpriteBatch spriteBatch, Texture2D tex, Color color, Vector2 position,Vector2 defaultPosition, Vector2 speed)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            //position = new Vector2(Shared.stage.X / 2 - tex.Width / 2, 0);
            this.position = position;
            this.defaultPosition = defaultPosition;
            //speed = new Vector2(0, r.Next(3, 7));
            this.speed = speed;
            this.color = color;
            
        }

        private void startCar()
        {
            int startCar = r.Next(1, 2);
            if (startCar == 1)
            {
                this.Enabled = true;   
            }
            else
            {
                startCar = r.Next(1,2);
            }
        }
        private void Reposition()
        {
            position = defaultPosition;
            this.Enabled = false;
           // startCar();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            position += speed;
            if (position.Y > Shared.stage.Y + tex.Height)
            {    
                timer++;
                resetTimer++;
                if ((timer==1f))
                {
                    timer = 0;
                    
                    Reposition();
                    startCar();
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
