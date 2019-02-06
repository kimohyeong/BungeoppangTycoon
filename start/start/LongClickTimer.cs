using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace start
{
    class LongClickTimer : Timer
    {
        int startTime;
        int term = 400;
        FishBread breads;

        public LongClickTimer(FishBread _breads, int _startTime)
        {
            startTime = _startTime + term;
            breads = _breads;
            breads.Start = 0;

        }
        public int process(int gameTime)
        {
            

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                if (breads.Start != breads.End)
                {
                    breads.State = TEEL_STATE.OJ_IDLE;
                    breads.IsAnimate = false;
                }
                else
                {
                    breads.State = TEEL_STATE.OJ_BASE;
                    breads.IsAnimate = false;
                }

                breads.Start = 0;
                breads.End = 0;
                breads.EndTime = 0;
                breads.IsAnimate = false;
                return (int)RETURN_FLAG.DELETE;
            }

            if (startTime < gameTime)
            {


                if (gameTime % 10 == 0)
                {
                    if (breads.Start == breads.End)
                    {
                        breads.Start = 0;
                    }
                    else
                    {
                        breads.Start++;
                    }
                }

                if (breads.State == TEEL_STATE.OJ_BASEING && breads.Start == breads.End)
                {
                    breads.State = TEEL_STATE.OJ_BASE;
                    breads.Start = 0;
                    breads.End = 0;
                    breads.IsAnimate = false;
                }

            }

            return (int)RETURN_FLAG.NULL;
        }
        
    }
}
