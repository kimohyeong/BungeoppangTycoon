using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Diagnostics;

namespace start
{
    class GameScene : Scene
    {
        int score = 0; // 붕어빵 개수 카운트
        string strscore = "";
        int money = 0;
        string strmoney = "";
        private SpriteFont font;
        private SpriteFont rankingfont;
        private string text;
        private float time;
        int end_checking = 0;
        private Vector2 position;
        private bool started;
        private bool paused;
        private bool finished;

        Vector2 scoreDrawPoint = new Vector2(500, 500);

        GraphicsDeviceManager graphics;
        public static Player player = new start.Player();
        Vector2[] posList;
        Vector2 teelPos = Vector2.Zero;

        FishBread[] breads;
        Customer[] customers;
        public static bool[] isMove;

        FileStream fw;
        int check = 0;
        StreamWriter sw;

        SpriteBatch spriteBatch;

        KeyboardState oldState;
        KeyboardState newState;

        MouseState mouseState;
        MouseState prevmouseState;

        Texture2D bg = null;

        Random random;
        int randomNum;

        private int mouseX;
        private int mouseY;

        TimerManager timerManager;

        int[] scoremoney = new int[6];
        int number = 5;
        int i = 0;
        public static bool isMode = false;

        public GameScene(Game game, GraphicsDeviceManager manager)
            : base(game, manager)
        {

            time = 70;
            started = true;
            paused = false;
            finished = false;
            text = "";


            graphics = manager;
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            posList = new Vector2[9];

            random = new Random();
            timerManager = new TimerManager();

            breads = new FishBread[9];

            customers = new Customer[5];
            for (int i = 0; i < 5; i++)
            {
                customers[i] = new Customer();
            }
            isMove = new bool[5];
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

            bg = new Texture2D(graphics.GraphicsDevice, 100, 100);
            bg = game.Content.Load<Texture2D>("market");
            font = game.Content.Load<SpriteFont>("Fonts\\GameFont");
            rankingfont = game.Content.Load<SpriteFont>("Rankingfont");

        }

        public override void Update(GameTime gameTime)
        {
            prevmouseState = mouseState;
            mouseState = Mouse.GetState();


            mouseX = mouseState.X;
            mouseY = mouseState.Y;


            ClickEvent(gameTime);
            if (end_checking == 1)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
                {
                    Scene.targetScreen = Scenes.MenuScene;
                }
            }
            TeelAnimationCheck(gameTime);
            CustomerEvent(gameTime);

            timerManager.process((int)gameTime.TotalGameTime.TotalMilliseconds);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (started)
            {
                if (!paused)
                {
                    if (isMode)
                    {
                        if (time > 0)
                            time -= deltaTime;
                        else
                        {
                            finished = true;
                        }
                    }

                }
            }

            if (finished)
            {
                text = "gameover";
            }
            else
            {
                if (isMode)
                {

                    text = ((int)time).ToString();

                }
                else
                {
                    text = "INFINITY";

                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            strscore = player.BreadCount.ToString();
            strmoney = player.Point.ToString();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(bg, new Vector2(0, 0), new Color(255, 255, 255, 255));//background
            if (end_checking == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    Game1.spManger.Draw(breads[i].State, breads[i].Start, posList[i], Color.White);
                }

                for (int i = 0; i < 5; i++)
                {
                    if (isMove[i] == true)
                        Game1.spManger.Draw(TEEL_STATE.OJ_CUSTOMER, customers[i].getImageNum(), customers[i].getPos(), Color.White);
                }
                for (int i = 0; i < 2; i++)
                {
                    Game1.spManger.Draw(TEEL_STATE.OJ_WHITE, 0, new Vector2(1070, 152 + 71 * i), Color.White);
                }
                Game1.spManger.Draw(TEEL_STATE.OJ_WHITE, 0, new Vector2(1070, 283), Color.White);
                if (time <= 10)
                {
                    Game1.spManger.Draw(TEEL_STATE.OJ_WATCH, 0, new Vector2(990, 100), Color.White);
                }
                spriteBatch.DrawString(font, text, new Vector2(1070, 162), Color.Black);
                spriteBatch.DrawString(font, strmoney, new Vector2(1070, 222), Color.Black);
                spriteBatch.DrawString(font, strscore, new Vector2(1070, 282), Color.Black);

            }
            else if (end_checking == 1)
            {
                int vk = 0;
                FileStream fr1 = new FileStream("data.txt", FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr1 = new StreamReader(fr1);
                sr1.BaseStream.Seek(0, SeekOrigin.Begin); ;
                while (sr1.Peek() > -1)
                {
                    vk++;
                    string r = sr1.ReadLine();
                    spriteBatch.DrawString(rankingfont, r, new Vector2(550, 50 + vk * 120), Color.Black);

                }
                sr1.Close();
                fr1.Close();
            }
            if (finished && check == 0)
            {
                end_checking = 1;
                i = 0;
                FileStream fr = new FileStream("data.txt", FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fr);
                while (!sr.EndOfStream && i < number)
                {
                    scoremoney[i] = int.Parse(sr.ReadLine());
                    i++;
                }
                sr.Close();
                fr.Close();
                int k = i;
                scoremoney[k] = Convert.ToInt32(strmoney);
                for (i = 0; i < k; i++)
                {
                    if (scoremoney[k] >= scoremoney[i])
                    {
                        break;
                    }
                }
                int tempscore = scoremoney[k];
                for (int j = k; j > i; j--)
                {
                    scoremoney[j] = scoremoney[j - 1];
                }
                scoremoney[i] = tempscore;
                check = 1;
                fw = new FileStream("data.txt", FileMode.Create);
                sw = new StreamWriter(fw);
                if (k >= number)
                {
                    for (i = 0; i < number; i++)
                    {
                        sw.WriteLine(scoremoney[i]);
                    }
                }
                else
                {
                    for (i = 0; i <= k; i++)
                    {
                        sw.WriteLine(scoremoney[i]);
                    }
                }
                sw.Close();
                fw.Close();

                bg = new Texture2D(graphics.GraphicsDevice, 100, 100);
                bg = game.Content.Load<Texture2D>("ranking");

            }

            base.Draw(spriteBatch);
            spriteBatch.End();

        }

        private void CustomerEvent(GameTime gameTime)
        {

            // Customer Variable control

            if (random.Next(1, 300) == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (isMove[i] == false)
                    {
                        isMove[i] = true;
                        randomNum = random.Next(1, 3);

                        customers[i].initCus(randomNum, i, new Vector2(100, 500), random.Next(0, 3) * 3 + randomNum - 1);
                        timerManager.AddTimer(new CustomerTimer(customers[i]));
                        return;
                    }

                }

            }
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
            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(1140, 0, 60, 60).Contains(mouseX, mouseY))
                {
                    Scene.targetScreen = Scenes.OptionScene;
                }

            }

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

                for (int j = 0; j < 5; j++)
                {

                    if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
                    {
                        if (customers[j] != null && new Rectangle((int)customers[j].getPos().X, (int)customers[j].getPos().Y, 100, 142).Contains(mouseX, mouseY))
                        {
                            if (customers[j].getWanabe() <= player.BreadCount)
                            {

                                customers[j].Remove();

                                //player bread count and point calc
                                if (time <= 10)
                                {
                                    player.BreadCount -= customers[j].getWanabe();
                                    player.Point += customers[j].getWanabe() * 500 * 2;
                                    money = player.Point;
                                    score = player.BreadCount;

                                }
                                else
                                {
                                    player.BreadCount -= customers[j].getWanabe();
                                    player.Point += customers[j].getWanabe() * 500;
                                    money = player.Point;
                                    score = player.BreadCount;
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
                        player.BreadCount++;
                        score = player.BreadCount;

                        //   spriteBatch.DrawString
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
                        breads[i].EndTime = time / 1000 + random.Next(5, 10);
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
