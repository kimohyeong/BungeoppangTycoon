using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace start
{
    class CustomerTimer : Timer
    {
        Customer customer;
        int returnValue;

        public CustomerTimer(Customer _customer)
        {
            customer = _customer;
            returnValue = (int)RETURN_FLAG.NULL;
        }

        public int process(int gameTime)
        {
            if (customer.getPos().X == -100)
                return returnValue;

            customer.moveCustomer();
            if (customer.getPos().Y < 100)
                removeCustomer();

            return returnValue;
        }
        public void removeCustomer()
        {

            customer.Remove();
            returnValue = (int)RETURN_FLAG.DELETE;
            if (GameScene.player.Point < 500 * customer.getWanabe())
                GameScene.player.Point = 0;
            else
            {
                GameScene.player.Point -= 500 * customer.getWanabe();
            }
        }
    }
}
