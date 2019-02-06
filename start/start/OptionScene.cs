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

using MySql.Data.MySqlClient;

namespace start
{
    class OptionScene : Scene
    {
        string strscore = "";
        int money = 0;
        string strmoney = "";

        int[] scoremoney = new int[6];
        int number = 5;
        int i = 0;

        MouseState mouseState;
        MouseState prevmouseState;

        TextboxHandler textboxHandler;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D bg = null;

        private int mouseX;
        private int mouseY;

        FileStream fw;
        int check = 0;
        StreamWriter sw;

        SpriteFont font;
        SpriteFont rankingfont;

        String textboxtext1 = "";

        String textboxtext2 = "";
        List<String> list = new List<String>();

        bool isRank = false;
        bool isFIrst = false;
        public OptionScene(Game game, GraphicsDeviceManager manager)
            : base(game, manager)
        {

            this.graphics = manager;
            this.spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            bg = new Texture2D(graphics.GraphicsDevice, 100, 100);
            bg = game.Content.Load<Texture2D>("option");




            rankingfont = game.Content.Load<SpriteFont>("Rankingfont");



            Texture2D texture = game.Content.Load<Texture2D>("selection");
            List<Textbox> textboxs = new List<Textbox>();

            font = game.Content.Load<SpriteFont>("font");

            Textbox textbox = new Textbox(new Rectangle(450, 120, 150, 70), font, "", 5, null, null, texture);
            textboxs.Add(textbox);
            textbox = new Textbox(new Rectangle(700, 120, 150, 70), font, "", 5, null, null, texture);
            textboxs.Add(textbox);
            textboxHandler = new TextboxHandler(textboxs, Vector2.Zero);
        }

        public override void Update(GameTime gameTime)
        {
            prevmouseState = mouseState;
            mouseState = Mouse.GetState();

            mouseX = mouseState.X;
            mouseY = mouseState.Y;


            if (isRank)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
                {
                    Scene.targetScreen = Scenes.MenuScene;
                    isRank = false;
                    isFIrst = false;

                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(1140, 0, 60, 60).Contains(mouseX, mouseY))
                {
                    Scene.targetScreen = Scenes.GameScene;
                }

            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(400, 200, 400, 80).Contains(mouseX, mouseY))
                {
                    if (!textboxtext1.Equals(""))
                    {

                        String insert = "select * from bread where id ='" + textboxtext1 + "'";

                        MySqlCommand cmd = new MySqlCommand(insert, Game1.conn);
                        cmd.ExecuteNonQuery();
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (!rdr.Read())
                        {

                            if (!textboxtext1.Equals(""))
                            {
                                rdr.Close();
                                insert = "insert  into bread(id, bread, point, pw) values('" + textboxtext1 + "', " + GameScene.player.BreadCount + "," + GameScene.player.Point + ",'" + textboxtext2 + "');";

                                cmd = new MySqlCommand(insert, Game1.conn);
                                cmd.ExecuteNonQuery();

                                GameScene.player.BreadCount = 0;
                                GameScene.player.Point = 0;
                            }
                        }
                        if (!rdr.IsClosed)
                            rdr.Close();
                    }


                    isRank = true;

                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(400, 360, 400, 80).Contains(mouseX, mouseY))
                {

                    Scene.targetScreen = Scenes.MenuScene;

                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(400, 480, 400, 80).Contains(mouseX, mouseY))
                {

                    Scene.targetScreen = Scenes.GameScene;

                }
            }

            textboxHandler.Update();

            textboxtext1 = textboxHandler.getTextbox(0).getText();
            textboxtext2 = textboxHandler.getTextbox(1).getText();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(bg, new Vector2(0, 0), new Color(255, 255, 255, 255));//background

            if (isRank)
            {



                if (GameScene.isMode)
                {
                    TimeAtackRank();
                    for (int i = 0; i < 5; i++)
                        spriteBatch.DrawString(rankingfont, list[i], new Vector2(450, 50 + (i + 1) * 120), Color.Black);

                }
                else
                {

                    if (!isFIrst)
                    {
                        String order = "select point, id from bread order by point DESC;";
                        MySqlCommand cm = new MySqlCommand(order, Game1.conn);
                        cm.ExecuteNonQuery();
                        MySqlDataReader rd = cm.ExecuteReader();
                        String sentence;
                        int vk = 0;
                        bg = new Texture2D(graphics.GraphicsDevice, 100, 100);
                        bg = game.Content.Load<Texture2D>("ranking");

                        while (vk != 5)
                        {
                            vk++;
                            rd.Read();
                            sentence = rd["id"].ToString() + " : " + rd["point"].ToString();

                            list.Add(sentence);
                        }

                        rd.Close();

                    }
                    for (int i = 0; i < 5; i++)
                        spriteBatch.DrawString(rankingfont, list[i], new Vector2(450, 50 + (i + 1) * 120), Color.Black);

                    isFIrst = true;

                }

            }
            if (!isRank)
                textboxHandler.Draw(spriteBatch);

            spriteBatch.End();

        }

        public void TimeAtackRank()
        {

            strscore = GameScene.player.BreadCount.ToString();
            strmoney = GameScene.player.Point.ToString();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            int end_checking = 0;
            if (end_checking == 0)
            {

                {
                    int vk = 0;
                    FileStream fr1 = new FileStream("data.txt", FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader sr1 = new StreamReader(fr1);
                    sr1.BaseStream.Seek(0, SeekOrigin.Begin); ;
                    while (sr1.Peek() > -1)
                    {
                        vk++;
                        string r = sr1.ReadLine();
                        list.Add(r);

                    }
                    sr1.Close();
                    fr1.Close();
                }

                end_checking = 1;
                int i = 0;
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
            spriteBatch.End();
        }
    }
}