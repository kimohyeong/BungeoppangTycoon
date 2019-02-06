using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace start
{
    public class Player
    {
        private int life;
        private int breadCount;
        private int point;
        private int cursorType;

        public int Life
        {
            get
            {
                return life;
            }
            set
            {
                life = value;
            }
        }

        public int BreadCount
        {
            get
            {
                return breadCount;
            }
            set
            {
                breadCount = value;
            }
        }

        public int Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
            }
        }
        public int CursorType
        {
            get
            {
                return cursorType;
            }
            set
            {
                cursorType = value;
            }
        }

        public Player()
        {
            life = 5;
            breadCount = 0;
            point = 0;
        }
    }
}
