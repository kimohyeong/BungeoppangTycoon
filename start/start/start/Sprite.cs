using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace start
{
    public class Sprite
    {
        private List<Texture2D> m_lstTex = new List<Texture2D>();
        private Point size;
        private int count;

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        public Point Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public Sprite()
        {
            size.X = 0;
            size.Y = 0;
            count = 0;
        }

        public void Load(ContentManager cm, string sName)
        {
            Texture2D tex = cm.Load<Texture2D>(sName);
            m_lstTex.Add(tex);
            count = 1;
            size.X = m_lstTex[0].Width;
            size.Y = m_lstTex[0].Height;
        }

        public void Load(ContentManager cm, string sName, int num)
        {
            string s;
            for (int i = 0; i < num; i++)
            {
                s = sName + i.ToString("D4");
                Texture2D tex = cm.Load<Texture2D>(s);
                m_lstTex.Add(tex);
            }
            count = num;
            size.X = m_lstTex[0].Width;
            size.Y = m_lstTex[0].Height;
        }

        public void Draw(SpriteBatch batch, int i, Vector2 pt, Color c)
        {

            if(i>=0&& i < count)
            {
                batch.Draw(m_lstTex[i], pt, c);
            }
        }
    }
}
