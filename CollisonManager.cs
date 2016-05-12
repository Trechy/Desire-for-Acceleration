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
    public class CollisonManager : Microsoft.Xna.Framework.GameComponent
    {
        private CopCar copCar;
        private EnemyCar enemyCar1;
        private EnemyCar enemyCar2;
        private EnemyCar enemyCar3;
        private SoundEffect explode;
        private int soundCounter = 0;

        public CollisonManager(Game game, CopCar copCar, EnemyCar enemyCar1, EnemyCar enemyCar2, EnemyCar enemyCar3, SoundEffect explode)
            : base(game)
        {
            // TODO: Construct any child components here
            this.copCar = copCar;
            this.enemyCar1 = enemyCar1;
            this.enemyCar2 = enemyCar2;
            this.enemyCar3 = enemyCar3;
            this.explode = explode;
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
            
            if (carRect.Intersects(enemy1Rect)|| carRect.Intersects(enemy2Rect) || carRect.Intersects(enemy3Rect))
            {
                soundCounter++;
                if (soundCounter <= 4)
                {
                   explode.Play(); 
                }
                enemyCar1.Enabled = false;
                copCar.Enabled = false;
                enemyCar2.Enabled = false;
                enemyCar3.Enabled = false;
            }

            base.Update(gameTime);
        }
    }
}
