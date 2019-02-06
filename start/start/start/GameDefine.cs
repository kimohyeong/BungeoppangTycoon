using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace start
{
    public enum TEEL_STATE
    {
        OJ_IDLE,
        OJ_BASEING,
        OJ_BASE,
        OJ_PATING,
        OJ_PAT,
        OJ_REVERSEING,
        OJ_FINISHED,
        OJ_FINISHING,
        OJ_END,
        OJ_BURNING,
    }
    public enum RETURN_FLAG
    {
        NULL,
        DELETE,
    }
    public enum CURSOR_TYPE
    {
        HAND,
        KETTLE,
        PAT,
    }
    public enum SPRITE_COUNT
    {
        SP_BASEING = 7,
        SP_PATING = 3,
        SP_REVERSING = 17,
        SP_BURNING = 16,
        SP_FINISHING = 2,
    }
}
