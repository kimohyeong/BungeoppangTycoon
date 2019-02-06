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

using System.Diagnostics;
using System.Threading;

namespace start
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        Scenes currentScene;
        GameScene gScene;
        MenuScene mScene;


        public static SpriteManager spManger;
        public static Player player1;
        public static Texture2D sprite;


        MouseState mouseState;
        MouseState prevmouseState;

        KeyboardState oldState;
        KeyboardState newState;

        private int whatCursur;


        Vector2[] posMouse;

        private int mouseX;
        private int mouseY;

        private bool isHelp;
        private bool count;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            posMouse = new Vector2[3];
            spManger = new SpriteManager();
            player1 = new Player();


            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            isHelp = false;
            count = false;

            gScene = new GameScene(this, graphics);
            mScene = new MenuScene(this, graphics);
            player1.CursorType = (int)CURSOR_TYPE.HAND;

            this.currentScene = Scenes.uninitiate;
            Scene.targetScreen = Scenes.MenuScene;


            posMouse[0] = new Vector2(14, 137);
            posMouse[1] = new Vector2(70, 100);
            posMouse[2] = new Vector2(123, 3);
            Game1.sprite = Content.Load<Texture2D>("hand");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // 이 변수는 후에 화면에 텍스처를 그릴 때 사용하게 됨

            spriteBatch = new SpriteBatch(GraphicsDevice);

            spManger.SetBatch(spriteBatch);


            spManger.AddSprite(TEEL_STATE.OJ_IDLE, Content, "Sprite/teel/teel/teel0000");
            spManger.AddSprite(TEEL_STATE.OJ_BASEING, Content, "Sprite/teel/base/teel", (int)SPRITE_COUNT.SP_BASEING);
            spManger.AddSprite(TEEL_STATE.OJ_BASE, Content, "Sprite/teel/base/teel0009");
            spManger.AddSprite(TEEL_STATE.OJ_PATING, Content, "Sprite/teel/pat/teel", (int)SPRITE_COUNT.SP_PATING);
            spManger.AddSprite(TEEL_STATE.OJ_PAT, Content, "Sprite/teel/pat/teel0002");
            spManger.AddSprite(TEEL_STATE.OJ_REVERSEING, Content, "Sprite/teel/reverse/teel", (int)SPRITE_COUNT.SP_REVERSING);
            spManger.AddSprite(TEEL_STATE.OJ_FINISHED, Content, "Sprite/teel/reverse/teel0016");
            spManger.AddSprite(TEEL_STATE.OJ_FINISHING, Content, "Sprite/teel/finishing/teel", (int)SPRITE_COUNT.SP_FINISHING);
            spManger.AddSprite(TEEL_STATE.OJ_BURNING, Content, "Sprite/teel/burn/teel", (int)SPRITE_COUNT.SP_BURNING);

            // TODO: use this.Content to load your game content here
            // 해석 : 게임 콘텐츠를 여기에서 로딩하세요.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            oldState = newState;
            newState = Keyboard.GetState();

            this.UpdateScreen();
            UpdateInput();

            scene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            scene.Draw(spriteBatch);
            switch (Game1.player1.CursorType)
            {
                case (int)CURSOR_TYPE.KETTLE:
                    whatCursur = 0;
                    break;
                case (int)CURSOR_TYPE.PAT:
                    whatCursur = 1;
                    break;
                case (int)CURSOR_TYPE.HAND:
                    whatCursur = 2;
                    break;
            }
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if (Game1.sprite != null)
            spriteBatch.Draw(Game1.sprite, new Vector2(Mouse.GetState().X - (int)posMouse[whatCursur].X, Mouse.GetState().Y - (int)posMouse[whatCursur].Y), Color.White);

            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void UpdateScreen()
        {
            if (this.currentScene != Scene.targetScreen)
            {
                switch (Scene.targetScreen)
                {
                    case Scenes.GameScene:
                        this.scene = gScene;
                        break;
                    case Scenes.MenuScene:
                        this.scene = mScene;
                        break;
                }
            }

            
        }
        private void UpdateInput()
        {

            if (oldState.IsKeyDown(Keys.Q) && newState.IsKeyUp(Keys.Q))
            {
                // If not down last update, key has just been pressed.
                //if (!oldState.IsKeyDown(Keys.Q)
                player1.CursorType = (int)CURSOR_TYPE.KETTLE;
                sprite = Content.Load<Texture2D>("banjuk");
            }
            if (oldState.IsKeyDown(Keys.W) && newState.IsKeyUp(Keys.W))
            {

                // If not down last update, key has just been pressed.
                //if (!oldState.IsKeyDown(Keys.W)

                player1.CursorType = (int)CURSOR_TYPE.PAT;
                sprite = Content.Load<Texture2D>("pat");
            }
            if (oldState.IsKeyDown(Keys.E) && newState.IsKeyUp(Keys.E))
            {
                // If not down last update, key has just been pressed.
                //if (!oldState.IsKeyDown(Keys.E)
                player1.CursorType = (int)CURSOR_TYPE.HAND;
                sprite = Content.Load<Texture2D>("hand");
            }
        }
    }


}
