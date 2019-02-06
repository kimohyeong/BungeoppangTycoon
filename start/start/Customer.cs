using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace start
{
    class Customer
    {
        int wanabe;
        int id;
        int imageNum;
        Vector2 pos;
        float speed;

        public Customer()
        {
            pos = new Vector2(-100, -100);
            speed = 0.5f;
        }

        public void setSpeed(float _speed)
        {
            speed = _speed;
        }

        public int getWanabe()
        {
            return wanabe;
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public int getImageNum()
        {
            return imageNum;
        }

        public void moveCustomer()
        {
            pos.Y -= (float)speed;
        }

        public void initCus(int _wanabe, int _id, Vector2 _pos, int _imageNum)
        {
            wanabe = _wanabe;
            id = _id;
            pos = _pos;
            imageNum = _imageNum;
        }

        public void Remove()
        {

            pos.Y = -100;
            pos.X = -100;
            GameScene.isMove[id] = false;
        }
    }

}