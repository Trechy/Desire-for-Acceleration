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
    public class ActionScene : GameScene
    {
        private const int SPLOSION_TIME = 24;
        private const int SCORE_INCREMENT = 10;
        private const int SPLOSION_DELAY = 3;
        private const int LANE1 = 150;
        private const int LANE2 = 210;
        private const int LANE3 = 275;
        private const int ROAD_SPEED = -30;
        private int score = 0;
        private SpriteBatch spriteBatch;
        private CopCar copCar;
        private EnemyCar enemyCar1;
        private EnemyCar enemyCar2;
        private EnemyCar enemyCar3;
        private CarCrash carCrash;
        private ScrollingHighway highway;
        private SpriteFont losingFont;
        private SpriteFont scoreFont;
        private SimpleString gameOver;
        private SimpleString scoreString;
        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            SoundEffect explode = game.Content.Load<SoundEffect>("Sounds/Explosion");
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            //Road
            Texture2D tex = game.Content.Load<Texture2D>("Images/road");
            Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 roadPos = new Vector2((Shared.stage.X / 2) + tex.Width / 2 - srcRect.Width, 0);
            highway = new ScrollingHighway(game, spriteBatch, tex, srcRect, roadPos, new Vector2(0, ROAD_SPEED));
            this.Components.Add(highway);
            //CopCar
            copCar = new CopCar(game, spriteBatch, game.Content.Load<Texture2D>("Images/CopCar"));
            this.Components.Add(copCar);
            //Collison Explosion
            Texture2D splosion = game.Content.Load<Texture2D>("Images/explosion");
            Vector2 splosionPos = Vector2.Zero;

            carCrash = new CarCrash(game, spriteBatch, splosion, splosionPos, SPLOSION_DELAY);
            this.Components.Add(carCrash);

            Random r = new Random();

            Vector2 enemy1pos = new Vector2(LANE1, 0-tex.Height);
            Vector2 enemy2pos = new Vector2(LANE2, 0-tex.Height);
            Vector2 enemy3pos = new Vector2(LANE3, 0-tex.Height);
            
            Vector2 enemy1speed = new Vector2(0, r.Next(7,12));
            Vector2 enemy2speed = new Vector2(0, r.Next(7,12));
            Vector2 enemy3speed = new Vector2(0, r.Next(7,10));

            while (enemy1speed == enemy2speed || enemy1speed == enemy3speed)
            {
                enemy1speed = new Vector2(0, r.Next(6, 13));
            }
            while (enemy2speed == enemy1speed || enemy2speed == enemy3speed)
            {
                enemy2speed = new Vector2(0, r.Next(6, 13));
            }
            while (enemy3speed == enemy1speed || enemy3speed == enemy2speed)
            {
                enemy3speed += new Vector2(0, 7);
            }

            enemyCar1 = new EnemyCar(game, spriteBatch, game.Content.Load<Texture2D>("Images/EnemyCar"), Color.Red, enemy1pos,enemy1pos,enemy1speed);
            enemyCar2 = new EnemyCar(game, spriteBatch, game.Content.Load<Texture2D>("Images/EnemyCar"), Color.Green,enemy2pos,enemy2pos,enemy2speed);
            enemyCar3 = new EnemyCar(game, spriteBatch, game.Content.Load<Texture2D>("Images/EnemyCar"), Color.Blue, enemy3pos,enemy3pos,enemy3speed);

            enemyCar1.Position = enemy1pos;
            enemyCar2.Position = enemy2pos;
            enemyCar3.Position = enemy3pos;

            CollisonManager cm = new CollisonManager(game,copCar,enemyCar1,enemyCar2,enemyCar3, explode);

            scoreFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            string scoreMessage = "Score:\n" + score;
            Vector2 scoreDim = scoreFont.MeasureString(scoreMessage);
            Vector2 fontPosScore = new Vector2(Shared.stage.X - scoreDim.X, Shared.stage.Y - scoreDim.Y);
            scoreString = new SimpleString(game, spriteBatch, scoreFont, fontPosScore, scoreMessage, Color.White);

            this.Components.Add(cm);
            this.Components.Add(enemyCar1);
            this.Components.Add(enemyCar2);
            this.Components.Add(enemyCar3);
            this.Components.Add(scoreString);

            losingFont = game.Content.Load<SpriteFont>("Fonts/losingFont");
            string losingMessage = "Your Life Has Ended\nPress Esc to Return\nto the Main Menu";
            Vector2 losingDim = losingFont.MeasureString(losingMessage);
            Vector2 fontPosLosing = new Vector2(Shared.stage.X / 4, Shared.stage.Y / 2);
            gameOver = new SimpleString(game, spriteBatch, losingFont, fontPosLosing, losingMessage, Color.White);
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
            Rectangle carRect = copCar.getBounds();
            Rectangle enemy1Rect = enemyCar1.getBounds();
            Rectangle enemy2Rect = enemyCar2.getBounds();
            Rectangle enemy3Rect = enemyCar3.getBounds();
           
            if (carRect.Intersects(enemy1Rect) || carRect.Intersects(enemy2Rect) || carRect.Intersects(enemy3Rect))
            {
                carCrash.Position = new Vector2(copCar.Position.X, copCar.Position.Y);
                if (carCrash.Counter <= SPLOSION_TIME)
                {
                    carCrash.Enabled = true;
                    carCrash.Visible = true;
                }
                else
                {
                    carCrash.Enabled = false;
                    carCrash.Visible = false;
                }
                enemyCar1.Enabled = false;
                copCar.Enabled = false;
                enemyCar2.Enabled = false;
                enemyCar3.Enabled = false;
                copCar.Visible = false;
                highway.Enabled= false;
                if (carRect.Intersects(enemy1Rect))
                {
                    enemyCar1.Visible = false;
                }
                if (carRect.Intersects(enemy2Rect))
                {
                    enemyCar2.Visible = false;
                }
                if(carRect.Intersects(enemy3Rect))
                {
                    enemyCar3.Visible = false;
                }
                this.Components.Add(gameOver);
            }
            else
            {              
                score += SCORE_INCREMENT;
                string scoreMessage = "Score:\n" + score;
                scoreString.Message = scoreMessage;
                Vector2 scoreDim = scoreFont.MeasureString(scoreMessage);
                Vector2 fontPosScore = new Vector2(Shared.stage.X - scoreDim.X, Shared.stage.Y - scoreDim.Y);
                scoreString.Position = fontPosScore;
            }
            
                 
            
            base.Update(gameTime);
        }
    }
}
