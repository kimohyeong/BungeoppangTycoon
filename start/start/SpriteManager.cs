using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace start
{
    public class SpriteManager
    {
        Sprite[] list_sp = new Sprite[200];
        SpriteBatch batch;
        
        public SpriteManager() { }

        public void SetBatch(SpriteBatch _batch)
        {
            batch = _batch;
            
        }

        public void AddSprite(TEEL_STATE TEEL_STATE, ContentManager cm, string s, int cnt)
        {
            list_sp[(int)TEEL_STATE] = new Sprite();
            list_sp[(int)TEEL_STATE].Load(cm, s, cnt);
        }

        public void AddSprite(TEEL_STATE TEEL_STATE, ContentManager cm, string s)
        {
            list_sp[(int)TEEL_STATE] = new Sprite();
            list_sp[(int)TEEL_STATE].Load(cm, s);
        }

        public void Draw(TEEL_STATE TEEL_STATE, int cf, Vector2 pt, Color c)
        {
            GetSprite(TEEL_STATE).Draw(batch, cf, pt, c);
        }

        public int WIdth(TEEL_STATE TEEL_STATE)
        {
            return GetSprite(TEEL_STATE).Size.X;
        }

        public int Height(TEEL_STATE TEEL_STATE)
        {
            return GetSprite(TEEL_STATE).Size.Y;
        }

        public int Count(TEEL_STATE TEEL_STATE)
        {
            return GetSprite(TEEL_STATE).Count;
        }

        public Sprite GetSprite(TEEL_STATE TEEL_STATE)
        {
            return list_sp[(int)TEEL_STATE];
        }

    }
}
