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
    public class CopCar : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private const int BOTTOM_PADDING = 3;
        private const float LEFT_SHOULDER = 0.25f;
        private const float RIGHT_SHOULDER = 0.75f;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public CopCar(Game game, SpriteBatch spriteBatch, Texture2D tex)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = new Vector2(Shared.stage.X / 2 - tex.Width / 2, Shared.stage.Y - tex.Height * BOTTOM_PADDING);
            speed = new Vector2(4, 2);
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
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W))
            {
                position.Y -= speed.Y;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
            }
            if (ks.IsKeyDown(Keys.S))
            {
                position.Y += speed.Y;
                if (position.Y > Shared.stage.Y - tex.Height)
                {
                    position.Y = Shared.stage.Y - tex.Height;
                }
            }
            if (ks.IsKeyDown(Keys.D))
            {
                position.X += speed.X;
                if (position.X > (Shared.stage.X * RIGHT_SHOULDER) - tex.Width)
                {
                    position.X = (Shared.stage.X * RIGHT_SHOULDER) - tex.Width;
                }
            }
            if (ks.IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
                if (position.X < (Shared.stage.X * LEFT_SHOULDER))
                {
                    position.X = (Shared.stage.X * LEFT_SHOULDER);
                }
                base.Update(gameTime);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
