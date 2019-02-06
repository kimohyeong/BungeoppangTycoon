using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
namespace start
{
   
    class FishBread
    {
        TEEL_STATE teel_type;

        private TEEL_STATE state;
        private bool isAnimate;
        private int start;
        private int end;
        private int endTime;


        public FishBread()
        {
            isAnimate = false;
            state = TEEL_STATE.OJ_IDLE;
            start = 0;
            endTime = 0;
        }

        public TEEL_STATE State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        public int EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        public bool IsAnimate
        {
            get
            {
                return isAnimate;
            }
            set
            {
                isAnimate = value;
            }
        }
    }
}
