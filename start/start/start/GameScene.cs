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
    class GameScene : Scene
    {

        GraphicsDeviceManager graphics;
        Vector2[] posList;
        Vector2 teelPos = Vector2.Zero;

        FishBread[] breads;
       
        SpriteBatch spriteBatch;

        KeyboardState oldState;
        KeyboardState newState;

        MouseState mouseState;
        MouseState prevmouseState;

        Texture2D bg = null;
      
        Random random;

        private int mouseX;
        private int mouseY;

        

        private int termClickTime;

        TimerManager timerManager;
        LongClickTimer longClickTimer;


        public GameScene(Game game, GraphicsDeviceManager manager)
            : base(game, manager)
        {

            graphics = manager;
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            posList = new Vector2[9];
            
            random = new Random();
            termClickTime = 0;
            timerManager = new TimerManager();

            breads = new FishBread[9];

            // TODO: Add your initialization logic here

            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                breads[i] = new FishBread();
                if (i % 3 == 0)
                {
                    k++;
                }

                teelPos = new Vector2((i % 3) * 200 + 300, 200 * k - 100);
                posList[i] = teelPos;
            }


            oldState = Keyboard.GetState();

            //spManger.SetBatch(spriteBatch);
            

            //spManger.AddSprite(TEEL_STATE.OJ_IDLE, game.Content, "Sprite/teel/teel/teel0000");
            //spManger.AddSprite(TEEL_STATE.OJ_BASEING, game.Content, "Sprite/teel/base/teel", (int)SPRITE_COUNT.SP_BASEING);
            //spManger.AddSprite(TEEL_STATE.OJ_BASE, game.Content, "Sprite/teel/base/teel0009");
            //spManger.AddSprite(TEEL_STATE.OJ_PATING, game.Content, "Sprite/teel/pat/teel", (int)SPRITE_COUNT.SP_PATING);
            //spManger.AddSprite(TEEL_STATE.OJ_PAT, game.Content, "Sprite/teel/pat/teel0002");
            //spManger.AddSprite(TEEL_STATE.OJ_REVERSEING, game.Content, "Sprite/teel/reverse/teel", (int)SPRITE_COUNT.SP_REVERSING);
            //spManger.AddSprite(TEEL_STATE.OJ_FINISHED, game.Content, "Sprite/teel/reverse/teel0016");
            //spManger.AddSprite(TEEL_STATE.OJ_FINISHING, game.Content, "Sprite/teel/finishing/teel", (int)SPRITE_COUNT.SP_FINISHING);
            //spManger.AddSprite(TEEL_STATE.OJ_BURNING, game.Content, "Sprite/teel/burn/teel", (int)SPRITE_COUNT.SP_BURNING);
            bg = new Texture2D(graphics.GraphicsDevice, 100, 100);
            bg = game.Content.Load<Texture2D>("background");
            //Game1.sprite = game.Content.Load<Texture2D>("hand");


        }

        public override void Update(GameTime gameTime)
        {
            prevmouseState = mouseState;
            mouseState = Mouse.GetState();
            

            mouseX = mouseState.X;
            mouseY = mouseState.Y;


            ClickEvent(gameTime);
            TeelAnimationCheck(gameTime);


            timerManager.process((int)gameTime.TotalGameTime.TotalMilliseconds);

            //UpdateInput();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(bg, new Vector2(0, 0), new Color(255, 255, 255, 255));//background
            
            for (int i = 0; i < 9; i++)
            {
                Game1.spManger.Draw(breads[i].State, breads[i].Start, posList[i], Color.White);
                
            }


            spriteBatch.End();





            base.Draw(spriteBatch);
        }

        private void ClickEvent(GameTime gameTime)
        {
            switch (Game1.player1.CursorType)
            {
                case (int)CURSOR_TYPE.HAND:
                case (int)CURSOR_TYPE.PAT:

                    ShortClick(gameTime);

                    break;
                case (int)CURSOR_TYPE.KETTLE:

                    if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (new Rectangle((int)posList[i].X, (int)posList[i].Y, 200, 200).Contains(mouseX, mouseY))
                            {
                                if (breads[i].State == TEEL_STATE.OJ_IDLE)
                                {
                                    breads[i].End = CalcCount(TEEL_STATE.OJ_BASEING);
                                    breads[i].State = TEEL_STATE.OJ_BASEING;
                                    breads[i].IsAnimate = true;
                                    timerManager.AddTimer(new LongClickTimer(breads[i], (int)gameTime.TotalGameTime.TotalMilliseconds));
                                }
                            }
                        }

                    }
                    break;
            }
        }

        private void ShortClick(GameTime gametime)
        {
            for (int i = 0; i < 9; i++)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
                {
                    if (new Rectangle((int)posList[i].X, (int)posList[i].Y, 200, 200).Contains(mouseX, mouseY))
                    {
                        if (!breads[i].IsAnimate && breads[i].State != TEEL_STATE.OJ_IDLE)
                        {
                            if ((Game1.player1.CursorType == (int)CURSOR_TYPE.PAT && breads[i].State == TEEL_STATE.OJ_BASE)
                                  || (Game1.player1.CursorType == (int)CURSOR_TYPE.HAND && (breads[i].State == TEEL_STATE.OJ_PAT || (breads[i].State == TEEL_STATE.OJ_FINISHED))))
                            {
                                breads[i].State++;
                                breads[i].Start = 0;
                                breads[i].End = CalcCount(breads[i].State);
                                if (breads[i].End != 0)
                                {
                                    breads[i].IsAnimate = true;
                                }
                            }
                        }
                    }
                }
            }
        }




        private void TeelAnimationCheck(GameTime gameTime)
        {
            int time = (int)gameTime.TotalGameTime.TotalMilliseconds;

            for (int i = 0; i < 9; i++)
            {
                if (breads[i].EndTime != 0 && breads[i].EndTime < time / 1000)
                {
                    breads[i].State = TEEL_STATE.OJ_BURNING;
                    breads[i].Start = 0;
                    breads[i].End = CalcCount(TEEL_STATE.OJ_BURNING);
                    breads[i].EndTime = 0;
                    breads[i].IsAnimate = true;
                }


                // chage anmation time
                if ((Game1.player1.CursorType == (int)CURSOR_TYPE.KETTLE && time % 10 == 0)
                    || (Game1.player1.CursorType == (int)CURSOR_TYPE.PAT && time % 10 == 0)
                        || (Game1.player1.CursorType == (int)CURSOR_TYPE.HAND && time % 2 == 0))
                {
                    if (breads[i].Start == breads[i].End)
                    {
                        breads[i].Start = 0;
                    }
                    else
                    {
                        breads[i].Start++;
                    }
                }

                if (breads[i].IsAnimate && breads[i].Start == breads[i].End)
                {
                    if (breads[i].State == TEEL_STATE.OJ_FINISHING)
                    {
                        breads[i].State = TEEL_STATE.OJ_IDLE;
                        breads[i].EndTime = 0;
                        Game1.player1.BreadCount++;
                    }
                    else if (breads[i].State == TEEL_STATE.OJ_BURNING)
                    {
                        breads[i].State = TEEL_STATE.OJ_IDLE;
                    }
                    else
                    {
                        breads[i].State++;
                    }

                    if (breads[i].State == TEEL_STATE.OJ_PAT)
                    {
                        breads[i].EndTime = time / 1000 + random.Next(3, 7);
                    }

                    breads[i].Start = 0;
                    breads[i].End = 0;
                    breads[i].IsAnimate = false;
                }
            }

        }

        private int CalcCount(TEEL_STATE state)
        {
            switch (state)
            {
                case TEEL_STATE.OJ_BASEING:
                    return (int)SPRITE_COUNT.SP_BASEING - 1;
                case TEEL_STATE.OJ_PATING:
                    return (int)SPRITE_COUNT.SP_PATING - 1;
                case TEEL_STATE.OJ_REVERSEING:
                    return (int)SPRITE_COUNT.SP_REVERSING - 1;
                case TEEL_STATE.OJ_FINISHING:
                    return (int)SPRITE_COUNT.SP_FINISHING - 1;
                case TEEL_STATE.OJ_BURNING:
                    return (int)SPRITE_COUNT.SP_BURNING - 1;
            }
            return 0;
        }

        private void UpdateInput()
        {

            if (oldState.IsKeyDown(Keys.Q) && newState.IsKeyUp(Keys.Q))
            {
                // If not down last update, key has just been pressed.
                //if (!oldState.IsKeyDown(Keys.Q)
                Game1.player1.CursorType = (int)CURSOR_TYPE.KETTLE;
                Game1.sprite = game.Content.Load<Texture2D>("banjuk");
            }
            if (oldState.IsKeyDown(Keys.W) && newState.IsKeyUp(Keys.W))
            {

                // If not down last update, key has just been pressed.
                //if (!oldState.IsKeyDown(Keys.W)

                Game1.player1.CursorType = (int)CURSOR_TYPE.PAT;
                Game1.sprite = game.Content.Load<Texture2D>("pat");
            }
            if (oldState.IsKeyDown(Keys.E) && newState.IsKeyUp(Keys.E))
            {
                // If not down last update, key has just been pressed.
                //if (!oldState.IsKeyDown(Keys.E)
                Game1.player1.CursorType = (int)CURSOR_TYPE.HAND;
                Game1.sprite = game.Content.Load<Texture2D>("hand");
            }
        }
    }
}
