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
    public class ScrollingHighway : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private const float SCROLL_PAD = 50f;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 position1, position2;
        private bool swap_road = false;
        private bool highwayEnabled = true;
        public bool HighwayEnabled
        {
            get { return highwayEnabled; }
            set { highwayEnabled = value; }
        }
        public ScrollingHighway(Game game, SpriteBatch spriteBatch, Texture2D tex, Rectangle srcRect, Vector2 position, Vector2 speed)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position = position;
            this.speed = speed;
            this.position1 = position;
            this.position2 = new Vector2(position1.X, Shared.stage.Y - tex.Height);
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
            if (swap_road)
            {
                position1.Y -= speed.Y;
                position2.Y = position1.Y - tex.Height;
                if (position1.Y > Shared.stage.Y)
                {
                    swap_road = !swap_road;
                }
            }
            else
            {
                position2.Y -= speed.Y;
                position1.Y = position2.Y - tex.Height;
                if (position2.Y > Shared.stage.Y)
                {
                    swap_road = !swap_road;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
