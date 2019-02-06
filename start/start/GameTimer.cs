
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

using System.IO;
namespace start
{
    class GameTimer : GameComponent
    {
        private SpriteFont font;
        private string text;
        private float time;
        private Vector2 position;
        private bool started;
        private bool paused;
        private bool finished;

        FileStream fw;
        StreamWriter sw;
        public GameTimer(Game game, float startTime)
            : base(game)
        {
            time = startTime;
            started = false;
            paused = false;
            finished = false;
            Text = "";
        }

        #region Properties
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool Started
        {
            get { return started; }
            set { started = value; }
        }
        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }
        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (started)
            {
                if (!paused)
                {
                    if (time > 0)
                        time -= deltaTime;
                    else
                        finished = true;
                }
            }

            if (finished)
            {
                text = "gameover";
            }
            else
                text = ((int)time).ToString();
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color.Black);
        }
    }
}