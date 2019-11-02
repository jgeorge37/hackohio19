using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game8.Commands;

namespace Game8
{
    class Controller
    {
        KeyboardState now;
        KeyboardState past;
        Dictionary<Keys, ICommand> keys;
        public Controller()
        {
            now = Keyboard.GetState();
            past = Keyboard.GetState();
            keys = new Dictionary<Keys, ICommand>();
        }

        public void AddKey(Keys k, ICommand val)
        {
            keys.Add(k, val);
        }

        public void Update()
        {
            now = Keyboard.GetState();
            foreach (Keys x in keys.Keys)
            {
                if(now.IsKeyDown(x) && past.IsKeyUp(x))
                {
                    keys[x].Execute();
                }
            }
            past = now;
        }



       
    }
}
