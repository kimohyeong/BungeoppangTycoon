using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace start
{
    class TimerManager
    {
        List<Timer> list;
        public TimerManager()
        {
            list = new List<Timer>();
        }

        public void process(int gameTime)
        {
            for(int i =0; i<list.Count(); i++)
            {
                if((int)RETURN_FLAG.DELETE == list[i].process(gameTime))
                {
                    list.RemoveAt(i);
                }
                
            }
        }

        public void AddTimer(Timer timer)
        {
            list.Add(timer);
        }
    }
}
