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
    public class StartScene : GameScene
    {
        private const int TITLE_PADDING = 10;
        private MenuComponent menu;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Texture2D title;
        string[] menuItems = { "Start Game", "Help", "About", "How to Play", "Quit" };
        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        public StartScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            Song menuMusic = game.Content.Load<Song>("Sounds/Music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(menuMusic);
            tex = game.Content.Load<Texture2D>("Images/MainMenuBackground");
            menu = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>("Fonts/regularFont"), game.Content.Load<SpriteFont>("Fonts/hilightFont"), menuItems);
            this.Components.Add(menu);
            title = game.Content.Load<Texture2D>("Images/Title");
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

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.Draw(title, new Vector2 (TITLE_PADDING, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
