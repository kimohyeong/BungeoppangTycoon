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
    class MenuScene : Scene
    {
        double counter = 0;

        Texture2D backgroundTexture;
        Texture2D button1;
        Texture2D button2;
        Texture2D howtoplay;
        Texture2D exit;
        Texture2D count3;
        Texture2D count2;
        Texture2D count1;
        Texture2D gamestart;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle viewportRect;

        SpriteFont font;


        MouseState mouseState;
        MouseState prevmouseState;

        private int mouseX;
        private int mouseY;

        private bool isHelp;
        private bool count;


        public MenuScene(Game game, GraphicsDeviceManager manager)
            : base(game, manager)
        {

            isHelp = false;
            count = false;

            graphics = manager;
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            backgroundTexture = game.Content.Load<Texture2D>("image\\최종배경");

            viewportRect = new Rectangle(0, 0,
                graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            button1 = game.Content.Load<Texture2D>("image\\시작버튼");
            button2 = game.Content.Load<Texture2D>("image\\게임방법버튼");
            howtoplay = game.Content.Load<Texture2D>("image\\게임방법");
            exit = game.Content.Load<Texture2D>("image\\창닫기버튼");
            count3 = game.Content.Load<Texture2D>("image\\3");
            count2 = game.Content.Load<Texture2D>("image\\2");
            count1 = game.Content.Load<Texture2D>("image\\1");
            gamestart = game.Content.Load<Texture2D>("image\\게임시작");

            font = game.Content.Load<SpriteFont>("Fonts\\GameFont");

        }



        public override void Update(GameTime gameTime)
        {

            prevmouseState = mouseState;
            mouseState = Mouse.GetState();

            mouseX = mouseState.X;
            mouseY = mouseState.Y;
            
            //종료버튼 클릭
            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(1120, 30, 84, 90).Contains(mouseX, mouseY))
                {
                    if (isHelp)
                    {
                        isHelp = !isHelp;
                    }
                    else
                    {
                        game.Exit();
                    }
                }
            }

            //시작버튼 클릭
            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(400, 600, 101, 109).Contains(mouseX, mouseY))
                {
                    count = !count;
                    
                }
            }

            //게임방법버튼 클릭
            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(640, 600, 102, 109).Contains(mouseX, mouseY))
                {
                    isHelp = !isHelp;
                }
            }

            if (count)
            {
                counter += gameTime.ElapsedGameTime.TotalSeconds;
                if (counter >= 4)
                {
                    count = !count;
                    Scene.targetScreen = Scenes.GameScene;
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(backgroundTexture, viewportRect, Color.White);
            spriteBatch.Draw(button1, new Vector2(400, 670), Color.White);
            spriteBatch.Draw(button2, new Vector2(640, 670), Color.White);

            if (isHelp)
                spriteBatch.Draw(howtoplay, viewportRect, Color.White);

            if (counter > 0 &&counter <= 1)
            {
                spriteBatch.Draw(count3, viewportRect, Color.White);
            }
            if (counter > 1 && counter <= 2)
            {
                spriteBatch.Draw(count2, viewportRect, Color.White);
            }
            if (counter > 2 && counter <= 3)
            {
                spriteBatch.Draw(count1, viewportRect, Color.White);
            }
            if (counter > 3 && counter <= 4)
            {
                spriteBatch.Draw(gamestart, viewportRect, Color.White);
            }



         

            spriteBatch.Draw(exit, new Vector2(1120, 30), Color.White);



            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
