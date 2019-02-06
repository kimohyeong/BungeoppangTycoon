using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace start
{
    abstract class Scene
    {
        protected Game game;
        static public Scenes targetScreen;


        public Scene(Game game, GraphicsDeviceManager manager)
        {
            this.game = game;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    public enum Scenes
    {
        uninitiate = -1, MenuScene, GameScene, OptionScene 
    }
}
